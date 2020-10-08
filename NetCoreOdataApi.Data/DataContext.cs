using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Core.Models;
using NetCoreOdataApi.Core.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Data
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
        public DbSet<ApplicationUser> ApplicationUsers  {get;set;}
        public DbSet<Category> Categories { get; set; }   
        public DbSet<Product> Products { get; set; }
        //quiz angular project
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
