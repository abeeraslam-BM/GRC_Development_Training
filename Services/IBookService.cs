using Day1Task1.DTOs;

namespace Day1Task1.Services;

public interface IBookService
{
    Task<List<BookResponseDTO>> GetAllAsync(
        string? author,
        int page,
        int pageSize);

    Task<BookResponseDTO> GetByIdAsync(int id);

    Task<BookResponseDTO> CreateAsync(BookCreateDTO dto);

    Task<BookResponseDTO> UpdateAsync(int id, BookUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}