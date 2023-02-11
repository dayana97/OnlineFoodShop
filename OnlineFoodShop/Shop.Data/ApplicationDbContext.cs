using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using OnlineFoodShop.Shop.Data.Models;


namespace OnlineFoodShop.Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodProducts>()
                .HasOne(f =>f.Category)
                .WithMany(f => f.FoodProducts)
                .isRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCardItem>()
                .HasOne(sci => sci.FoodProducts);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(user => user.Email)
                //trqbva da napravim taka che da ne mogat da se registrat s edin
                //email nqkolko puti
                .IsUnique(true);
        }

        public DbSet<FoodProducts>  FoodProducts { get; set; }
        public DbSet<ShoppingCardItem> ShoppingCardItems { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


    }
}
