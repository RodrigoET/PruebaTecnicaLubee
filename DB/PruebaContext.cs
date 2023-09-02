using Microsoft.EntityFrameworkCore;
using Model;
using System.Diagnostics.Contracts;

namespace DB
{
    public class PruebaContext: DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
        {
            
        }

        public DbSet<Model.Contract> Contracts { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemXContract> ItemXContract { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}