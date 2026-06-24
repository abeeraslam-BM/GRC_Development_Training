using Day1Task1.DTOs;
using Day1Task1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Day1Task1.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetAll(
        string? author,
        int page = 1,
        int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(author, page, pageSize);

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookCreateDTO dto)
    {
        var createdBook = await _bookService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdBook.Id },
            createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookUpdateDTO dto)
    {
        var updatedBook = await _bookService.UpdateAsync(id, dto);

        return Ok(updatedBook);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}