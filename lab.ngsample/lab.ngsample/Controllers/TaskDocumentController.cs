using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using lab.ngsample.Controllers;
namespace RB.Areas.TaskManagement.Controllers
{
    public class TaskDocumentController : BaseController
    {
        #region Variable Declaration

        private readonly ITaskDocumentRepository _taskdocumentRepository;

        #endregion

        #region Constructor

        public TaskDocumentController(IAppMenuRepository appmenuRepository, IUserActivityRepository userActivityRepository, IUserEmailCommunicationRepository userEmailCommunicationRepository, ITaskRepository taskRepository, IUserRepository userRepository, ICompanyRepository companyRepository, ITaskDocumentRepository taskdocumentRepository)
            : base(appmenuRepository, userActivityRepository, userEmailCommunicationRepository, taskRepository)
        {
            this.UserRepository = userRepository;
            this.CompanyRepository = companyRepository;
            this.TaskRepository = taskRepository;
            _taskdocumentRepository = taskdocumentRepository;
        }

        #endregion

        #region Task Document List

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
            DataSourceResult result = _taskdocumentRepository.AllIncluding(taskId.ToInteger(true), taskdocument => taskdocument.LastUpdatedByUser).Select(taskdocument => new { taskdocument.CreateDate, taskdocument.LastUpdateDate, taskdocument.LastUpdatedByUserID, taskdocument.CreatedByCompanyID, CreatedByUserName = taskdocument.CreatedByUser.FirstName + " " + taskdocument.CreatedByUser.LastName, taskdocument.Name, taskdocument.DocumentFileName, taskdocument.DocumentFileSize, taskdocument.TaskID, taskdocument.TaskDocumentID }).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        public ActionResult GetTaskDocumentByTaskIdAjaxAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int taskId)
        {
            DataSourceResult result = _taskdocumentRepository.FindAllTaskDocumentByTaskId(taskId).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        public ActionResult GetAllTaskContactDocumentByTaskIdAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int taskId)
        {
            DataSourceResult result = _taskdocumentRepository.FindAllTaskContactDocumentByTaskId(taskId).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        public ActionResult GetAllTaskCompanyDocumentByTaskIdAjax([DataSourceRequest] DataSourceRequest dataSourceRequest, int taskId)
        {
            DataSourceResult result = _taskdocumentRepository.FindAllTaskCompanyDocumentByTaskId(taskId).ToDataSourceResult(dataSourceRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Task Document Save & Delete

        [UserAuthorize]
        [HttpPost]
        public ActionResult SaveAjax(TaskDocument taskdocument)
        {
            bool isNew = taskdocument.TaskDocumentID == 0;

            if (ModelState.IsValid)
            {
                if (Session["TaskFileName"] != null)
                {
                    var fileName = Session["TaskFileName"].ToString();
                    var physicalPath = Path.Combine(Server.MapPath(Constants.Paths.TemporaryFileUploadPath), fileName);
                    taskdocument.Document = Utility.ReadFile(physicalPath);
                    taskdocument.DocumentFileName = fileName;
                    taskdocument.DocumentFileSize = taskdocument.Document.LongLength;
                    try
                    {
                        System.IO.File.Delete(physicalPath);
                        Session["TaskFileName"] = null;
                    }
                    catch { }
                }
                else if (taskdocument.DocumentFileName.IsNotNullOrEmpty())
                {
                    taskdocument.Document = (byte[])Session["Document"];
                }
                else
                {
                    taskdocument.Document = null;
                    taskdocument.DocumentFileName = null;
                    taskdocument.DocumentFileSize = null;
                }

                try
                {
                    if (taskdocument.Document != null)
                    {
                        taskdocument.LastUpdatedByUserID = CurrentLoggedInUser.UserID;
                        _taskdocumentRepository.InsertOrUpdate(taskdocument);
                        _taskdocumentRepository.Save();
                        if (isNew)
                        {
                            taskdocument.SuccessMessage = "Task Document has been added successfully";
                        }
                        else
                        {
                            taskdocument.SuccessMessage = "Task Document has been updated successfully";
                        }
                    }
                    else
                    {
                        taskdocument.ErrorMessage = "Select another new Task Document to upload.";
                    }
                }
                catch (CustomException ex)
                {
                    taskdocument.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    taskdocument.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            else
            {
                foreach (var modelStateValue in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        taskdocument.ErrorMessage = error.ErrorMessage;
                        break;
                    }
                    if (taskdocument.ErrorMessage.IsNotNullOrEmpty())
                    {
                        break;
                    }
                }
            }
            if (taskdocument.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskdocument) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskdocument) });
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            TaskDocument taskdocument = _taskdocumentRepository.Find(id);
            if (taskdocument == null)
            {
                taskdocument = new TaskDocument();
                taskdocument.ErrorMessage = "TaskDocument not found";
            }
            else
            {
                try
                {
                    _taskdocumentRepository.Delete(taskdocument);
                    _taskdocumentRepository.Save();
                    taskdocument.SuccessMessage = "Task Document has been deleted successfully";
                }
                catch (CustomException ex)
                {
                    taskdocument.ErrorMessage = ex.UserDefinedMessage;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Manage(ex);
                    taskdocument.ErrorMessage = Constants.Messages.UnhandelledError;
                }
            }
            if (taskdocument.ErrorMessage.IsNotNullOrEmpty())
            {
                return Json(new { success = false, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskdocument) });
            }
            else
            {
                return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.Alert, taskdocument) });
            }
        }

        [UserAuthorize]
        public ActionResult RemoveDocumentAjax(int id)
        {
            TaskDocument taskdocument = null;
            if (id > 0)
            {
                taskdocument = _taskdocumentRepository.Find(id);
                if (taskdocument == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Task Document not found");
                }
            }
            if (taskdocument != null)
            {
                //Delete cached file
                string fileBasePath = Constants.Paths.DownloadFilePath + "TaskDocument/" + id.ToString() + "/";
                string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + taskdocument.DocumentFileName;
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }

                taskdocument.Document = null;
                taskdocument.DocumentFileName = string.Empty;
                taskdocument.DocumentFileSize = null;
                Session["Document"] = null;
            }
            ViewBag.PossibleCreatedByCompanies = CompanyRepository.All;
            return Json(new { success = true, data = this.RenderPartialViewToString(Constants.PartialViews.EditorPopUp, taskdocument) });
        }

        #endregion

        #region Document Download

        [UserAuthorize]
        public ActionResult DownloadDocument(int id)
        {
            TaskDocument taskdocument = null;
            if (id > 0)
            {
                taskdocument = _taskdocumentRepository.Find(id);
                if (taskdocument == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Task Document not found");
                }
            }
            return File(taskdocument.Document, Utility.GetMIMEType(taskdocument.DocumentFileName), taskdocument.DocumentFileName);
        }

        [UserAuthorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowDocument(int id, string DocumentFileName = null)
        {
            string fileBasePath = Constants.Paths.DownloadFilePath + "TaskDocument/" + id.ToString() + "/";
            if (DocumentFileName.IsNotNullOrEmpty())
            {
                string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + DocumentFileName;
                if (System.IO.File.Exists(physicalPath))
                {
                    string filePath = fileBasePath + DocumentFileName;
                    ImageResult result = new ImageResult(filePath, Utility.GetMIMEType(DocumentFileName));
                    return result;
                }
            }
            ITaskDocumentRepository repository = DependencyResolver.Current.GetService(typeof(ITaskDocumentRepository)) as ITaskDocumentRepository;
            TaskDocument taskdocument = repository.Find(id);
            if (taskdocument.DocumentFileName.IsNotNullOrEmpty())
            {
                DocumentFileName = taskdocument.DocumentFileName;
                string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + DocumentFileName;
                if (!System.IO.File.Exists(physicalPath))
                {
                    Utility.WriteFile(physicalPath, taskdocument.Document);
                }
                string filePath = fileBasePath + DocumentFileName;
                //ImageResult result = new ImageResult(taskdocument.Document, Utility.GetMIMEType(taskdocument.DocumentFileName));
                ImageResult result = new ImageResult(filePath, Utility.GetMIMEType(DocumentFileName));
                return result;
            }
            return null;
        }

        [UserAuthorize]
        public ActionResult DownloadTaskDocument(int id)
        {
            TaskDocument taskDocument = new TaskDocument();
            if (id > 0)
            {
                taskDocument = _taskdocumentRepository.Find(id);
                if (taskDocument == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Document not found");
                }
            }
            return File(taskDocument.Document, Utility.GetMIMEType(taskDocument.DocumentFileName), taskDocument.DocumentFileName);
        }

        #endregion
    }
}

