namespace RandomStore.Services.Models.OrderDetailsModels
{
    public class OrderDetailsUpdateModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
