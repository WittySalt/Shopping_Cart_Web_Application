using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
    public interface IPurchaseHistoryRepository
    {
        Task<List<Order>> GetUserOrderHistory(string sTerm = "");
    }
}
