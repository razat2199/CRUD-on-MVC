using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }        

        public DbSet<Employee> Employees{ get; set; }

        internal object Find(int? id)
        {
            throw new NotImplementedException();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Employee && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; 

                if (entity.State == EntityState.Added)
                {
                    ((Employee)entity.Entity).CreatedAt = now;
                }
                ((Employee)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
