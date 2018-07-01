using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Models;

namespace TodoListProject.Domain
{
    public class Friend : EntityBase
    {
        public string friendId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //Mapping
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}