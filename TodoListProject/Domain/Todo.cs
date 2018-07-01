using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TodoListProject.Models;

namespace TodoListProject.Domain
{
    public class Todo : EntityBase
    {
        public Todo()
        {
            //ApplicationUsers = new HashSet<ApplicationUser>(ApplicationUsers);
            isCompleted = false;
        }
        [Display(Name ="Başlık")]
        [Required(ErrorMessage ="Bu alan boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public bool isCompleted { get; set; }


        //Mapping
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Domain.Task> Tasks { get; set; }
        public virtual ICollection<SharingTodoWithFriend> SharingTodoWithFriends { get; set; }
    }
}