//*********************************************************
//
//    Copyright (c) iBaax LLC. All rights reserved.
//	  Contact: Faisal Alam, sal@iBaax.com
//	  http://www.iBaax.com
//
//*********************************************************

using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskController : TaskManagementBaseController
    {
        #region Variable Declaration

        private readonly ITaskStatusCategoryRepository _taskStatusCategoryRepository;
        private readonly ITaskDocumentRepository _taskDocumentRepository;
        private readonly ITaskTagCategoryRepository _taskTagCategoryRepository;
        private readonly ITaskTagMapRepository _taskTagMapRepository;
        private readonly IAppMasterEmailTemplateRepository _appMasterEmailTemplateRepository;
        private readonly IUserMailServerRepository _userMailServerRepository;
        private readonly IEmailGroupRepository _emailGroupRepository;
        private readonly ITaskTeamRepository _taskTeamRepository;

        #endregion

        #region Constructor

        public TaskController(IUserActivityRepository userActivityRepository, IAppMenuRepository appMenuRepository,
            IUserEmailCommunicationRepository userEmailCommunicationRepository, IUserRepository userRepository,
            ITaskStatusCategoryRepository taskstatuscategoryRepository,
            IContactRepository contactRepository,
            ICompanyRepository companyRepository,
            ITaskRepository taskRepository,
            ITaskTypeCategoryRepository taskTypeCategoryRepository,
            ITaskPriorityCategoryRepository taskPriorityCategoryRepository,
            ITaskDocumentRepository taskDocumentRepository,
            ITaskStatusCategoryRepository taskStatusCategoryRepository,
            ITaskTeamRepository taskTeamRepository,
            ITaskTagCategoryRepository taskTagCategoryRepository,
            ITaskTagMapRepository taskTagMapRepository,
            IAppMasterEmailTemplateRepository appMasterEmailTemplateRepository,
            IUserMailServerRepository userMailServerRepository,
            IEmailGroupRepository emailGroupRepository)
            : base(appMenuRepository, userActivityRepository, userEmailCommunicationRepository, taskRepository)
        {
            UserRepository = userRepository;
            _taskStatusCategoryRepository = taskstatuscategoryRepository;
            ContactRepository = contactRepository;
            CompanyRepository = companyRepository;
            TaskRepository = taskRepository;
            _taskDocumentRepository = taskDocumentRepository;
            TaskStatusCategoryRepository = taskStatusCategoryRepository;
            TaskTypeCategoryRepository = taskTypeCategoryRepository;
            TaskPriorityCategoryRepository = taskPriorityCategoryRepository;
            _taskTagCategoryRepository = taskTagCategoryRepository;
            _taskTagMapRepository = taskTagMapRepository;
            _userMailServerRepository = userMailServerRepository;
            _appMasterEmailTemplateRepository = appMasterEmailTemplateRepository;
            _emailGroupRepository = emailGroupRepository;
            _taskTeamRepository = taskTeamRepository;
        }

        #endregion

        #region Task and Schedule Editor
        
        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task subject only while subject is modified.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveSubjectAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.Subject = task.Subject;
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task description only while description is modified.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveDescriptionAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.Description = task.Description;
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task priority only while priority is modified.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SavePriorityAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.TaskPriorityCategoryID = task.TaskPriorityCategoryID;
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method mark tasks as overdue after crossing end date and task border color goes wrong.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult MarkTaskAsOverdueAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.TaskStatusCategoryID = task.TaskStatusCategoryID;
                        if (task.TaskStatusCategoryID == 1)
                        {
                            exTask.CompletionPercent = 0;
                        }
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task status category only while status category is modified.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveTaskStatusCategoryAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.TaskStatusCategoryID = task.TaskStatusCategoryID;
                        exTask.CompletionPercent = task.CompletionPercent;
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method add task tag(single/multiple) from task dashboard.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveTaskTagCategoryAjax(Task task)
        {
            Task exTask = null;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        var existingTags = _taskTagMapRepository.FindByTaskId(task.TaskID);
                        if (existingTags != null)
                        {
                            foreach (var tagMap in existingTags)
                            {
                                _taskTagMapRepository.Delete(tagMap);
                                _taskTagMapRepository.Save();
                            }
                        }
                        if (task.TagIdList != null)
                        {
                            foreach (var tagId in task.TagIdList)
                            {
                                TaskTagMap tagMap = new TaskTagMap();
                                tagMap.TaskID = task.TaskID;
                                tagMap.TaskTagCategoryID = tagId;
                                tagMap.ContactID = task.ContactID;
                                tagMap.CompanyID = task.CompanyID;
                                tagMap.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                                _taskTagMapRepository.InsertOrUpdate(tagMap);
                            }
                        }
                        exTask.SuccessMessage = "Task tag has been updated successfully";
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
            }
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task schedule through ajax call.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveTaskScheduleAjax(Task task)
        {
            Task exTask = null;
            bool isSuccess = false;
            bool isRefresh = false;
            try
            {
                if (task.TaskID > 0)
                {
                    exTask = TaskRepository.Find(task.TaskID);
                    if (exTask != null)
                    {
                        exTask.EstimatedStartDate = task.EstimatedStartDate;
                        exTask.EstimatedEndDate = task.EstimatedEndDate;
                        exTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;

                        if (task.EstimatedEndDate > DateTime.Now && exTask.TaskStatusCategoryID == 4)
                        {
                            exTask.TaskStatusCategoryID = 1;
                            exTask.CompletionPercent = 0;
                            isRefresh = true;
                        }
                        TaskRepository.InsertOrUpdate(exTask);
                        TaskRepository.Save();
                        exTask.SuccessMessage = "Task Updated successfully";
                        isSuccess = true;
                    }
                    else
                    {
                        exTask = new Task();
                        exTask.ErrorMessage = "The task does not exist";
                        isSuccess = false;
                    }
                }
            }
            catch (Exception x)
            {
                exTask = new Task();
                exTask.ErrorMessage = "Error occured";
                isSuccess = false;
            }
            return Json(new { success = isSuccess, isRefresh = isRefresh, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, exTask) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save task from different task editor view through ajax call.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAjax(Task task)
        {
            bool isNew = task.TaskID == 0;
            if (task.DisplayId.IsNullOrEmpty())
            {
                task.DisplayId = MiscUtility.GetTaskPersonalizedId();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    task.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                    TaskRepository.InsertOrUpdate(task);
                    TaskRepository.Save();
                    if (isNew)
                    {
                        task.SuccessMessage = "Task has been added successfully";
                    }
                    else
                    {
                        task.SuccessMessage = "Task has been updated successfully";
                    }
                }
                catch (CustomException ex)
                {
                    task.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    task.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        task.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (task.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (task.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
            }
            else
            {
                int id = task.TaskID;
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
            }
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method insert task form dashboard popup task editor, my team task editor, organizational task editor, property and contact task editor.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveQuickTaskAjax(Task task)
        {
            bool isNew = task.TaskID == 0;
            if (task.DisplayId.IsNullOrEmpty())
            {
                task.DisplayId = MiscUtility.GetTaskPersonalizedId();
            }
            try
            {
                task.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                TaskRepository.InsertOrUpdate(task);
                TaskRepository.Save();
                //add task tag category
                if (task.TagIdList != null)
                {
                    foreach (var tagId in task.TagIdList)
                    {
                        var tagMap = new TaskTagMap
                        {
                            TaskID = task.TaskID,
                            TaskTagCategoryID = tagId,
                            ContactID = task.ContactID,
                            CompanyID = task.CompanyID,
                            LastUpdatedByUserID = CurrentLoggedInUser.UserID
                        };
                        _taskTagMapRepository.InsertOrUpdate(tagMap);
                    }
                }

                if (isNew)
                {
                    task.SuccessMessage = "Task has been added successfully";
                }
                else
                {
                    task.SuccessMessage = "Task has been updated successfully";
                }
            }
            catch (CustomException ex)
            {
                task.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                task.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            return Json(new { id = task.TaskID, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This postback method save task form synchronous task editor.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveQuickUserTask(Task task)
        {
            bool isNew = task.TaskID == 0;
            bool isSuccess = false;
            if (task.DisplayId.IsNullOrEmpty())
            {
                task.DisplayId = MiscUtility.GetTaskPersonalizedId();
            }
            try
            {
                task.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                TaskRepository.InsertOrUpdate(task);
                TaskRepository.Save();

                //add task tag category
                if (task.TagIdList != null)
                {
                    foreach (var tagId in task.TagIdList)
                    {
                        TaskTagMap tagMap = new TaskTagMap();
                        tagMap.TaskID = task.TaskID;
                        tagMap.TaskTagCategoryID = tagId;
                        tagMap.ContactID = task.ContactID;
                        tagMap.CompanyID = task.CompanyID;
                        tagMap.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        _taskTagMapRepository.InsertOrUpdate(tagMap);
                    }
                }

                if (Session["TaskFileFileName"] != null)
                {
                    foreach (var fileName in Session["TaskFileFileName"].ToString()
                        .Split(new[] { "___" }, StringSplitOptions.None)
                        .Where(x => !String.IsNullOrEmpty(x)))
                    {
                        var taskDocument = new TaskDocument
                        {
                            TaskID = task.TaskID
                        };

                        var physicalPath = Path.Combine(Server.MapPath(Constants.Paths.TemporaryFileUploadPath),
                            fileName);
                        taskDocument.Document = Utility.ReadFile(physicalPath);
                        taskDocument.DocumentFileName = fileName;
                        taskDocument.DocumentFileSize = taskDocument.Document.LongLength;
                        taskDocument.CreatedByUserID = CurrentLoggedInUser.UserID;
                        taskDocument.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        try
                        {
                            System.IO.File.Delete(physicalPath);
                        }
                        catch
                        {
                        }

                        _taskDocumentRepository.InsertOrUpdate(taskDocument);
                    }

                    Session["TaskFileFileName"] = null;
                    _taskDocumentRepository.Save();
                }
                //send email to participant users
                if (task.ManagerID > 0)
                {
                    var manager = new List<int> { task.ManagerID };
                    SendBulkMailToAssignedUsersAjax(manager.ToArray(), task.TaskID, "m");
                }
                //send email to participant users
                if (task.ParticipantsID != null)
                {
                    task.ParticipantsID.Remove(task.ManagerID);
                    SendBulkMailToAssignedUsersAjax(task.ParticipantsID.ToArray(), task.TaskID,"p");
                }
                //send email to participant users
                if (task.SharedUsersID != null)
                {
                    if (task.ParticipantsID != null)
                    {
                        foreach (var pid in task.ParticipantsID)
                        {
                            task.SharedUsersID.Remove(pid);
                        }
                    }
                    
                    SendBulkMailToAssignedUsersAjax(task.SharedUsersID.ToArray(), task.TaskID, "s");
                }
                //
                task.SuccessMessage = "Task has been added successfully";
                isSuccess = true;
            }
            catch (CustomException ex)
            {
                task.ErrorMessage = ex.UserDefinedMessage;
                isSuccess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                task.ErrorMessage = Constants.Messages.UnhandelledError;
                isSuccess = false;
            }
            return Json(new { success = isSuccess, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method send bulk email to assigned task user with a task assignment email template.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Boolean SendBulkMailToAssignedUsersAjax(int[] userId, int taskId, string type)
        {
            var taskteam = new TaskTeam();
            //
            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            var serverPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //
            var contactIds = new List<int>();
            var emailIds = new List<string>();
            var flag = false;
            var subjectString = "";
            if (type == "p")
            {
                subjectString = " assigned you a Task: ";
            }
            if (type == "s")
            {
                subjectString = " shared a Task with you: ";
            }
            if (type == "m")
            {
                subjectString = " assigned you in a Task as Manager: ";
            }
            //
            try
            {
                if (userId.Count() > 0)
                {
                    var template = _appMasterEmailTemplateRepository.FindByName("Task Assignment Email Template");
                    var mailServer = _userMailServerRepository.FindActiveConfiguration(1);
                    var task = TaskRepository.FindTaskInfoForSendingEmail(taskId);
                    if (template != null && mailServer != null)
                    {
                        var emailTemplateViewModel = new EmailTemplateViewModel
                        {
                            TaskID = taskId,
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
                            Subject = CurrentLoggedInUser.Name + subjectString + task.Subject,
                            BodyText = ProcessTaskEmailTemplate(emailTemplateViewModel, baseUrl, serverPath),
                            LastUpdatedByUserID = CurrentLoggedInUser.UserID
                        };
                        _emailGroupRepository.InsertOrUpdate(emailGroup);

                        System.Threading.Tasks.Task.Factory.StartNew(() => new AsyncEmailSender().Send(emailGroup.EmailGroupId, baseUrl, serverPath));

                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (CustomException ex)
            {
                taskteam.ErrorMessage = "Email Sending failed";
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > When a task is dropped inside of the calendar the this method save the task modification history.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveDroppedAndResizedTaskAjax(Task task)
        {
            bool isNew = task.TaskID == 0;
            Task existingTask = new Task();

            if (task.TaskID > 0)
            {
                existingTask = TaskRepository.Find(task.TaskID);
            }
            try
            {
                existingTask.LastUpdatedByUserID = CurrentLoggedInUser.UserID;

                existingTask.EstimatedStartDate = task.EstimatedStartDate;
                existingTask.EstimatedEndDate = task.EstimatedEndDate;

                TaskRepository.InsertOrUpdate(existingTask);
                TaskRepository.Save();
                if (isNew)
                {
                    existingTask.SuccessMessage = "Task has been added successfully";
                }
                else
                {
                    existingTask.SuccessMessage = "Task has been updated successfully";
                }
            }
            catch (CustomException ex)
            {
                existingTask.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                existingTask.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            if (existingTask.ErrorMessage.IsNotNullOrEmpty())
            {
                return
                    Json(
                        new
                        {
                            success = false,
                            data = this.RenderPartialViewToString(Constants.PartialViews.Alert, existingTask)
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            success = true,
                            data = this.RenderPartialViewToString(Constants.PartialViews.Alert, existingTask)
                        });
            }
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method delete task from database based on task id.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Task task = TaskRepository.Find(id);
            if (task == null)
            {
                task = new Task();
                task.ErrorMessage = "Task not found";
            }
            else
            {
                try
                {
                    TaskRepository.Delete(id);
                    TaskRepository.Save();
                    task.SuccessMessage = "Task has been deleted successfully";
                }
                catch (CustomException ex)
                {
                    task.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    task.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            if (task.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task), statusId = task.TaskStatusCategoryID });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task), statusId = task.TaskStatusCategoryID });
            }
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method return the task editor view as json while new task button is clicked.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult ReturnViewAjax(int id)
        {
            ViewBag.TaskId = id;
            ViewBag.CurrentUser = CurrentLoggedInUser.UserID;
            return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_CreateorEditTaskOrEvent.cshtml", null) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns view of workflow task attachments editor.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [UserAuthorize]
        public JsonResult AttachmentEditorAjax()
        {
            return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_WorkflowTaskAttachments.cshtml", null) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method filter dashboard task based on task parameter.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult FilterDashboardTaskAjax(TaskSearchViewModel searchViewModel)
        {
            try
            {
                var taskList = TaskRepository.GetAllTaskForCalendarById(searchViewModel).ToList();
                foreach (var t in taskList)
                {
                    List<TaskTagCategory> taskTagCategory = null;
                    taskTagCategory = _taskTagCategoryRepository.FindTaskTagCategoryByTaskId(t.TaskID);
                    t.TagCategoryList = taskTagCategory;
                }
                return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_TasksListCategory.cshtml", taskList) });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false });
            }
        }

        #region Task Dashbroad By: Rasel Ahmmed

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > Load task dashboard data and preview task dashboard.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult DashboardPreview(int? id)
        {
            Task task = null;
            if (id > 0)
            {
                task = TaskRepository.Find(id.Value);
            }
            if (task != null)
            {
                var isTeamTask = _taskTeamRepository.IsExistTaskTeam(task.TaskID, CurrentLoggedInUser.UserID);
                if (task.CreatedByUserID == CurrentLoggedInUser.UserID || isTeamTask)
                {
                    //Find overdue tasks and mark those.
                    var overdueTasks = TaskRepository.FindOverDueTask();
                    foreach (var o in overdueTasks)
                    {
                        var t = TaskRepository.Find(o.TaskID);
                        t.TaskStatusCategoryID = 4;
                        TaskRepository.InsertOrUpdate(t);
                    }
                    //..
                    TaskOverviewModel newTaskOverviewModel = TaskRepository.FindOverview(task.TaskID, task);
                    LoadTaskDashboardCommonData(task.TaskID, TaskRepository, task);
                    ViewBag.NoteControllerName = "TaskNote";
                    ViewBag.tagList = _taskTagCategoryRepository.FindAllTaskTagCategory();
                    ViewBag.CurrentUserId = CurrentLoggedInUser.UserID;

                    return View(newTaskOverviewModel);
                }
                else
                {
                    return RedirectToAction("NotFound", "Home", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Home", new { area = "" });
            }
        }

        #endregion

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This post method load task weekly schedule. The view display week list with details of assigned task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ViewResult WeeklySchedule()
        {
            var searchViewModel = new TaskSearchViewModel
            {
                IsAttachments = false,
                IsAssignedToMe = false,
                IsCreatedByMe = false,
                CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID
            };
            //
            ViewBag.CurrentLoggedInContactId = CurrentLoggedInContact.ContactID;

            var tasks = TaskRepository.QueryToCountTask(searchViewModel);
            ViewBag.CountedOpenTask = tasks.Count(o => o.TaskStatusCategoryID == 1);
            ViewBag.CountedInProgressTask = tasks.Count(o => o.TaskStatusCategoryID == 2);
            ViewBag.CountedCompletedTask = tasks.Count(o => o.TaskStatusCategoryID == 3);
            ViewBag.CountedOverDueTask = tasks.Count(o => o.TaskStatusCategoryID == 4);
            //

            ViewBag.TaskSearchViewModel = new TaskSearchViewModel();
            ViewBag.TaskSearchViewModel.CreatedByCompanyID =
                CurrentLoggedInUser.CreatedByCompanyID;

            ViewBag.TaskTagCategoryList = _taskTagCategoryRepository.FindAllTaskTagCategory();
            ViewBag.TaskPriorityCategoryList = TaskPriorityCategoryRepository.FindPriorityList();
            foreach (var o in ViewBag.TaskTagCategoryList)
            {
                o.CountedTask = _taskTagMapRepository.CountOrgTaskByTag(o.TaskTagCategoryID);
            }
            foreach (var o in ViewBag.TaskPriorityCategoryList)
            {
                o.CountedPriorityTask = TaskPriorityCategoryRepository.CountTaskByPriorityByCompanyId(o.TaskPriorityCategoryID);
            }
            //
            searchViewModel.StartDate = DateTime.Today.AddDays(-4);
            searchViewModel.EndDate = DateTime.Today;
            //
            var companyId = CurrentLoggedInUser.CreatedByCompanyID ?? 0;
            ViewBag.UserList = ContactRepository.FindAllOrganizationalEmployees(companyId);
            //
            TaskWeeklyScheduleViewModel newTaskWeeklyScheduleViewModel = TaskRepository.FindWeeklyScheduleViewModel(searchViewModel);
            newTaskWeeklyScheduleViewModel.LoggedInUserId = CurrentLoggedInUser.UserID;
            newTaskWeeklyScheduleViewModel.LoggedInUserPhotoFileName = CurrentLoggedInUser.PhotoFileName;
            newTaskWeeklyScheduleViewModel.LoggedInUserName = CurrentLoggedInUser.Name;
            return View(newTaskWeeklyScheduleViewModel);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This ajax method load task weekly schedule. The view display week list with details of assigned task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult WeeklyScheduleAjax(DateTime? StartDate, DateTime? EndDate, TaskSearchViewModel searchViewModel)
        {
            if (StartDate.HasValue)
            {
                searchViewModel.StartDate = StartDate;
            }
            else
            {
                searchViewModel.StartDate = DateTime.Today.AddDays(-4);
            }
            if (EndDate.HasValue)
            {
                searchViewModel.EndDate = EndDate;
            }
            else
            {
                searchViewModel.EndDate = searchViewModel.StartDate.Value.AddDays(4);
            }
            //if (!searchViewModel.StartDate.HasValue)
            //{
            //    searchViewModel.StartDate = DateTime.Today.AddDays(-4);
            //}
            //if (!searchViewModel.EndDate.HasValue)
            //{
            //    searchViewModel.EndDate = searchViewModel.StartDate.Value.AddDays(4);
            //}
            //
            searchViewModel.IsAssignedToMe = false;
            searchViewModel.IsCreatedByMe = false;
            searchViewModel.CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID;
            //
            TaskWeeklyScheduleViewModel newTaskWeeklyScheduleViewModel = TaskRepository.FindWeeklyScheduleViewModel(searchViewModel);

            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.WeeklyTaskDisplay, newTaskWeeklyScheduleViewModel) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method return the view of OrganizationWeekly Task with list of task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public ViewResult OrganizationWeeklyTask()
        {
            TaskMyTeamScheduleViewModel newTaskMyTeamScheduleViewModel = TaskRepository.FindEmployeeByOrganization();
            ViewBag.CurrentLoggedInContactId = CurrentLoggedInContact.ContactID;

            var searchViewModel = new TaskSearchViewModel
            {
                IsAttachments = false,
                IsAssignedToMe = false,
                IsCreatedByMe = false,
                CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID
            };
            var tasks = TaskRepository.QueryToCountTask(searchViewModel);
            newTaskMyTeamScheduleViewModel.CountedOpenTask = tasks.Count(o => o.TaskStatusCategoryID == 1);
            newTaskMyTeamScheduleViewModel.CountedInProgressTask = tasks.Count(o => o.TaskStatusCategoryID == 2);
            newTaskMyTeamScheduleViewModel.CountedCompletedTask = tasks.Count(o => o.TaskStatusCategoryID == 3);
            newTaskMyTeamScheduleViewModel.CountedOverDueTask = tasks.Count(o => o.TaskStatusCategoryID == 4);
            //

            newTaskMyTeamScheduleViewModel.TaskSearchViewModel = new TaskSearchViewModel();
            newTaskMyTeamScheduleViewModel.TaskSearchViewModel.CreatedByCompanyID =
                CurrentLoggedInUser.CreatedByCompanyID;

            newTaskMyTeamScheduleViewModel.TaskTagCategoryList = _taskTagCategoryRepository.FindAllTaskTagCategory();
            newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList = TaskPriorityCategoryRepository.FindPriorityList();
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskTagCategoryList)
            {
                o.CountedTask = _taskTagMapRepository.CountOrgTaskByTag(o.TaskTagCategoryID);
            }
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList)
            {
                o.CountedPriorityTask = TaskPriorityCategoryRepository.CountTaskByPriorityByCompanyId(o.TaskPriorityCategoryID);
            }

            return View(newTaskMyTeamScheduleViewModel);
        }

        #endregion

        #region Task List

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > Load task list in order to display beside the calendar.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetAllTaskListForCalendarByContactAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, TaskSearchViewModel searchViewModel)
        {
            DataSourceResult result = TaskRepository.GetAllTaskForCalendarById(searchViewModel).ToDataSourceResult(dataSourceRequest);
            foreach (Task t in result.Data)
            {
                List<TaskTagCategory> taskTagCategory = null;
                taskTagCategory = _taskTagCategoryRepository.FindTaskTagCategoryByTaskId(t.TaskID);
                t.TagCategoryList = taskTagCategory;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load my tasks.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ViewResult MyTasks(int? sid)
        {
            //TaskMyTeamScheduleViewModel newTaskMyTeamScheduleViewModel = TaskRepository.FindMyTeamScheduleViewModel(CurrentLoggedInContact.ContactID);
            var newTaskMyTeamScheduleViewModel = new TaskMyTeamScheduleViewModel();
            newTaskMyTeamScheduleViewModel.TaskSearchViewModel = new TaskSearchViewModel();
            ViewBag.CurrentLoggedInContactID = CurrentLoggedInContact.ContactID;
            ViewBag.LoggedInUserID = CurrentLoggedInUser.UserID;

            var searchViewModel = new TaskSearchViewModel
            {
                IsAttachments = false,
                IsAssignedToMe = false,
                IsCreatedByMe = false,
                UserID = CurrentLoggedInUser.UserID,
                CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID
            };
            var tasks = TaskRepository.QueryToCountTask(searchViewModel);
            newTaskMyTeamScheduleViewModel.CountedOpenTask = tasks.Count(o => o.TaskStatusCategoryID == 1);
            newTaskMyTeamScheduleViewModel.CountedInProgressTask = tasks.Count(o => o.TaskStatusCategoryID == 2);
            newTaskMyTeamScheduleViewModel.CountedCompletedTask = tasks.Count(o => o.TaskStatusCategoryID == 3);
            newTaskMyTeamScheduleViewModel.CountedOverDueTask = tasks.Count(o => o.TaskStatusCategoryID == 4);
            //

            newTaskMyTeamScheduleViewModel.TaskSearchViewModel.TaskStatusCategoryID = sid;
            newTaskMyTeamScheduleViewModel.TaskTagCategoryList = _taskTagCategoryRepository.FindAllTaskTagCategory();
            newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList = TaskPriorityCategoryRepository.FindPriorityList();
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskTagCategoryList)
            {
                o.CountedTask = _taskTagMapRepository.CountMyTaskByTag(o.TaskTagCategoryID);
            }
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList)
            {
                o.CountedPriorityTask = TaskPriorityCategoryRepository.CountTaskByPriorityByUserId(o.TaskPriorityCategoryID);
            }
            return View(newTaskMyTeamScheduleViewModel);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load my tasks.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ViewResult MyTeamSchedule(int? sid)
        {
            var newTaskMyTeamScheduleViewModel = new TaskMyTeamScheduleViewModel();
            newTaskMyTeamScheduleViewModel.TaskSearchViewModel = new TaskSearchViewModel();
            ViewBag.CurrentLoggedInContactID = CurrentLoggedInContact.ContactID;
            ViewBag.LoggedInUserID = CurrentLoggedInUser.UserID;

            var searchViewModel = new TaskSearchViewModel
            {
                IsAttachments = false,
                IsAssignedToMe = false,
                IsCreatedByMe = false,
                UserID = CurrentLoggedInUser.UserID,
                CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID
            };
            var tasks = TaskRepository.QueryToCountTask(searchViewModel);
            newTaskMyTeamScheduleViewModel.CountedOpenTask = tasks.Count(o => o.TaskStatusCategoryID == 1);
            newTaskMyTeamScheduleViewModel.CountedInProgressTask = tasks.Count(o => o.TaskStatusCategoryID == 2);
            newTaskMyTeamScheduleViewModel.CountedCompletedTask = tasks.Count(o => o.TaskStatusCategoryID == 3);
            newTaskMyTeamScheduleViewModel.CountedOverDueTask = tasks.Count(o => o.TaskStatusCategoryID == 4);
            //

            newTaskMyTeamScheduleViewModel.TaskSearchViewModel.TaskStatusCategoryID = sid;
            newTaskMyTeamScheduleViewModel.TaskTagCategoryList = _taskTagCategoryRepository.FindAllTaskTagCategory();
            newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList = TaskPriorityCategoryRepository.FindPriorityList();
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskTagCategoryList)
            {
                o.CountedTask = _taskTagMapRepository.CountMyTaskByTag(o.TaskTagCategoryID);
            }
            foreach (var o in newTaskMyTeamScheduleViewModel.TaskPriorityCategoryList)
            {
                o.CountedPriorityTask = TaskPriorityCategoryRepository.CountTaskByPriorityByUserId(o.TaskPriorityCategoryID);
            }
            return View(newTaskMyTeamScheduleViewModel);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load task and display inside of the calendar.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetAllDroppedTaskForCalendarByContactIdAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int Id, int taskStatusCategoryId)
        {
            DataSourceResult result = TaskRepository.GetAllDroppedTaskForCalendar(Id, taskStatusCategoryId).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load task and display inside of the calendar.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetDroppedScheduleTaskForCalendarAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int id, string type)
        {
            DataSourceResult result = TaskRepository.GetScheduledTaskForCalendar(id, type).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > Count task based on task status category.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetTaskCountedByTaskStatusCategoryAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int Id, string type)
        {
            var task = new Task();
            //task = taskRepository.CountAllTaskByTaskStatusCategoryId(Id, type);
            var searchViewModel = new TaskSearchViewModel
            {
                IsAttachments = false,
                IsAssignedToMe = false,
                IsCreatedByMe = false,
                UserID = CurrentLoggedInUser.UserID,
                CreatedByCompanyID = CurrentLoggedInUser.CreatedByCompanyID
            };

            var tasks = TaskRepository.QueryToCountTask(searchViewModel);
            task.TotalOpenTaskCount = tasks.Count(o => o.TaskStatusCategoryID == 1);
            task.TotalInProgressTaskCount = tasks.Count(o => o.TaskStatusCategoryID == 2);
            task.TotalCompletedTaskCount = tasks.Count(o => o.TaskStatusCategoryID == 3);
            task.TotalOverDueTaskCount = tasks.Count(o => o.TaskStatusCategoryID == 4);

            return Json(task, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns the view to Create new task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult Create()
        {
            Task task = new Task();
            ViewBag.PossibleTaskStatusCategories = _taskStatusCategoryRepository.All.ToList();
            task.ContactID = CurrentLoggedInContact.ContactID;
            ViewBag.CurrentUserID = CurrentLoggedInUser.UserID;
            return View(task);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load task list in my team schedule view with filtering options.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetAllTaskByTeamIdIdAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, TaskSearchViewModel taskSearchViewModel)
        {
            taskSearchViewModel.PageSize = dataSourceRequest.PageSize;
            taskSearchViewModel.Index = dataSourceRequest.Page;

            var result = TaskRepository.FilterTask(taskSearchViewModel, dataSourceRequest);

            foreach (UserTasksViewModel t in result.Data)
            {
                if (t.ContactID.HasValue)
                {
                    t.RefName = ContactRepository.RefererName(t.ContactID.Value).Name;
                }
            }
            var dsResutl = new DataSourceResult
            {
                Data = result.Data,
                Total = result.Total
            };
            return Json(dsResutl, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load list of tasks based on created by company id.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetAllTaskByCompanyIdIdAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, TaskSearchViewModel taskSearchViewModel)
        {
            var result = TaskRepository.FilterTask(taskSearchViewModel, dataSourceRequest);

            foreach (UserTasksViewModel t in result.Data)
            {
                if (t.ContactID.HasValue)
                {
                    t.RefName = ContactRepository.RefererName(t.ContactID.Value).Name;
                }
            }

            var dsResutl = new DataSourceResult
            {
                Data = result.Data,
                Total = result.Total
            };
            return Json(dsResutl, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns organizational task list view.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult ReturnOrganizationalTaskListViewAjax(TaskSearchViewModel taskSearchViewModel)
        {
            var orgTasks = new TaskMyTeamScheduleViewModel
            {
                TaskSearchViewModel = taskSearchViewModel
            };

            ViewBag.LoggedInUserID = CurrentLoggedInUser.UserID;
            return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_OrganizationTaskList.cshtml", orgTasks) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns my team task list view.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult ReturnTaskListViewAjax(TaskSearchViewModel taskSearchViewModel)
        {
            var myTasks = new TaskMyTeamScheduleViewModel();
            myTasks.TaskSearchViewModel = taskSearchViewModel;
            var dataSourceRequest = new DataSourceRequest();
            var result = TaskRepository.FilterTask(taskSearchViewModel, dataSourceRequest);
            ViewBag.ResultCount = result.Total;
            ViewBag.LoggedInUserID = CurrentLoggedInUser.UserID;
            return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_TaskTeamList.cshtml", myTasks) });
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method load list of tasks including category and display beside the calendar. This list of tasks are drag and droppable.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult DraggableTaskPreviewAjax(TaskSearchViewModel searchViewModel)
        {
            try
            {
                var taskList = TaskRepository.GetAllTaskForCalendarById(searchViewModel).ToList();

                foreach (var t in taskList)
                {
                    List<TaskTagCategory> taskTagCategory = null;
                    taskTagCategory = _taskTagCategoryRepository.FindTaskTagCategoryByTaskId(t.TaskID);
                    t.TagCategoryList = taskTagCategory;
                }

                return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_Tasks.cshtml", taskList) });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false });
            }
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method loads task list category and retuen view as json.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult TaskListByTaskCategoryAjax(TaskSearchViewModel searchViewModel)
        {
            try
            {
                var taskList = TaskRepository.GetAllTaskForCalendarById(searchViewModel).ToList();

                foreach (var t in taskList)
                {
                    List<TaskTagCategory> taskTagCategory = null;
                    taskTagCategory = _taskTagCategoryRepository.FindTaskTagCategoryByTaskId(t.TaskID);
                    t.TagCategoryList = taskTagCategory;
                }

                return Json(new { data = this.RenderPartialViewToString("~/Areas/TaskManagement/Views/Task/_TasksListCategory.cshtml", taskList) });
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false });
            }
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns workflow task list.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult GetAllWorkflowTaskListAjax([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            DataSourceResult result = TaskRepository.FindAllWorkflowTasks().ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Export Excel, CSV

        [UserAuthorize]
        [System.Web.Mvc.HttpPost]
        public ActionResult ExportTaskTeamExcelAjax(TaskSearchViewModel taskSearchViewModel)
        {
            try
            {
                var userTaskExportViewModelList = TaskRepository.FilterTaskForExport(taskSearchViewModel);

                if (userTaskExportViewModelList.Count > 0)
                {
                    string title = "TaskListExcel";

                    Session[title] = userTaskExportViewModelList;

                    string strUrl = "/TaskManagement/Task/ExportTaskTeamExcel?title=" + title;

                    return Json(new { success = true, url = strUrl, data = "Tasks export to excel successfully" });
                }
                else
                {
                    return Json(new { success = false, data = "Tasks not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ExceptionManager.Manage(ex) });
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpGet]
        public void ExportTaskTeamExcel(string title)
        {
            try
            {
                if (Session[title] != null)
                {
                    var userTaskExportViewModelList = (List<UserTaskExportViewModel>)Session[title];

                    string fileName = CurrentLoggedInUser.Name + " Task List";

                    ExportHelper.ExportToExcelFile(userTaskExportViewModelList, fileName);

                    Session[title] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpPost]
        public ActionResult ExportTaskTeamCSVAjax(TaskSearchViewModel taskSearchViewModel)
        {
            try
            {
                var userTaskExportViewModelList = TaskRepository.FilterTaskForExport(taskSearchViewModel);

                if (userTaskExportViewModelList.Count > 0)
                {
                    string title = "TaskListCSV";

                    Session[title] = userTaskExportViewModelList;

                    string strUrl = "/TaskManagement/Task/ExportTaskTeamCSV?title=" + title;

                    return Json(new { success = true, url = strUrl, data = "Tasks export to csv successfully" });
                }
                else
                {
                    return Json(new { success = false, data = "Tasks not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ExceptionManager.Manage(ex) });
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpGet]
        public void ExportTaskTeamCSV(string title)
        {
            try
            {
                if (Session[title] != null)
                {
                    var userTaskExportViewModelList = (List<UserTaskExportViewModel>)Session[title];

                    string fileName = CurrentLoggedInUser.Name + " Task List";

                    ExportHelper.ExportToCSVFile(userTaskExportViewModelList, fileName);

                    Session[title] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //OrganizationWeekly
        public ActionResult ExportTaskOrganizationWeeklyExcelAjax(TaskSearchViewModel taskSearchViewModel)
        {
            try
            {
                var userTaskExportViewModelList = TaskRepository.FilterTaskForExport(taskSearchViewModel);

                if (userTaskExportViewModelList.Count > 0)
                {
                    string title = "TaskOrganizationWeeklyListExcel";

                    Session[title] = userTaskExportViewModelList;

                    string strUrl = "/TaskManagement/Task/ExportTaskOrganizationWeeklyExcel?title=" + title;

                    return Json(new { success = true, url = strUrl, data = "Tasks export to excel successfully" });
                }
                else
                {
                    return Json(new { success = false, data = "Tasks not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ExceptionManager.Manage(ex) });
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpGet]
        public void ExportTaskOrganizationWeeklyExcel(string title)
        {
            try
            {
                if (Session[title] != null)
                {
                    var userTaskExportViewModelList = (List<UserTaskExportViewModel>)Session[title];

                    string fileName = CompanyRepository.Find(Convert.ToInt32(CurrentLoggedInUser.CreatedByCompanyID)).Name + " Task List";

                    ExportHelper.ExportToExcelFile(userTaskExportViewModelList, fileName);

                    Session[title] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpPost]
        public ActionResult ExportTaskOrganizationWeeklyCSVAjax(TaskSearchViewModel taskSearchViewModel)
        {
            try
            {
                var userTaskExportViewModelList = TaskRepository.FilterTaskForExport(taskSearchViewModel);

                if (userTaskExportViewModelList.Count > 0)
                {
                    string title = "TaskOrganizationWeeklyListCSV";

                    Session[title] = userTaskExportViewModelList;

                    string strUrl = "/TaskManagement/Task/ExportTaskOrganizationWeeklyCSV?title=" + title;

                    return Json(new { success = true, url = strUrl, data = "Tasks export to csv successfully" });
                }
                else
                {
                    return Json(new { success = false, data = "Tasks not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ExceptionManager.Manage(ex) });
            }
        }

        [UserAuthorize]
        [System.Web.Mvc.HttpGet]
        public void ExportTaskOrganizationWeeklyCSV(string title)
        {
            try
            {
                if (Session[title] != null)
                {
                    var userTaskExportViewModelList = (List<UserTaskExportViewModel>)Session[title];

                    string fileName = CompanyRepository.Find(Convert.ToInt32(CurrentLoggedInUser.CreatedByCompanyID)).Name + " Task List";

                    ExportHelper.ExportToCSVFile(userTaskExportViewModelList, fileName);

                    Session[title] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion Export Excel, CSV

        #endregion

        #region Workflow Task

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns the view to Create new workflow task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult CreateWorkflowTask()
        {
            Task task = new Task();
            ViewBag.PossibleTaskStatusCategories = _taskStatusCategoryRepository.All.ToList();
            return View(task);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method returns the view with workflow task list.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult WorkflowTaskList()
        {
            Task task = new Task();
            return View(task);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This is the post method to save new workflow task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult CreateWorkflowTask(Task task)
        {
            task.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
            try
            {
                if (ModelState.IsValid)
                {
                    task.DisplayId = MiscUtility.GetTaskPersonalizedId();
                    task.TaskStatusCategoryID = 1;
                    task.IsWorkflowTemplate = true;
                    TaskRepository.InsertOrUpdateWorkflowTask(task);
                    TaskRepository.Save();

                    return RedirectToAction(Constants.Views.WorkflowTaskList);
                }
                else
                {
                    foreach (var modelStateValue in ViewData.ModelState.Values)
                    {
                        foreach (var error in modelStateValue.Errors)
                        {
                            task.ErrorMessage = error.ErrorMessage;
                            break;
                        }
                        if (task.ErrorMessage.IsNotNullOrEmpty())
                        {
                            break;
                        }
                    }
                }
            }
            catch (CustomException ex)
            {
                task.ErrorMessage = ex.UserDefinedMessage;
            }
            catch (Exception ex)
            {
                ExceptionManager.Manage(ex);
                task.ErrorMessage = Constants.Messages.UnhandelledError;
            }
            return View(task);
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method return the workflow editor view through ajax call to add or update workflow.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult WorkflowEditorAjax(int taskId)
        {
            Task task = null;
            if (taskId > 0)
            {
                task = TaskRepository.Find(taskId);
                if (task == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Task not found");
                }
            }
            else
            {
                task = new Task();
            }
            return Content(this.RenderPartialViewToString(Constants.PartialViews.WorkflowEditorPopUp, task));
        }

        /// <summary>
        /// Functionalities Added
        /// - By Gopal C. Bala > This method save workflow task through ajax request and return json.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveWorkflowTaskAjax(Task task)
        {
            bool isNew = task.TaskID == 0;
            if (task.DisplayId.IsNullOrEmpty())
            {
                task.DisplayId = MiscUtility.GetTaskPersonalizedId();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                    task.TaskStatusCategoryID = 1;
                    task.IsWorkflowTemplate = true;
                    TaskRepository.InsertOrUpdateWorkflowTask(task);
                    TaskRepository.Save();
                    if (isNew)
                    {
                        task.SuccessMessage = "The Workflow Task has been added successfully";
                    }
                    else
                    {
                        task.SuccessMessage = "The Workflow Task has been updated successfully";
                    }
                }
                catch (CustomException ex)
                {
                    task.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    task.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        task.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (task.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (task.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, task) });
            }
        }

        #endregion
    }
}