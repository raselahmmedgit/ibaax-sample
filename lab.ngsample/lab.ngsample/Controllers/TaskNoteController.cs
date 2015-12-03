using System.Linq;
using EasySoft.Helper;
using Core.Business.Helpers;
using Core.Business.Repositories;
using Core.Business.Repositories.AppEmail;
using Core.Common;
using Core.Common.ExceptionLogging;
using Core.Data.Models;
using Core.Data.ViewModels;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RB.Areas.TaskManagement.Controllers;

namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskNoteController : TaskManagementBaseController
    {
        #region Variable Declaration

        private readonly ITaskNoteRepository _taskNoteRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAppMasterEmailTemplateRepository _appMasterEmailTemplateRepository;
        private readonly IUserMailServerRepository _userMailServerRepository;
        private readonly IEmailGroupRepository _emailGroupRepository;

        #endregion

        #region Constructor

        public TaskNoteController(IAppMenuRepository appmenuRepository, IUserActivityRepository userActivityRepository,
            IUserEmailCommunicationRepository userEmailCommunicationRepository, ITaskRepository taskRepository,
            IUserRepository userRepository, ICompanyRepository companyRepository, ITaskNoteRepository tasknoteRepository,
            IContactRepository contactRepository,
            IAppMasterEmailTemplateRepository appMasterEmailTemplateRepository,
            IUserMailServerRepository userMailServerRepository,
            IEmailGroupRepository emailGroupRepository)
            : base(appmenuRepository, userActivityRepository, userEmailCommunicationRepository, taskRepository)
        {
            this.UserRepository = userRepository;
            this.CompanyRepository = companyRepository;
            this.TaskRepository = taskRepository;
            this._taskNoteRepository = tasknoteRepository;
            this.ContactRepository = contactRepository;
            _contactRepository = contactRepository;
            _appMasterEmailTemplateRepository = appMasterEmailTemplateRepository;
            _userMailServerRepository = userMailServerRepository;
            _emailGroupRepository = emailGroupRepository;
        }

        #endregion

        #region Note List

        [UserAuthorize]
        [OutputCache(Duration = 0)]
        public ActionResult IndexAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int ParentID)
        {
            if (dataSourceRequest.Filters == null)
            {
                dataSourceRequest.Filters = new List<IFilterDescriptor>();
            }
            DataSourceResult result =
                _taskNoteRepository.AllIncluding(ParentID, tasknote => tasknote.CreatedByUser,
                    tasknote => tasknote.LastUpdatedByUser, tasknote => tasknote.Task)
                    .OrderByDescending(item => item.CreateDate)
                    .Select(
                        tasknote =>
                            new
                            {
                                ID = tasknote.TaskNoteID,
                                CurrentUserId = CurrentLoggedInUser.UserID,
                                ParentModelID = tasknote.TaskID,
                                ModelName = "TaskNote",
                                PhotoFileName = tasknote.CreatedByUser.PhotoFileName,
                                tasknote.CreateDate,
                                tasknote.CreatedByUserID,
                                CreatedByUserName =
                                    tasknote.CreatedByUser.FirstName + " " + tasknote.CreatedByUser.LastName,
                                tasknote.LastUpdateDate,
                                tasknote.LastUpdatedByUserID,
                                LastUpdatedByUserName =
                                    tasknote.LastUpdatedByUser.FirstName + " " + tasknote.LastUpdatedByUser.LastName,
                                tasknote.Notes,
                                tasknote.TaskNoteID,
                                tasknote.TaskID
                            }).ToList()
                    .ToDataSourceResult(dataSourceRequest);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Note Save, Delete & Editor

        [UserAuthorize]
        public ActionResult EditorAjax(int id, string taskId)
        {
            TaskNote tasknote = null;
            if (id > 0)
            {
                tasknote = _taskNoteRepository.Find(id);
                if (tasknote == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Task Note not found");
                }
            }
            else
            {
                tasknote = new TaskNote();
                tasknote.TaskID = taskId.ToInteger(true);
            }

            ViewBag.PossibleCreatedByCompanies = CompanyRepository.All;
            return Content(this.RenderPartialViewToString(Constants.PartialViews.EditorPopUp, tasknote));
        }

        [UserAuthorize]
        public JsonResult TaskNoteEditorAjax(TaskNote tasknote)
        {
            if (tasknote.TaskNoteID > 0)
            {
                tasknote = _taskNoteRepository.Find(tasknote.TaskNoteID);
            }

            List<string> values = new List<string>();
            values.Add(tasknote.Notes);

            ViewBag.TaskNoteId = tasknote.TaskNoteID;
            return Json(tasknote.Notes, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAjax(TaskNote tasknote)
        {
            if (tasknote.NoteID > 0)
            {
                tasknote.TaskNoteID = tasknote.NoteID;
            }
            if (tasknote.ParentModelID > 0)
            {
                tasknote.TaskID = tasknote.ParentModelID;
            }
            bool isNew = tasknote.TaskNoteID == 0;
            var flag = false;
            if (ModelState.IsValid)
            {

                try
                {
                    tasknote.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                    _taskNoteRepository.InsertOrUpdate(tasknote);
                    _taskNoteRepository.Save();
                    //send email to the manager for this task
                    //flag = SendCommentEmailAjax(tasknote.TaskNoteID);
                    if (isNew)
                    {
                        tasknote.SuccessMessage = "Task Comment has been added successfully";
                    }
                    else
                    {
                        tasknote.SuccessMessage = "Task Comment has been updated successfully";
                    }
                }
                catch (CustomException ex)
                {
                    tasknote.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    tasknote.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        tasknote.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (tasknote.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (tasknote.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, noteId = tasknote.TaskNoteID, flag = flag, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, tasknote) });
            }
            else
            {
                return Json(new { success = true, noteId = tasknote.TaskNoteID, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, tasknote) });
            }
        }

        public ActionResult SendCommentEmailAjax(int noteId)
        {
            var flag = false;
            //
            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            var serverPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //
            var contactIds = new List<int>();
            var emailIds = new List<string>();
            var mailServer = _userMailServerRepository.FindActiveConfiguration(1);
            var note = _taskNoteRepository.Find(noteId);
            var tasks = TaskRepository.FindNoteInfoForEmailAlert(note.TaskID);
            var idList = tasks.Where(t => t.UserID != CurrentLoggedInUser.UserID).Select(task => task.UserID).ToList();
            var userId = idList.ToArray();
            try
            {
                if (userId.Count() > 0)
                {
                    var template = _appMasterEmailTemplateRepository.FindByName("Task Comment Alert Email Template");
                    var task = TaskRepository.FindTaskInfoForSendingEmail(note.TaskID);
                    var emailTemplateViewModel = new EmailTemplateViewModel
                    {
                        TaskID = note.TaskID,
                        BodyHtml = template.EmailMessage
                    };
                    foreach (var contact in userId.Select(o => ContactRepository.FindContactIdAndEmailByUserId(o)))
                    {
                        contactIds.Add(contact.ContactID);
                        emailIds.Add(contact.PrimaryEmailAddress);
                    }
                    var emailGroup = new EmailGroup
                    {
                        ContactIDList = contactIds.ToArray(),
                        EmailAddress = string.Join(",", emailIds.ToArray()),
                        MailServerId = mailServer.MailServerId,
                        Subject = CurrentLoggedInUser.Name + " Commented on " + task.Subject,
                        BodyText = ProcessTaskEmailTemplate(emailTemplateViewModel, baseUrl, serverPath),
                        LastUpdatedByUserID = CurrentLoggedInUser.UserID
                    };
                    _emailGroupRepository.InsertOrUpdate(emailGroup);
                    var result = new AsyncEmailSender().Send(emailGroup.EmailGroupId, baseUrl, serverPath);
                    flag = true;
                }
            }
            catch (CustomException ex)
            {
                flag = false;
            }
            return Json(new { success = flag });
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult ModifyTaskNoteAjaxAjax(TaskNote tasknote)
        {
            bool isNew = tasknote.TaskNoteID == 0;
            TaskNote existingTaskNote = new TaskNote();
            var flag = false;
            try
            {
                if (tasknote.TaskNoteID > 0)
                {
                    existingTaskNote = _taskNoteRepository.Find(tasknote.TaskNoteID);
                }
                existingTaskNote.Notes = tasknote.Notes;
                existingTaskNote.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                _taskNoteRepository.InsertOrUpdate(existingTaskNote);
                _taskNoteRepository.Save();
                //flag = SendCommentEmailAjax(tasknote.TaskNoteID);
                if (isNew)
                {
                    existingTaskNote.SuccessMessage = "Task Note has been added successfully";
                }
                else
                {
                    existingTaskNote.SuccessMessage = "Task Note has been updated successfully";
                }
            }
            catch (CustomException ex)
            {
                existingTaskNote.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                existingTaskNote.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            if (existingTaskNote.ErrorMessage.IsNotNullOrEmpty())
            {
                return
                    Json(
                        new
                        {
                            success = false,
                            flag = flag,
                            noteId = tasknote.NoteID,
                            data = this.RenderPartialViewToString(Constants.PartialViews.Alert, existingTaskNote)
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            success = true,
                            noteId = tasknote.NoteID,
                            data = this.RenderPartialViewToString(Constants.PartialViews.Alert, existingTaskNote)
                        });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            TaskNote tasknote = _taskNoteRepository.Find(id);
            if (tasknote == null)
            {
                tasknote = new TaskNote();
                tasknote.ErrorMessage = "TaskNote not found";
            }
            else
            {
                try
                {
                    _taskNoteRepository.Delete(tasknote);
                    _taskNoteRepository.Save();
                    tasknote.SuccessMessage = "Task Note has been deleted successfully";
                }
                catch (CustomException ex)
                {
                    tasknote.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    tasknote.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            if (tasknote.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, tasknote) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, tasknote) });
            }
        }

        #endregion
    }
}

