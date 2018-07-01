using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Models;
using TodoListProject.Repositories;

namespace TodoListProject.Tools
{
    public class TodoSingleton
    {
        private static ApplicationDbContext _instance = null;
        private static TodoRepo _todoInstance = null;
        private static TaskRepo _taskInstance = null;
        private static FriendRepo _friendInstance = null;
        private static SharingTodoRepo _sharingTodoInstance = null;

        public static ApplicationDbContext getInstance()
        {
            if (_instance == null)
                _instance = new ApplicationDbContext();

            return _instance;
        }

        public static TodoRepo getTodoInstance()
        {
            if (_todoInstance == null)
                _todoInstance = new TodoRepo();

            return _todoInstance;
        }
        public static TaskRepo getTaskInstance()
        {
            if (_taskInstance == null)
                _taskInstance = new TaskRepo();

            return _taskInstance;
        }
        public static FriendRepo getfriendInstance()
        {
            if (_friendInstance == null)
                _friendInstance = new FriendRepo();

            return _friendInstance;
        }
        public static SharingTodoRepo getShareingTodoInstance()
        {
            if (_sharingTodoInstance == null)
                _sharingTodoInstance = new SharingTodoRepo();

            return _sharingTodoInstance;
        }
    }
}