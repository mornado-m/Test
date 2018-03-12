using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class AuthorsViewModel
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public string ErrorMessage { get; set; } = "";
    }
}