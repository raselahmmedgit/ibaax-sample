using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.emailverify.ViewModels
{
    public class MandrilResultViewModel
    {
        public String ID { get; set; }
        public String Email { get; set; }
        public String Status { get; set; }
        public String SuccessMessage { get; set; }
        public String ErrorMessage { get; set; }
    }
}