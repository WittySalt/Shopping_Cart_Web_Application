using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
		{
			//Empty constructor
		}
		public DbSet<Product> Product { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<CartDetail> CartDetail { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<OrderDetail> OrderDetail { get; set; }
	}
}
