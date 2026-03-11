using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TaskPriority>? TaskPriorities { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("UserId");

            modelBuilder.Entity<User>()
                .HasMany(u => u.TaskPriorities)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .IsRequired(false);

            modelBuilder.Entity<TaskPriority>()
                .HasOne(t => t.User)
                .WithMany(u => u.TaskPriorities)
                .HasForeignKey(t => t.UserId)
                .IsRequired(false);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TaskPriorityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
