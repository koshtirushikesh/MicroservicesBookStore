namespace ProductManagmentCQRS.CommandModel
{
    public class ResponseModel<T>
    {
        public bool status {  get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
