using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RnD.iBaaxData;
using RnD.iBaaxWebApi.Models.Repository;
using System.Net.Http;
using System.Net;
using System;

namespace RnD.iBaaxWebApi.Controllers
{
    //[Authorize]
    //[RoutePrefix("api/Account")]
    public class CategoryController : ApiController
    {

        ICategoryRepository _iCategoryRepository;

        public CategoryController()
        {
            _iCategoryRepository = new CategoryRepository(new AppDbContext());
        }
        
        //[AllowAnonymous]
        //[HttpPost]
        //public HttpResponseMessage GetAll()
        //{
        //    try
        //    {
        //        var categoryList = _iCategoryRepository.All.ToArray();
        //        return Request.CreateResponse(HttpStatusCode.OK, "OK");
        //    }

        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.Message));
        //    }
        //}

        // GET api/GetCategories
        public IEnumerable<Category> GetAllCategories()
        {
            //var categoryList = _db.Categories.ToList();
            //var categoryList = _iCategoryRepository.All.ToList();
            var categoryList = _iCategoryRepository.All.ToArray();

            return categoryList;
        }

        // GET api/GetCategories
        public IEnumerable<Category> GetCategories()
        {
            //var categoryList = _db.Categories.ToList();
            //var categoryList = _iCategoryRepository.All.ToList();
            var categoryList = _iCategoryRepository.All.ToArray();

            return categoryList;
        }

        // GET api/GetCategory/by id
        public Category GetCategory(int id)
        {
            //var category = _db.Categories.ToList().SingleOrDefault(x => x.CategoryId == id);
            var category = _iCategoryRepository.Find(id);

            return category;
        }

        // Add api/Add/by entity
        public void Add(Category model)
        {
            _iCategoryRepository.Insert(model);
            _iCategoryRepository.Save();
        }

        // Edit api/Edit/by entity
        public void Edit(Category model)
        {
            _iCategoryRepository.Update(model);
            _iCategoryRepository.Save();
        }

        // Delete api/Delete/by id
        public void Delete(int id)
        {
            var category = _iCategoryRepository.Find(id);
            _iCategoryRepository.Delete(category);
            _iCategoryRepository.Save();
        }
    }
}