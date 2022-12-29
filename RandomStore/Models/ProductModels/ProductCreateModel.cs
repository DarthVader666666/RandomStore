namespace RandomStore.Application.Models.ProductModels
{
    public class ProductCreateModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; } = "";
    }
}
