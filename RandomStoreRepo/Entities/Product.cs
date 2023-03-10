namespace RandomStoreRepo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
