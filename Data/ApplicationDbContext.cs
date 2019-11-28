using System;
using System.Collections.Generic;
using System.Text;
using Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    public class ApplicationUser: IdentityUser<string>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class ApplicationRole: IdentityRole<string>
    {
        public string RoleDescription { get; set; }
    }
}
