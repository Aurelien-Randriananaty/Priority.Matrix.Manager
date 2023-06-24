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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category
                    {
                        Id=1,
                        CategoryName = "Important et urgent",
                        CategoryCode = "important-et-urgent",
                    },
                     new Category
                     {
                         Id = 2,
                         CategoryName = "Important, mais pas urgent",
                         CategoryCode = "important-mais-pas-urgent ",
                     },
                    new Category
                    {
                        Id=3,
                        CategoryName = "Important, mais pas urgent",
                        CategoryCode = "pas-important-mais-urgent",
                    },
                    new Category
                    {
                        Id =4,
                        CategoryName = "Ni important, ni urgent",
                        CategoryCode = "Ni-important-ni-urgent",
                    }
                );
        }
    }
}
