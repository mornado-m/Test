using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_Test.Models
{
    public class AddBookViewModel
    {
        public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
        public string ErrorMessage { get; set; } = "";
    }
}