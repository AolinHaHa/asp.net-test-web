using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assignment4.Models
{
    public class ProductViewModel
    {
        public List<EditableProduct> Products { get; set; }
        public Boolean CanAdd { get; set; }
        public bool IsErrorStatusMessage { get; set; }
        public string StatusMessage { get; set; }
    }
}