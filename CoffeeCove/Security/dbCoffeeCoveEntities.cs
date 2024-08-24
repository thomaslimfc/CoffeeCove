using System;
using System.Data.Entity;

namespace CoffeeCove.Securities // Adjust namespace based on your folder structure
{
    public partial class dbCoffeeCoveEntities : DbContext, IDisposable
    {
        public dbCoffeeCoveEntities()
            : base("name=dbCoffeeCoveEntities")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        // Add other DbSet properties for your entities here

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize entity mappings here if needed
        }
    }
}
