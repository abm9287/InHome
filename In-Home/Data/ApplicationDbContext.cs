using System;
using System.Collections.Generic;
using System.Text;
using In_Home.Areas.Users.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace In_Home.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TUsers> TUsers { get; set; }
        public DbSet<TClients> TClients { get; set; }
        public DbSet<TReports_clients> TReports_clients { get; set; }
    }
}
