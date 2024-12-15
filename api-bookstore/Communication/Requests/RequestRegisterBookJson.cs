namespace api_bookstore.Communication.Requests;

public class RequestRegisterBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string genre { get; set; }
    public decimal price { get; set; }
    public int AvailableInStock { get; set; }
}
