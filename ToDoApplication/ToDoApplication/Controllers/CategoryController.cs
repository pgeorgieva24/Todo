using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ToDoApplication.DataAccess;
using ToDoApplication.Models;
using ToDoApplication.ViewModels.Category;

namespace ToDoApplication.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository repository;

        public CategoryController()
        {
            repository = new CategoryRepository(ConfigurationManager.ConnectionStrings["ToDoApp"].ToString());
        }
        //
        // GET: /Category/
        public ActionResult Index()
        {
            CategoryIndexViewModel model = new CategoryIndexViewModel();
                                                  
            model.CurrentDateTime = DateTime.Now;
            model.Categories = repository.GetAll();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            Category category = new Category();
            category.Id = model.Id;
            category.Name = model.Name;

            repository.Insert(category);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            CategoryEditViewModel model = new CategoryEditViewModel();
            Category category = repository.Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            model.Id = category.Id;
            model.Name = category.Name;            

            return View(model);            
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Category category = new Category();
            category.Id = model.Id;
            category.Name = model.Name;

            if (category == null)
            {
                return HttpNotFound();
            }

            repository.Update(category);

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Category category = repository.Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            repository.Delete(id);

            return RedirectToAction("Index");
        }
	}
}