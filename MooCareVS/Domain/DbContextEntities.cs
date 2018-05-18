using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;

namespace Domain
{
    public class DbContextEntities:DbContext
    {
        public DbContextEntities(): base("dbContextEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Cow> Cows { get; set; }
        public DbSet<Lactation> Lactations { get; set; }
        public DbSet<Yield> Yields { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Food> Foods { get; set; }
    }
}
