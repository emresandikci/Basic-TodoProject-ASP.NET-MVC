using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoListProject.Domain;
using TodoListProject.Repositories;

namespace TodoListProject.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoRepo _db;
        private readonly SharingTodoRepo _dbShare;

        public TodoController()
        {
            _db = Tools.TodoSingleton.getTodoInstance();
            _dbShare = Tools.TodoSingleton.getShareingTodoInstance();
        }

        public ActionResult List()
        {
            var list = _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new Todo
            {
                ApplicationUserId = User.Identity.GetUserId()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Todo item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                //item.ApplicationUserId = User.Identity.GetUserId();
                _db.Create(item);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
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
                if (!currentItem.SharingTodoWithFriends.Where(x => x.UserId == User.Identity.GetUserId()).Any())
                {
                    if (currentItem.ApplicationUserId == User.Identity.GetUserId())
                    {
                        return View(currentItem);
                    }
                    return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
                }
                return View(currentItem);
            }
            catch (Exception ex)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Todo item)
        {
            try
            {
                item.UpdatedDate = DateTime.Now;
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

        public ActionResult DeleteFriendFromTodo(string Id, string todoId)
        {
            _dbShare.Delete(Id);
            return View("Update", _db.getByItemId(todoId));
        }

        public ActionResult TodoTasks(string Id)
        {
            var result = _db.getByItemId(Id).Tasks.ToList();
            if (result == null)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            return View(result);
        }

        public ActionResult ShareWithFriend(string Id)
        {
            return View(new SharingTodoWithFriend()
            {
                TodoId = Id
            });
        }
        [HttpPost]
        public ActionResult ShareWithFriend(SharingTodoWithFriend item)
        {
            try
            {
                _dbShare.Create(item);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }

        public ActionResult SharedWithMeUpdate(string Id)
        {
            try
            {
                var currentItem = _db.getByItemId(Id);
                if (!currentItem.SharingTodoWithFriends.Where(x => x.UserId == User.Identity.GetUserId()).Any())
                {
                    if (currentItem.ApplicationUserId == User.Identity.GetUserId())
                    {
                        return View(currentItem);
                    }
                    return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
                }
                return View(currentItem);
            }
            catch (Exception ex)
            {
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SharedWithMeUpdate(Todo item)
        {
            try
            {
                item.UpdatedDate = DateTime.Now;
                _db.Update(item);
                return View("List", _db.getAll().Where(x => x.ApplicationUserId == User.Identity.GetUserId()).ToList());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "işlem yapılırken bir hata oluştu - Hata:" + ex.Message);
                return View(item);
            }
        }
    }
}