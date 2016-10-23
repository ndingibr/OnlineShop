using System.Data.Entity;
using Model.Models;
using Model.Models.Mapping;

namespace Model
{
  public partial class OnlineShopContext : DbContext
  {
    static OnlineShopContext()
    {
      Database.SetInitializer<OnlineShopContext>(null);
    }

    public OnlineShopContext()
      : base("Name=OnlineShopContext")
    {
    }

    public virtual void Commit()
    {
      base.SaveChanges(); 
    }

    public DbSet<AspNetRole> AspNetRoles { get; set; }
    public DbSet<AspNetUser> AspNetUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new AspNetRoleMap());
      modelBuilder.Configurations.Add(new AspNetUserMap());
    }
  }
}
