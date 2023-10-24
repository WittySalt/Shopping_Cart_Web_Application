using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Web_Application_V1._0.Data;
using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PurchaseHistoryRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }
        public async Task<string> GetUserId()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("username");
            var user = await _db.User
                    .Where(u => u.UserName == username)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                return user.Id.ToString();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Order>> GetUserOrderHistory(string sTerm = "")
        {
            var userId = await GetUserId();
            if (userId == null)
            {
                throw new Exception("Invalid UserId");
            }
            sTerm ??= string.Empty;
            sTerm = sTerm.ToLower();
            IEnumerable<Order> orders = await (from order in _db.Order
                                               where order.UserId == userId && 
                                               (string.IsNullOrWhiteSpace(sTerm) || order.OrderDetail.Any(od => od.Product.ProductName.ToLower().Contains(sTerm)))
                                               select order).Include(o => o.OrderDetail)
                                                            .ThenInclude(od => od.Product)
                                                            .Where(o => o.UserId == userId) // Include OrderDetail in the result
                                                            .ToListAsync();
            return orders.ToList();
        }
    }
}
