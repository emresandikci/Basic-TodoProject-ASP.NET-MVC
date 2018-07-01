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
    public class FriendController : Controller
    {
        private readonly FriendRepo _db;

        public FriendController()
        {
            _db = Tools.TodoSingleton.getfriendInstance();
        }

        public ActionResult List()
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _db.getAll().Where(x => x.ApplicationUserId == currentUserId).ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string keyword)
        {
            var result = _db.findFriendByEmail(keyword).Where(x=>x.Id!=User.Identity.GetUserId()).ToList();

            if (!result.Any())
            {
                ViewBag.ErrorMessage = "Bu kriterlere göre kullanıcı bulunamadı";
            }

            return View(result);
        }

        public ActionResult Delete(string Id)
        {
            _db.DeleteFriend(Id, User.Identity.GetUserId());
            return View("List", _db.getAll());
        }

        public ActionResult Add(string Id)
        {

            var result = new Friend()
            {
                friendId = Id,
                ApplicationUserId = User.Identity.GetUserId(),
                UserName = Tools.TodoSingleton.getInstance().Users.Find(Id).UserName,
                Email = Tools.TodoSingleton.getInstance().Users.Find(Id).Email
            };
            result.ApplicationUserId = User.Identity.GetUserId();
            _db.Create(result);
            return View("List", _db.getAll());
        }

    }
}