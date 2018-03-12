using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Test.Models
{
    public class ConfirmBuyViewModel
    {
        public string BookName { get; set; }
        public Sale Sale { get; set; }
        public string ErrorMessage { get; set; }
    }
}