using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagmentCQRS.CommandModel
{
    public class ProductAddUpdateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId {  get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Quantity { get; set; }

        [Required]
        [Range(1,100000)]
        public float Price { get; set; }
        
        public DateTime CreatedAt { get; set; } 
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } 
    }
}
