namespace Shopping_Cart_Web_Application_V1._0.Models.DTOs
{
    public class PurchaseHistoryViewModel
    {
        public IEnumerable<Order>? Order { get; set; }
        public string? STerm { get; set; }
    }
}
