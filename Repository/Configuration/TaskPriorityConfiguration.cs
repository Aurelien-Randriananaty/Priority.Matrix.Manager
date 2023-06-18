using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    internal class TaskPriorityConfiguration : IEntityTypeConfiguration<TaskPriority>
    {
        public void Configure(EntityTypeBuilder<TaskPriority> builder)
        {
            builder.HasData
                (
                    new TaskPriority
                    {
                        Id = 1,
                        TaskTitre = "Title task 1",
                        TaskDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.",
                        TaskStatus = "To do",
                        TaskCreatedBy = 1,
                        CategoryID = 1,
                        CreatedDate = DateTime.Today,
                        TaskToSee = DateTime.Today,
                        Hour = 1
                    },
                    new TaskPriority
                    {
                        Id= 2,
                        TaskTitre = "Title task 2",
                        TaskDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.",
                        TaskStatus = "To do",
                        TaskCreatedBy = 1,
                        CategoryID = 2,
                        CreatedDate = DateTime.Today,
                        TaskToSee = DateTime.Today,
                        Hour = 2
                    },
                    new TaskPriority
                    {
                        Id= 3,
                        TaskTitre = "Title task 3",
                        TaskDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.",
                        TaskStatus = "To do",
                        TaskCreatedBy = 2,
                        CategoryID = 2,
                        CreatedDate = DateTime.Today,
                        TaskToSee = DateTime.Today,
                        Hour = 3
                    },
                    new TaskPriority
                    {
                        Id = 4,
                        TaskTitre = "Title task 4",
                        TaskDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.",
                        TaskStatus = "To do",
                        TaskCreatedBy = 2,
                        CategoryID = 3,
                        CreatedDate = DateTime.Today,
                        TaskToSee = DateTime.Today,  
                        Hour = 2
                    },
                    new TaskPriority
                    {
                        Id = 5,
                        TaskTitre = "Title task 5",
                        TaskDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.",
                        TaskStatus = "To do",
                        TaskCreatedBy = 2,
                        CategoryID = 4,
                        CreatedDate = DateTime.Today,
                        TaskToSee = DateTime.Today,
                        Hour = 2
                    }
                );
        }
    }
}
