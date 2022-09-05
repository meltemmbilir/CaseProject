using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoynerCaseProject.Model;

namespace BoynerCaseProject.Data
{
    public class BoynerCaseProjectContext : DbContext
    {
        public BoynerCaseProjectContext (DbContextOptions<BoynerCaseProjectContext> options)
            : base(options)
        {
        }

        public DbSet<BoynerCaseProject.Model.Category> Category { get; set; } = default!;

        public DbSet<BoynerCaseProject.Model.Product>? Product { get; set; }

        public DbSet<BoynerCaseProject.Model.Attribute>? Attribute { get; set; }

        public DbSet<BoynerCaseProject.Model.AttributeDetail>? AttributeDetail { get; set; }
    }
}
