using System.ComponentModel;
using lab.ngsample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using lab.ngsample.Helpers;
using System.Security.Claims;

namespace lab.ngsample.Controllers
{
    public class BaseController : Controller
    {
        #region Global Variable Declaration

        #endregion Global Variable Declaration

        #region Constructor
        
        #endregion Constructor

        #region Action

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object objCurrentControllerName;
            this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
            object objCurrentActionName;
            this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
            if (this.RouteData.DataTokens.ContainsKey("area"))
            {
            }
            if (objCurrentActionName != null)
            {
                string currentActionName = objCurrentActionName.ToString();
            }
            if (objCurrentControllerName != null)
            {
                string currentControllerName = objCurrentControllerName.ToString();
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object objCurrentControllerName;
            this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
            object objCurrentActionName;
            this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
            string currentAreaName = string.Empty;
            if (this.RouteData.DataTokens.ContainsKey("area"))
            {
                currentAreaName = this.RouteData.DataTokens["area"].ToString();
            }
            if (objCurrentActionName != null)
            {
                string currentActionName = objCurrentActionName.ToString();
                if (objCurrentControllerName != null)
                {
                    string currentControllerName = objCurrentControllerName.ToString();

                    ViewBag.CurrentActionName = currentActionName;
                    ViewBag.CurrentControllerName = currentControllerName;
                }
            }
            ViewBag.CurrentAreaName = currentAreaName;

            string httpMethod = filterContext.HttpContext.Request.HttpMethod.ToLower();

            base.OnActionExecuted(filterContext);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            var cultureCookie = Request.Cookies["Culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                    ? Request.UserLanguages[0]
                    : null; // obtain it from HTTP header AcceptLanguages
            }

            // Validate culture name
            cultureName = "en"; // This is safe

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        #region Common

        public ActionResult UploadFiles()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    // Some browsers send file names with full path. This needs to be stripped.
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var fileName = Path.GetFileName(files[i].FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/Temporary"), fileName);
                        string uploadedFiles = Session[files.AllKeys[i] + "FileName"].ToString();
                        uploadedFiles = uploadedFiles + "|" + fileName;
                        Session.Add(files.AllKeys[i] + "FileName", uploadedFiles);

                        // The files are not actually saved in this demo
                        files[i].SaveAs(physicalPath);
                    }
                    return Content("");
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return null;
            // Return an empty string to signify success
        }

        public ActionResult RemoveFiles(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/Temporary"), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                    string uploadedFiles = Session["ResumesFileName"].ToString();
                    uploadedFiles = uploadedFiles.Replace(fileName, string.Empty);
                    Session.Add("ResumesFileName", uploadedFiles);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult UploadFile()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(files[0].FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/Temporary"), fileName);
                    Session.Add(files.AllKeys[0] + "FileName", fileName);
                    var extension = Path.GetExtension(fileName);
                    if (extension != null)
                    {
                        TempData["FileExtension"] = extension;
                    }
                    // The files are not actually saved in this demo
                    files[0].SaveAs(physicalPath);
                    return Content("");
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            return null;
            // Return an empty string to signify success
        }

        public ActionResult RemoveFile(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/Temporary"), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                    Session.Remove(fileName + "FileName");
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        protected static string GetDefaultValue(PropertyInfo prop)
        {
            var attributes = prop.GetCustomAttributes(typeof(DefaultValueAttribute), true);
            if (attributes.Length > 0)
            {
                var defaultAttr = (DefaultValueAttribute)attributes[0];
                return defaultAttr.Value.ToString();
            }

            // Attribute not found, fall back to default value for the type
            if (prop.PropertyType.IsValueType && !"Nullable`1".Equals(prop.PropertyType.Name))
                return Activator.CreateInstance(prop.PropertyType).ToString();

            return null;
        }

        protected static bool IsEmptyEntity<T>(T obj)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.GetValue(obj) != null)
                {
                    if (property.GetValue(obj).ToString() != GetDefaultValue(property))
                        return false;
                }
            }
            return true;
        }

        #endregion Common

        public User CurrentLoggedInUser
        {
            get
            {
                User user = (User)Session["LoggedInUser"];
                if (user == null)
                {
                    int userId = 0;
                    string emailAddress = string.Empty;
                    if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                    {
                        var identity = (ClaimsIdentity)User.Identity;
                        IEnumerable<Claim> claims = identity.Claims;
                        foreach (Claim claim in claims)
                        {
                            if (claim.Type.Contains("identity/claims/sid"))
                            {
                                userId = Convert.ToInt32(claim.Value);
                                break;
                            }
                            else if (claim.Type.Contains("identity/claims/emailaddress"))
                            {
                                emailAddress = claim.Value;
                            }
                        }
                    }
                    if (userId > 0 || !String.IsNullOrEmpty(emailAddress))
                    {
                        if (UserRepository == null)
                        {
                            UserRepository = DependencyResolver.Current.GetService(typeof(IUserRepository)) as IUserRepository;
                        }
                        if (userId > 0)
                        {
                            user = UserRepository.Find(userId);
                        }
                        else if (!String.IsNullOrEmpty(emailAddress))
                        {
                            user = UserRepository.Find(emailAddress);
                        }
                        Session["LoggedInUser"] = user;
                    }
                    if (user == null)
                    {
                        user = new User();
                    }
                }
                return user;
            }
        }

        #region TaskManagement

        [HttpPost]
        public ActionResult UploadTaskFile()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    if (Constants.ValidFileExtension._validTaskFileExtension.Contains(Path.GetExtension(files[0].FileName)))
                    {
                        // Some browsers send file names with full path. This needs to be stripped.
                        var fileName = Path.GetFileName(files[0].FileName);
                        var physicalPath = Path.Combine(Server.MapPath(Constants.Paths.TemporaryFileUploadPath), fileName);
                        var sessionName = Session[files.AllKeys[0] + "FileName"] ?? String.Empty;
                        sessionName = String.Format("{0}___{1}", fileName, sessionName);
                        Session[files.AllKeys[0] + "FileName"] = sessionName;

                        // The files are not actually saved in this demo
                        files[0].SaveAs(physicalPath);
                    }
                }
                return Content("");
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex);
            }
            Response.StatusCode = 400;
            return Json("Unsuccessful", JsonRequestBehavior.DenyGet);
            // Return an empty string to signify success
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowTaskDocument(int? id, string documentFileName = null)
        {
            string fileBasePath = Constants.Paths.DownloadFilePath + "Company/" + id.ToString() + "/";
            string filePath = string.Empty;
            if (documentFileName.IsNotNullOrEmpty())
            {
                string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + documentFileName;
                if (System.IO.File.Exists(physicalPath))
                {
                    filePath = fileBasePath + documentFileName;
                }
            }
            if (filePath.IsNullOrEmpty() && id.HasValue)
            {
                if (TaskDocumentRepository == null)
                {
                    TaskDocumentRepository = DependencyResolver.Current.GetService(typeof(ITaskDocumentRepository)) as ITaskDocumentRepository;
                }
                TaskDocument taskDocument = TaskDocumentRepository.Find(id.Value);
                if (taskDocument != null && taskDocument.DocumentFileName.IsNotNullOrEmpty())
                {
                    documentFileName = taskDocument.DocumentFileName;
                    string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + documentFileName;
                    if (!System.IO.File.Exists(physicalPath))
                    {
                        Utility.WriteFile(physicalPath, taskDocument.Document);
                    }
                    filePath = fileBasePath + documentFileName;
                }
            }
            if (filePath.IsNullOrEmpty())
            {
                //filePath = Url.Content("~/assets1/img/no-photo.jpg");
                //documentFileName = "no-photo.jpg";
            }
            ImageResult result = new ImageResult(filePath, Utility.GetMIMEType(documentFileName));
            return result;
        }

        public string ProcessTaskEmailTemplate(EmailTemplateViewModel emailTemplateViewModel, string baseUrl, string serverPath)
        {
            _taskMethodDictionary = new Dictionary<string, string>();
            _taskMethodDictionary = GetTaskTemplateMethodDictionary();

            var type = typeof(TaskFunctions);
            var taskFunctionsInstance = Activator.CreateInstance(type, new object[] { emailTemplateViewModel, baseUrl, serverPath });

            #region Task Information

            var infoMatch = Regex.Matches(emailTemplateViewModel.BodyHtml, @"\[([A-Za-z0-9\-]+)]", RegexOptions.IgnoreCase);
            foreach (var o in infoMatch)
            {
                var originalString = o.ToString();
                var x = o.ToString();
                x = x.Replace("[", "");
                x = x.Replace("]", "");
                x = _taskMethodDictionary[x];

                var toInvoke = type.GetMethod(x);
                var result = toInvoke.Invoke(taskFunctionsInstance, null);
                emailTemplateViewModel.BodyHtml = emailTemplateViewModel.BodyHtml.Replace(originalString, result.ToString());
            }

            #endregion Task Information

            return emailTemplateViewModel.BodyHtml;
        }

        private static Dictionary<string, string> GetTaskTemplateMethodDictionary()
        {
            var dic = new Dictionary<string, string>{
                                                            {"TaskTitle", "TaskTitle"},
                                                            {"TaskDescription", "TaskDescription"},
                                                            {"TaskStartDate", "TaskStartDate"},
                                                            {"TaskEndDate", "TaskEndDate"},
                                                            {"TaskPriority", "TaskPriority"},
                                                            {"TaskStatus", "TaskStatus"},
                                                            {"TaskProgress", "TaskProgress"},
                                                            {"TaskTags", "TaskTags"},
                                                            {"CreateDate", "CreateDate"},
                                                            {"DueDateTime", "DueDateTime"},
                                                            {"TaskDashboardUrl", "TaskDashboardUrl"},
                                                            {"TaskDashboardCommentUrl", "TaskDashboardCommentUrl"},
                                                            {"TaskComments", "TaskComments"},
                                                            {"Year", "Year"},
                                                            {"CreateTime","CreateTime"},
                                                            {"CreatorPhoto","CreatorPhoto"},
                                                            {"CreatorName","CreatorName"}
                                                        };
            return dic;
        }

        #endregion TaskManagement

        #endregion Action
    }
}