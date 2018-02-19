using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApplication.ViewModels.Category
{
    public class CategoryIndexViewModel
    {
        public DateTime CurrentDateTime { get; set; }
        public List<Models.Category> Categories { get; set; }
    }
}