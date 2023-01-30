
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Models;

namespace RestaurantWeb.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
	
		}
		public DbSet<Category> Categories { get; set; }
	}
}
