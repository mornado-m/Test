using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class SalesViewModel
    {
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public string ErrorMessage { get; set; } = "";
    }
}