using EasySoft.Helper;
using Core.Business.Helpers;
using Core.Business.Repositories;
using Core.Common;
using RB.Controllers;
using Core.Data.Models;
using Core.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RB;

namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskManagementBaseController : BaseController
    {
        
        #region Global Variable Declaration

        #endregion

        #region Constructor
        public TaskManagementBaseController(IAppMenuRepository appMenuRepository
            ,IUserActivityRepository userActivityRepository
            ,IUserEmailCommunicationRepository userEmailCommunicationRepository
            ,ITaskRepository taskRepository)
            : base(appMenuRepository
            ,userActivityRepository
            ,userEmailCommunicationRepository
            ,taskRepository)
        {
        }

        #endregion

        #region Actions
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object objCurrentControllerName = string.Empty;
            this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
            object objCurrentActionName = string.Empty;
            this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
            string currentAreaName = string.Empty;
            if (this.RouteData.DataTokens.ContainsKey("area"))
            {
                currentAreaName = this.RouteData.DataTokens["area"].ToString();
            }
            string currentActionName = objCurrentActionName.ToString(true).ToLower();
            string currentControllerName = objCurrentControllerName.ToString(true).ToLower();
            currentAreaName = currentAreaName.ToLower();

            ViewBag.CurrentActionName = currentActionName;
            ViewBag.CurrentControllerName = currentControllerName;
            ViewBag.CurrentAreaName = currentAreaName;
            int loggedInUserID = 0;
            if (CurrentLoggedInUser != null)
            {
                loggedInUserID = CurrentLoggedInUser.UserID;
                ViewBag.CurrentUserID = loggedInUserID;
                ViewBag.CurrentUserRoles = CurrentLoggedInUser.UserRoles;
                ViewBag.CurrentUserName = CurrentLoggedInUser.FirstName + " " + CurrentLoggedInUser.LastName;
                ViewBag.CurrentUserPhotoFileName = CurrentLoggedInUser.PhotoFileName;
            }
            if (CurrentLoggedInContact != null)
            {
                ViewBag.CurrentUserContactID = CurrentLoggedInContact.ContactID;
                ViewBag.CurrentUserCompanyID = CurrentLoggedInContact.CompanyID;
            }
            currentActionName = currentActionName.ToLower();
            if (!currentActionName.Contains("ajax") && !currentActionName.Contains("icon") && !currentActionName.Contains("logo") && !currentActionName.Contains("photo") && !currentActionName.Contains("uploadfile") && !currentActionName.Contains("removefile"))
            {
                if (filterContext.ActionParameters != null && filterContext.ActionParameters.Count > 0 && (filterContext.ActionParameters.ContainsKey("taskId") || Request.QueryString["taskId"].IsNotNullOrEmpty()))
                {
                    int taskId = 0;
                    if (filterContext.ActionParameters.ContainsKey("taskId"))
                    {
                        taskId = filterContext.ActionParameters["taskId"].ToInteger(true);
                    }
                    if (taskId == 0)
                    {
                        taskId = Request.QueryString["taskId"].ToInteger(true);
                    }
                    ViewBag.TaskId = taskId;
                    if (TaskRepository == null)
                    {
                        TaskRepository = DependencyResolver.Current.GetService(typeof(ITaskRepository)) as ITaskRepository;
                    }
                }
            }

            HeaderViewModel newHeaderViewModel = WebHelper.CurrentSession.Content.HeaderContent;
            if (newHeaderViewModel == null)
            {
                newHeaderViewModel = new HeaderViewModel();
                if (UserActivityRepository != null)
                {
                    newHeaderViewModel.ListNotification = UserActivityRepository.FindAllNew();
                    if (newHeaderViewModel.ListNotification != null)
                    {
                        newHeaderViewModel.CountNotificatioin = newHeaderViewModel.ListNotification.Count;
                    }
                }
                if (TaskRepository != null)
                {
                    Task searchParameter = new Task()
                    {
                        CreatedByUserID = loggedInUserID,
                        TaskStatusCategoryID = 2
                    };
                    if (newHeaderViewModel.ListTask != null)
                    {
                        newHeaderViewModel.CountTask = newHeaderViewModel.ListTask.Count;
                    }
                }
                if (UserEmailCommunicationRepository != null)
                {
                    newHeaderViewModel.ListMessage = UserEmailCommunicationRepository.FindAllNew(CurrentLoggedInContact.ContactID);
                    if (newHeaderViewModel.ListMessage != null)
                    {
                        newHeaderViewModel.CountMessage = newHeaderViewModel.ListMessage.Count;
                    }
                }
                WebHelper.CurrentSession.Content.HeaderContent = newHeaderViewModel;
            }
            ViewBag.HeaderData = newHeaderViewModel;
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected void LoadTaskDashboardCommonData(int taskId, ITaskRepository taskRepository, Task task)
        {
            if (taskRepository == null)
            {
                taskRepository = DependencyResolver.Current.GetService(typeof(ITaskRepository)) as ITaskRepository;
            }
            if (taskRepository != null)
            {
                object objCurrentControllerName = string.Empty;
                this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
                object objCurrentActionName = string.Empty;
                this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
                string currentAreaName = string.Empty;
                if (this.RouteData.DataTokens.ContainsKey("area"))
                {
                    currentAreaName = this.RouteData.DataTokens["area"].ToString();
                }
                string currentActionName = objCurrentActionName.ToString(true).ToLower();
                string currentControllerName = objCurrentControllerName.ToString(true).ToLower();
                currentAreaName = currentAreaName.ToLower();
                ViewBag.TaskLandingMenu = AppMenuRepository.BuildMenuByCategory(Convert.ToInt32(AppMenuCategories.TaskDashboard), CurrentLoggedInUser.UserID, currentActionName, currentControllerName, currentAreaName);
                WebHelper.CurrentSession.Content.IsMenuLoaded = false;
            }
            ViewBag.TaskId = taskId;
        }

        #endregion
    }
}
