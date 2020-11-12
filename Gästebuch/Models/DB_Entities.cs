using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace Gästebuch.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("Gästebuch") { }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("tbl_Admin");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }
}