using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoListProject.Repositories;

namespace TodoListProject.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskRepo _db;
        public TaskController()
        {
            _db = Tools.TodoSingleton.getTaskInstance();
        }

        public ActionResult List()
        {
            var list = _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList();
            return View(list);
        }

        public ActionResult Create(string todoId)
        {
            return View(new Domain.Task { TodoId = todoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Domain.Task item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                item.ApplicationUserId = User.Identity.GetUserId();
                _db.Create(item);
                return RedirectToAction("List", "Todo");
                //return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "işlem yapılırken bir hata oluştu - Hata:" + ex.Message);
                return View(item);
            }
        }

        public ActionResult Update(string Id)
        {
            try
            {
                var currentItem = _db.getByItemId(Id);
                return View(currentItem);
            }
            catch (Exception ex)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Domain.Task item)
        {
            try
            {
                _db.Update(item);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "işlem yapılırken bir hata oluştu - Hata:" + ex.Message);
                return View(item);
            }
        }

        public ActionResult Delete(string Id)
        {
            try
            {
                _db.Delete(Id);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception ex)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }

        public ActionResult CompleteToTask(string Id)
        {
            try
            {
                _db.CompleteIt(Id);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception ex)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }
    }
}