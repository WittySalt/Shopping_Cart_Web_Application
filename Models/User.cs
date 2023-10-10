using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
	[Table("User")]
	public class User
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool isAdmin {  get; set; }
	}
}
