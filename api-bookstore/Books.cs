namespace api_bookstore;

public class Books
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string? genre { get; set; }
    public decimal price { get; set; }
    public int AvailableInStock { get; set; }
}
