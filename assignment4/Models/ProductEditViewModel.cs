using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assignment4.Models
{
    public class ProductEditViewModel
    {
        public String Name { get; set; }
        public EditableProduct Product { get; set; }
        public EditableProduct Payable { get; set; }
        public String StatusMessage { get; set; }
        public bool IsErrorStatusMessage { get; set; }
    }
}