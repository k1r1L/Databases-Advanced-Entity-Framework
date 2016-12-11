namespace ProductsShopData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("CategoriesProducts").MapLeftKey("CategoryId").MapRightKey("ProductId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductsBought)
                .WithOptional(e => e.Seller)
                .HasForeignKey(e => e.BuyerId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductsBought)
                .WithRequired(e => e.Seller)
                .HasForeignKey(e => e.SellerId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Friends)
                .WithMany(e => e.Users)
                .Map(m => m.ToTable("UserFriends").MapLeftKey("FriendId").MapRightKey("UserId"));
        }
    }
}
