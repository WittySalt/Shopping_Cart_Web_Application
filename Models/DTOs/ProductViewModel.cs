namespace Shopping_Cart_Web_Application_V1._0.Models.DTOs
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string STerm { get; set; } = "";
    }
}
