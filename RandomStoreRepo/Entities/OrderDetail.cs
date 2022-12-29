using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RandomStoreRepo.Entities
{
    public class OrderDetail
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetails")]
        public Product Product { get; set; }
    }
}
