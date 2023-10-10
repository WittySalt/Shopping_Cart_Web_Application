using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("Product")]
	public class Product
	{
		public int Id { get; set; }
		[Required]
		public string? ProductName {  get; set; }
		public string? ProductDescription { get; set; }
		public string? Image {  get; set; }
		[Required]
		public double Price { get; set; }
		public Guid ActivationCode { get; set; }

	}
}
