namespace RandomStore.Services.Models.OrderDetailModels
{
    public class OrderDetailCreateModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
