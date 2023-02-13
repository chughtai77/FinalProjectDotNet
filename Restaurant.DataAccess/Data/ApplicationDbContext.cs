
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
//using RestaurantWeb.Models;

namespace Restaurant.DataAccess
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
	
		} 
		public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Product { get; set; }

    }
}
