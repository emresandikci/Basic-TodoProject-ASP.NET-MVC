using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Models;

namespace TodoListProject.Domain
{
    public class SharingTodoWithFriend : EntityBase
    {
        public string TodoId { get; set; }
        public string UserId { get; set; }

        //Mapping
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Todo Todo { get; set; }
    }
}