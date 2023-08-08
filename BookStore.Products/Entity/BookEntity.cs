using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Products.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        [Range(1,100000)]
        public int Quantity { get; set; }

        [Required]
        [Range(1.0,1000.0)]
        public float discountedprice { get; set; }

        [Required]
        [Range(1.0, 100000.0)]
        public float actualprice { get; set;}
    }
}
