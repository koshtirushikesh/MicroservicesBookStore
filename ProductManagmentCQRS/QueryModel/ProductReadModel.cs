namespace ProductManagmentCQRS.QueryModel
{
    public class ProductReadModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
