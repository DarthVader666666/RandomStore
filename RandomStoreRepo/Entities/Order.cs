namespace RandomStoreRepo.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
