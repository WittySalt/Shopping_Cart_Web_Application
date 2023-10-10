using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("Cart")]
	public class Cart
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int ProductId {  get; set; }
		public int Quantity {  get; set; }
		public double UnitPrice { get; set; }
		public virtual ICollection<Product> Product { get; set; }
		public virtual ICollection<Order> Order { get; set; }
	}
}
