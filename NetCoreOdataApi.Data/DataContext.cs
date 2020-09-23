using Microsoft.EntityFrameworkCore;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Core.Models;
using NetCoreOdataApi.Core.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }   
        public DbSet<Product> Products { get; set; }
        //quiz angular project
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
