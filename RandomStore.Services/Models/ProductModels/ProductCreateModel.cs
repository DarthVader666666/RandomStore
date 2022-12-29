using System.ComponentModel.DataAnnotations;

namespace RandomStore.Services.Models.ProductModels
{
    public class ProductCreateModel
    {
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
    }
}
