using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace assignment4.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        public String ApplicationUserId { get; set; }
        public String Payable { get; set; }
        public DateTime AddedDate { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}