using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Order.Entity
{
    public class CartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,}$")]
        public int BookID { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,}$")]
        public int UserID { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,}$")]
        public int Quantity { get; set; }

        [NotMapped]
        public UserEntity User { get; set; }
        [NotMapped]
        public BookEntity book { get; set; }
    }
}
