using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("Order")]
	public class Order
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; }
		public virtual ICollection<OrderDetail> OrderDetail { get; set; }
	}
}
