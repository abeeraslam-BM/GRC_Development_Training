using Microsoft.AspNetCore.Mvc;
using Day1Task1.Services;

namespace Day1Task1.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllAsync();

        var response = books.Select(b => new
        {
            b.Id,
            b.Title,
            b.Year,
            b.PageCount,
            AuthorName = b.Author.Name
        });

        return Ok(response);
    }


 [HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var book = await _bookService.GetByIdAsync(id);

    var response = new
    {
        book.Id,
        book.Title,
        book.Year,
        book.PageCount,
        AuthorName = book.Author.Name
    };

    return Ok(response);
}
      }
