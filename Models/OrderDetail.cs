using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		public int Id { get; set; }
		public int OrderId {  get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public virtual Order Order { get; set; }
		public virtual ICollection<Product> Product { get; set; }
	}
}
