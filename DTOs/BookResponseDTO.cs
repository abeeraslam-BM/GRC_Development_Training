namespace Day1Task1.DTOs;

public record BookResponseDTO
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public int AuthorId { get; init; }

    public string AuthorName { get; init; } = string.Empty;

    public int PageCount { get; init; }

    public int Year { get; init; }
}