using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TodoListProject.Models;

namespace TodoListProject.Domain
{
    public class Task:EntityBase
    {
        public Task()
        {
            isCompleted = false;
        }
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public DateTime? startingDate { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public DateTime? expirationDate { get; set; }
        public bool isCompleted { get; set; }

        public string ApplicationUserId { get; set; }
        public string TodoId { get; set; }

        //Mapping
        public virtual Todo Todo { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}