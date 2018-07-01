using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;

namespace TodoListProject.Models
{
    public class TodoViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }

        public string ApplicationUserId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Domain.Task> Tasks { get; set; }
        //public ICollection<SharingTodoWithFriend> SharingTodoWithFriends { get; set; }
    }
}