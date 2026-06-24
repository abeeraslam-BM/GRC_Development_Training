using Day1Task1.Database;
using Day1Task1.DTOs;
using Day1Task1.Exceptions;
using Day1Task1.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Day1Task1.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _db;

    public BookService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<BookResponseDTO>> GetAllAsync(
        string? author,
        int page,
        int pageSize)
    {
        var query = _db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b =>
                b.Author.Name.Contains(author));
        }

        var books = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return books
            .Select(BookMapper.ToResponse)
            .ToList();
    }

    public async Task<BookResponseDTO> GetByIdAsync(int id)
    {
        var book = await _db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            throw new BookNotFoundException(id);

        return BookMapper.ToResponse(book);
    }

    public async Task<BookResponseDTO> CreateAsync(BookCreateDTO dto)
    {
        var book = BookMapper.ToEntity(dto);

        _db.Books.Add(book);

        await _db.SaveChangesAsync();

        var createdBook = await _db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .FirstAsync(b => b.Id == book.Id);

        return BookMapper.ToResponse(createdBook);
    }

    public async Task<BookResponseDTO> UpdateAsync(int id, BookUpdateDTO dto)
    {
        var book = await _db.Books
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            throw new BookNotFoundException(id);

        BookMapper.ApplyUpdate(book, dto);

        await _db.SaveChangesAsync();

        var updatedBook = await _db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .FirstAsync(b => b.Id == id);

        return BookMapper.ToResponse(updatedBook);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _db.Books
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return false;

        _db.Books.Remove(book);

        await _db.SaveChangesAsync();

        return true;
    }
}