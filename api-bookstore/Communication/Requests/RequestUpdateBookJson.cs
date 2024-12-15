namespace api_bookstore.Communication.Requests;

public class RequestUpdateBookJson
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string genre { get; set; }
    public decimal price { get; set; }
    public int AvailableInStock { get; set; }
}
