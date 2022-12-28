using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RandomStoreRepo.Entities
{
    class OrderDetail
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
    }
}
