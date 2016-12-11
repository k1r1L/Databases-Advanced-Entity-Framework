namespace ProductsShop.Data
{
    using Models;
    using System.Data.Entity;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(uf => 
                {
                    uf.MapLeftKey("UserId");
                    uf.MapRightKey("FriendId");
                    uf.ToTable("UserFriends");
                });

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .Map(cp =>
                {
                    cp.MapLeftKey("CategoryId");
                    cp.MapRightKey("ProductId");
                    cp.ToTable("CategoriesProducts");
                });

        }
    }
}