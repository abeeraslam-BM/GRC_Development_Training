using Day1Task1.DTOs;
using Day1Task1.Models;

namespace Day1Task1.Mappers;

public static class BookMapper
{
    public static Book ToEntity(BookCreateDTO dto)
    {
        return new Book
        {
            Title = dto.Title,
            Year = dto.Year,
            PageCount = dto.PageCount,
            AuthorId = dto.AuthorId,
        };
    }

    public static void ApplyUpdate(Book book, BookUpdateDTO dto)
    {
        book.Title = dto.Title;
        book.Year = dto.Year;
        book.PageCount = dto.PageCount;
        book.AuthorId = dto.AuthorId;
    }

    public static BookResponseDTO ToResponse(Book book)
    {
        return new BookResponseDTO
        {
            Id = book.Id,
            Title = book.Title,
            Year = book.Year,
            PageCount = book.PageCount,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name ?? string.Empty
        };
    }
}