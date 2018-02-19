using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApplication.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("[A-Za-z_ ]+", ErrorMessage = "Name should contain letters only")]
        public string Name { get; set; }
    }
}