using System;
using System.Web.Mvc;
using lab.ngsample.Controllers;

namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskTagCategoryController : BaseController
    {
        #region Variable Declaration

        private readonly ITaskTagCategoryRepository _taskTagCategoryRepository;

        #endregion

        #region Constructor

        public TaskTagCategoryController(IAppMenuRepository appmenuRepository, IUserActivityRepository userActivityRepository, IUserEmailCommunicationRepository userEmailCommunicationRepository, ITaskRepository taskRepository, ITaskTagCategoryRepository taskTagCategoryRepository)
            : base(appmenuRepository, userActivityRepository, userEmailCommunicationRepository, taskRepository)
        {
            this._taskTagCategoryRepository = taskTagCategoryRepository;
        }

        #endregion

        #region Task Tag List

        [UserAuthorize]
        public ActionResult LoadTaskTagAjax()
        {
            var result = _taskTagCategoryRepository.FindAllTaskTagCategory();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Editor, Save & Delete

        [UserAuthorize]
        public ActionResult EditorAjax(int id)
        {
            TaskTagCategory taskTagCategory = null;
            if (id > 0)
            {
                taskTagCategory = _taskTagCategoryRepository.Find(id);
                if (taskTagCategory == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Task Tag Category not found");
                }
            }
            else
            {
                taskTagCategory = new TaskTagCategory();
            }

            return Content(this.RenderPartialViewToString(Constants.PartialViews.EditorPopUp, taskTagCategory));
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAjax(TaskTagCategory taskTagCategory)
        {
            bool isNew = taskTagCategory.TaskTagCategoryID == 0;
            if (ModelState.IsValid)
            {
                try
                {
                    taskTagCategory.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                    _taskTagCategoryRepository.InsertOrUpdate(taskTagCategory);
                    _taskTagCategoryRepository.Save();
                    if (isNew)
                    {
                        taskTagCategory.SuccessMessage = "Task tag category has been added successfully";
                    }
                    else
                    {
                        taskTagCategory.SuccessMessage = "Task tag category has been updated successfully";
                    }
                }
                catch (CustomException ex)
                {
                    taskTagCategory.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    taskTagCategory.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        taskTagCategory.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (taskTagCategory.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (taskTagCategory.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskTagCategory) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskTagCategory) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            TaskTagCategory taskTagCategory = _taskTagCategoryRepository.Find(id);
            if (taskTagCategory == null)
            {
                taskTagCategory = new TaskTagCategory();
                taskTagCategory.ErrorMessage = "Task Tag Category not found";
            }
            else
            {
                try
                {
                    _taskTagCategoryRepository.Delete(taskTagCategory);
                    _taskTagCategoryRepository.Save();
                    taskTagCategory.SuccessMessage = "Task Tag Category has been deleted successfully";
                }
                catch (CustomException ex)
                {
                    taskTagCategory.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    taskTagCategory.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            if (taskTagCategory.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskTagCategory) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskTagCategory) });
            }
        }

        #endregion
    }
}

