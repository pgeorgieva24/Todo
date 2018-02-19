using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApplication.DataAccess;
using ToDoApplication.Models;
using ToDoApplication.ViewModels.Todo;

namespace ToDoApplication.Controllers
{
    public class TodoController : Controller
    {
        private TodoRepository repository;

        public TodoController()
        {
            repository = new TodoRepository(ConfigurationManager.ConnectionStrings["ToDoApp"].ToString());
        }
        
        // GET: /Todo/
        public ActionResult Index(int categoryId)
        {
            List<Todo> model = repository.GetByCategoryId(categoryId);
            ViewData["categoryId"] = categoryId;
            return View(model);
        }

        public ActionResult Create(int categoryId)
        {
            TodoCreateViewModel model = new TodoCreateViewModel();
            
            model.CategoryId= categoryId;

            return View(model);
            //return View();
        }

        [HttpPost]
        public ActionResult Create(TodoCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Todo todo = new Todo();
            todo.CategoryId = model.CategoryId;
            todo.Id = model.Id;
            todo.IsDone = model.IsDone;
            todo.Title = model.Title;
            repository.Insert(todo);
            return RedirectToAction("Index", new { categoryId = todo.CategoryId});
        }
        public ActionResult Edit(int id)
        {
            Todo todo = repository.Get(id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            TodoEditViewModel model = new TodoEditViewModel();
            model.CategoryId = todo.CategoryId;
            model.Id = todo.Id;
            model.IsDone = todo.IsDone;
            model.Title = todo.Title;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(TodoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Todo todo = new Todo();
            todo.CategoryId = model.CategoryId;
            todo.Id = model.Id;
            todo.IsDone = model.IsDone;
            todo.Title = model.Title;

            if (todo == null)
            {
                return HttpNotFound();
            }

            repository.Update(todo);
            return RedirectToAction("Index", new { categoryId = todo.CategoryId });
        }
        public ActionResult Delete(int id)
        {
            Todo todo = repository.Get(id);
            if (todo == null)
            {
                return HttpNotFound();
            }

            repository.Delete(id);

            return RedirectToAction("Index", new { categoryId = todo.CategoryId });
        }
	}

}