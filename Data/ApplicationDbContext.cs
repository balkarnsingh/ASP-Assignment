using System;
using System.Collections.Generic;
using System.Text;
using Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetCoreAssignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
