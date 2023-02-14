using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
//using RestaurantWeb.Models;

namespace Restaurant.DataAccess
{
	public class ApplicationDbContext : IdentityDbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
	
		} 
		public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Product { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

		public DbSet<OrderHeader> OrderHeader { get; set; }
		public DbSet<OrderDetail> OrderDetail { get; set; }



	}
}
