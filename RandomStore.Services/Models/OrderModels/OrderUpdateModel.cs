namespace RandomStore.Services.Models.OrderModels
{
    public class OrderUpdateModel
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
    }
}
