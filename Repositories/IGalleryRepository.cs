using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
	public interface IGalleryRepository
	{
		Task<IEnumerable<Product>> GetProducts(string sTerm = "");
	}
}
