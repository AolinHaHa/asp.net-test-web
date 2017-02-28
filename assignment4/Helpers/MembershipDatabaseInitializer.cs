using System;
using System.Globalization;
using System.Linq;
using System.Data.Entity;
using assignment4.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using assignment4.Helpers;



namespace CodeFirst.Helpers
{
    public class MembershipDatabaseInitializer : DropCreateDatabaseIfModelChanges<Assignment4Context>
    {
        protected override void Seed(Assignment4Context context)
        {

            GetRoles().ForEach(c => context.Roles.Add(c));
            context.SaveChanges();
            assignment4PasswordHasher hasher = new assignment4PasswordHasher();
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", PasswordHash = hasher.HashPassword("admin") };
            var role = context.Roles.Where(r => r.Name == "Admin").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            context.SaveChanges();
            base.Seed(context);
        }

        private static List<ApplicationRole> GetRoles()
        {
            var roles = new List<ApplicationRole> {
               new ApplicationRole {Name="Admin", Description="Admin"},
                new ApplicationRole {Name="Member", Description="Member"}
            };

            return roles;
        }
    }
}