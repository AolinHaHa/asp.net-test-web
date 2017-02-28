using assignment4.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CodeFirst.Helpers;

namespace assignment4.Models
{
    public class Assignment4Context : IdentityDbContext<ApplicationUser>
    {
        public Assignment4Context()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<Assignment4Context>(new MembershipDatabaseInitializer());
        }

        public virtual DbSet<Product> Products { get; set; }
        public static Assignment4Context Create()
        {
            return new Assignment4Context();
        }

    }
}