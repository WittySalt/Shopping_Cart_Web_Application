using System.ComponentModel.DataAnnotations.Schema;
//One Cart can have multiple products.
//We use CartDetail to record each of the products.
//This Model is used for each Cart to have different Items.
namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("CartDetail")]
	public class CartDetail
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public Product Product { get; set; }
		public Cart Cart { get; set; }
	}
}
