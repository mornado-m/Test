using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class IndexViewModel
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public string ErrorMessage { get; set; } = "";
    }
}