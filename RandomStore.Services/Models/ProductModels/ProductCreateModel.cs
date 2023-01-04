namespace RandomStore.Services.Models.ProductModels
{
    public class ProductCreateModel
    {
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
