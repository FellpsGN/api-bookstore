using Microsoft.AspNetCore.Mvc;
using api_bookstore.Communication.Requests;

namespace api_bookstore.Controllers;
public class BookController : ApiBookStoreController
{
    private static List<Books> DataBaseBooks = new List<Books>
    {
        new Books { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", genre = "Programming", price = 49.99m, AvailableInStock = 5 },
        new Books { Id = 2, Title = "The Pragmatic Programmer", Author = "Andy Hunt", genre = "Programming", price = 39.99m, AvailableInStock = 3 }
    };

    [HttpGet]
    [ProducesResponseType(typeof(List<Books>), StatusCodes.Status200OK)]
    public IActionResult GetAllBooks() 
    {
        return Ok(DataBaseBooks);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Books), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetBookById(int id)
    {
        var book = DataBaseBooks.FirstOrDefault(bookElement => bookElement.Id == id);
        if (book == null)
        {
            return NotFound($"Não foi possível encontrar o livro com o ID {id}");
        }
        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateNewBook([FromBody] RequestRegisterBookJson request)
    {
        var lastElementId = (DataBaseBooks.Count - 1);
        var lastId = (DataBaseBooks[lastElementId].Id + 1);
        
        var newBook = new Books {
            Id = lastId,
            Title = request.Title,
            Author = request.Author,
            genre = request.genre,
            price = request.price,
            AvailableInStock = request.AvailableInStock
        };

        DataBaseBooks.Add(newBook);

        return Created(string.Empty, newBook);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult UpdateBookById([FromRoute] int id, [FromBody] RequestUpdateBookJson request)
    {

        var bookIndex = DataBaseBooks.FindIndex(book => book.Id == id);
        if (bookIndex == -1)
        {
            return NotFound($"Não foi possível encontrar o livro com o ID {id}");
        }

        DataBaseBooks[bookIndex] = new Books
        {
            Id = id,
            Title = request.Title ?? DataBaseBooks[bookIndex].Title,
            Author = request.Author ?? DataBaseBooks[bookIndex].Author,
            genre = request.genre ?? DataBaseBooks[bookIndex].genre,
            price = request.price != 0 ? request.price : DataBaseBooks[bookIndex].price,
            AvailableInStock = request.AvailableInStock != 0 ? request.AvailableInStock : DataBaseBooks[bookIndex].AvailableInStock
        };
        return NoContent();

    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult DeleteBookById(int id)
    {
        var IsBookExists = DataBaseBooks.Any(idBookElement => idBookElement.Id == id);
        if (IsBookExists)
        {
            var book = DataBaseBooks.FirstOrDefault(idBookElement => idBookElement.Id == id);
            DataBaseBooks.Remove(book);
            return NoContent();
        }
        return NotFound($"Não foi possível encontrar o livro com o ID {id}");

    }
}
