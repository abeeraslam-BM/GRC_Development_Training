using System.ComponentModel.DataAnnotations;

namespace Day1Task1.DTOs;

public class BookUpdateDTO
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int AuthorId { get; set; }

    [Range(1, 10000)]
    public int PageCount { get; set; }

    public int Year { get; set; }
}