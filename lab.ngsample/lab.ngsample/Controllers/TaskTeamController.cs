using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using lab.ngsample.Controllers;

namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskTeamController : BaseController
    {
        #region Variable Declaration

        private readonly ITaskTeamRepository _taskTeamRepository;
        private readonly IAppMasterEmailTemplateRepository _appMasterEmailTemplateRepository;
        private readonly IUserMailServerRepository _userMailServerRepository;
        private readonly IEmailGroupRepository _emailGroupRepository;

        #endregion

        #region Constructor

        public TaskTeamController(IAppMenuRepository appmenuRepository,
            IUserActivityRepository userActivityRepository,
            IUserEmailCommunicationRepository userEmailCommunicationRepository, ITaskRepository taskRepository,
            IUserRepository userRepository, ICompanyRepository companyRepository, ITaskTeamRepository taskteamRepository,
            IAppMasterEmailTemplateRepository appMasterEmailTemplateRepository,
            IUserMailServerRepository userMailServerRepository,
            IEmailGroupRepository emailGroupRepository,
            IContactRepository contactRepository
            )
            : base(appmenuRepository, userActivityRepository, userEmailCommunicationRepository, taskRepository)
        {
            UserRepository = userRepository;
            CompanyRepository = companyRepository;
            TaskRepository = taskRepository;
            _taskTeamRepository = taskteamRepository;
            ContactRepository = contactRepository;
            _appMasterEmailTemplateRepository = appMasterEmailTemplateRepository;
            _userMailServerRepository = userMailServerRepository;
            _emailGroupRepository = emailGroupRepository;
        }

        #endregion

        #region Team List

        [UserAuthorize]
        [OutputCache(Duration = 0)]
        public ActionResult IndexAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, string taskId)
        {
            if (dataSourceRequest.Filters == null)
            {
                dataSourceRequest.Filters = new List<IFilterDescriptor>();
            }
            FilterDescriptor newDesc = new FilterDescriptor("TaskID", FilterOperator.IsEqualTo, taskId);
            dataSourceRequest.Filters.Add(newDesc);
            DataSourceResult result =
                _taskTeamRepository.AllIncluding(taskId.ToInteger(true), taskteam => taskteam.Task,
                    taskteam => taskteam.User)
                    .Select(
                        taskteam =>
                            new
                            {
                                taskteam.CreateDate,
                                taskteam.TaskTeamID,
                                taskteam.TaskID,
                                taskteam.Task.CreatedByUserID,
                                taskteam.UserID,
                                UserName = taskteam.User.FirstName + " " + taskteam.User.LastName,
                                UserEmailAddress = taskteam.User.EmailAddress,
                                IsManager = taskteam.IsManager,
                                IsParticipant = taskteam.IsParticipant,
                                IsShared = taskteam.IsShared,
                                UserPhone = taskteam.User.Phone,
                                UserPhotoFileName = taskteam.User.PhotoFileName
                            })
                    .ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Editor, Save & Delete

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAjax(TaskTeam taskteam)
        {
            bool isNew = taskteam.TaskTeamID == 0;

            if (ModelState.IsValid)
            {
                var checkExisting = _taskTeamRepository.FindFromTaskTeam(taskteam.TaskID, taskteam.UserID);
                if (checkExisting == null)
                {
                    if (taskteam.IsManager)
                    {
                        //TaskTeam taskTeam = new TaskTeam();
                        var team = _taskTeamRepository.FindFromTaskTeamByTaskId(taskteam.TaskID);
                        foreach (TaskTeam existingTeam in team)
                        {
                            taskteam.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                            existingTeam.IsManager = false;
                            //
                            if (!existingTeam.IsParticipant && !existingTeam.IsShared)
                            {
                                existingTeam.IsShared = true;
                            }
                            //
                            _taskTeamRepository.InsertOrUpdate(existingTeam, true);
                            _taskTeamRepository.Save();
                        }
                    }

                    try
                    {
                        if (!taskteam.IsManager && !taskteam.IsParticipant && !taskteam.IsShared)
                        {
                            taskteam.IsShared = true;
                        }
                        taskteam.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        _taskTeamRepository.InsertOrUpdate(taskteam, false);
                        _taskTeamRepository.Save();
                        if (isNew)
                        {
                            taskteam.SuccessMessage = "Task Team has been added successfully";
                        }
                        else
                        {
                            taskteam.SuccessMessage = "Task Team has been updated successfully";
                        }
                    }
                    catch (CustomException ex)
                    {
                        taskteam.ErrorMessage = ex.UserDefinedMessage;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Manage(ex);
                        taskteam.ErrorMessage = Constants.Messages.UnhandelledError;
                    }
                }
                else
                {
                    taskteam.ErrorMessage = "The user has already been added into the team";
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        taskteam.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (taskteam.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                int userId = taskteam.UserID;
                return Json(new { success = true, userId = userId, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAsTaskTeamManagerAjax(TaskTeam taskteam)
        {
            try
            {
                var team = _taskTeamRepository.FindFromTaskTeamByTaskId(taskteam.TaskID);
                foreach (TaskTeam existingTeam in team)
                {
                    taskteam.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                    existingTeam.IsManager = false;
                    //
                    if (!existingTeam.IsParticipant && !existingTeam.IsShared)
                    {
                        existingTeam.IsShared = true;
                    }
                    //
                    _taskTeamRepository.InsertOrUpdate(existingTeam, true);
                    _taskTeamRepository.Save();
                }
                var teamToPerform = _taskTeamRepository.Find(taskteam.TaskTeamID);
                teamToPerform.IsManager = true;
                teamToPerform.IsParticipant = false;
                teamToPerform.IsShared = false;
                teamToPerform.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                _taskTeamRepository.InsertOrUpdate(teamToPerform, true);
                _taskTeamRepository.Save();
                taskteam.SuccessMessage = "Primary manager has been changed successfully";
            }
            catch (CustomException ex)
            {
                taskteam.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                taskteam.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAsTaskTeamParticipantAjax(TaskTeam taskteam)
        {
            try
            {
                var teamToPerform = _taskTeamRepository.Find(taskteam.TaskTeamID);
                //teamToPerform.IsManager = true;
                teamToPerform.IsParticipant = true;
                teamToPerform.IsShared = false;
                teamToPerform.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                _taskTeamRepository.InsertOrUpdate(teamToPerform, true);
                _taskTeamRepository.Save();
                taskteam.SuccessMessage = "Primary manager has been changed successfully";
            }
            catch (CustomException ex)
            {
                taskteam.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                taskteam.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAsTaskTeamSharedAjax(TaskTeam taskteam)
        {
            try
            {
                var teamToPerform = _taskTeamRepository.Find(taskteam.TaskTeamID);
                //teamToPerform.IsManager = true;
                teamToPerform.IsParticipant = false;
                teamToPerform.IsShared = true;
                teamToPerform.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                _taskTeamRepository.InsertOrUpdate(teamToPerform, true);
                _taskTeamRepository.Save();
                taskteam.SuccessMessage = "Primary manager has been changed successfully";
            }
            catch (CustomException ex)
            {
                taskteam.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                taskteam.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SendBulkMailToAssignedUsersAjax(int[] userId, int taskId, string type)
        {
            var taskteam = new TaskTeam();
            //
            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            var serverPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //
            var contactIds = new List<int>();
            var emailIds = new List<string>();
            try
            {
                var mailServer = _userMailServerRepository.FindActiveConfiguration(1);
                if (userId.Count() > 0)
                {
                    var template = _appMasterEmailTemplateRepository.FindByName("Task Assignment Email Template");
                    var task = TaskRepository.FindTaskInfoForSendingEmail(taskId);
                    var emailTemplateViewModel = new EmailTemplateViewModel
                    {
                        TaskID = taskId,
                        BodyHtml = template == null ? "" : template.EmailMessage
                    };
                    foreach (var contact in userId.Select(o => ContactRepository.FindContactIdAndEmailByUserId(o)))
                    {
                        contactIds.Add(contact.ContactID);
                        emailIds.Add(contact.PrimaryEmailAddress);
                    }
                    var subjectString = "";
                    if (type == "m")
                    {
                        subjectString = " assigned you in a Task as Manager: ";
                    }
                    if (type == "p")
                    {
                        subjectString = " assigned you a Task: ";
                    }
                    if (type == "s")
                    {
                        subjectString = " shared a Task with you: ";
                    }
                    var emailGroup = new EmailGroup
                    {
                        ContactIDList = contactIds.ToArray(),
                        EmailAddress = string.Join(",", emailIds.ToArray()),
                        MailServerId = mailServer.MailServerId,
                        Subject = CurrentLoggedInUser.Name + subjectString + task.Subject,
                        BodyText = ProcessTaskEmailTemplate(emailTemplateViewModel, baseUrl, serverPath),
                        LastUpdatedByUserID = CurrentLoggedInUser.UserID
                    };
                    _emailGroupRepository.InsertOrUpdate(emailGroup);
                    var result = new AsyncEmailSender().Send(emailGroup.EmailGroupId, baseUrl, serverPath);
                }
            }
            catch (CustomException ex)
            {
                taskteam.ErrorMessage = "Email Sending failed";
            }

            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            TaskTeam taskteam = _taskTeamRepository.Find(id);
            if (taskteam == null)
            {
                taskteam = new TaskTeam();
                taskteam.ErrorMessage = "TaskTeam not found";
            }
            else
            {
                try
                {
                    _taskTeamRepository.Delete(taskteam);
                    _taskTeamRepository.Save();
                    taskteam.SuccessMessage = "Task Team has been deleted successfully";
                }
                catch (CustomException ex)
                {
                    taskteam.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    taskteam.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            if (taskteam.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskteam) });
            }
        }

        #endregion
    }
}

