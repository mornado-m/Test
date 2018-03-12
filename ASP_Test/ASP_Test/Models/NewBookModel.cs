using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class NewBookModel
    {
        public string book_name { get; set; }
        public string author_name { get; set; }
        public int available_count { get; set; }
        public double price { get; set; }
        public string description { get; set; }
    }
}