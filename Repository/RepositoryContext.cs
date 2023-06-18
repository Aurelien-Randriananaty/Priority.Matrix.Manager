using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class RepositoryContext : DbContext
	{
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TaskPriority>? TaskPriorities { get; set; }
        public DbSet<Category>? Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Entity<TaskPriority>().HasKey(x => x.Id);
        //    //modelBuilder.Entity<Category>().HasKey(x => x.Id);
        //}
    }
}
