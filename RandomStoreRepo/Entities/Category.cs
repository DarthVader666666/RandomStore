using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RandomStoreRepo.Entities
{
    public class Category
    {
        public Category() 
        {
            this.Products= new List<Product>();
        }

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[]? Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
