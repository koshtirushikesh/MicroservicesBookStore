
namespace BookStore.Order
{
    public class BookEntity
    {
        public int BookID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public float discountedprice { get; set; }
        public float actualprice { get; set;}
    }
}
