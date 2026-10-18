using SprintFlowApp.Application.DTOs.Comments;

namespace SprintFlowApp.Application.Interfaces;

public interface ICommentService
{
    Task<CommentDto> CreateAsync(CreateCommentDto dto);

    Task<List<CommentDto>> GetAllAsync();

    Task<CommentDto?> GetByIdAsync(Guid id);
}