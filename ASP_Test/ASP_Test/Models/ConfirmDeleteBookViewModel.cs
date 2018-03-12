using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class ConfirmDeleteBookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ErrorMessage { get; set; } = "";
    }
}