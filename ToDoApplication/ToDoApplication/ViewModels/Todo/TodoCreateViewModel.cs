using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApplication.ViewModels.Todo
{
    public class TodoCreateViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [RegularExpression("[A-Za-z_ ]+", ErrorMessage = "Name should contain letters only")]
        public string Title { get; set; }
        public bool IsDone { get; set; }
        
    }
}