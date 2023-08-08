using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Order.Entity
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public int BookID { get; set; }
        public DateTime CreatedAt { get; set; }  = DateTime.Now;

        [NotMapped]
        public float OrderAmout { get; set; }
        [NotMapped]
        public UserEntity User { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }
    }
}
