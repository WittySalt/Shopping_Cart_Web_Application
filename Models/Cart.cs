using System.ComponentModel.DataAnnotations.Schema;
//The selected products of User will be added into the Cart.
//This Model is used for each User to have a unique Cart.
namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("Cart")]
	public class Cart
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual ICollection<CartDetail> CartDetail { get; set; }
	}
}
