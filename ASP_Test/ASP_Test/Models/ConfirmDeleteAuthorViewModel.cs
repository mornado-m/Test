using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class ConfirmDeleteAuthorViewModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string ErrorMessage { get; set; } = "";
    }
}