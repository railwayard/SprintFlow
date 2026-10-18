using Microsoft.EntityFrameworkCore;
using SprintFlowApp.Application.DTOs.Comments;
using SprintFlowApp.Application.Interfaces;
using SprintFlowApp.Domain.Entities;
using SprintFlowApp.Infrastructure.Data;

namespace SprintFlowApp.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _dbContext;

    public CommentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CommentDto> CreateAsync(CreateCommentDto dto)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = dto.Content,
            TaskItemId = dto.TaskItemId,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync();

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskItemId = comment.TaskItemId,
            UserId = comment.UserId,
            CreatedAt = comment.CreatedAt
        };
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        return await _dbContext.Comments
            .Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                TaskItemId = c.TaskItemId,
                UserId = c.UserId,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<CommentDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Comments
            .Where(c => c.Id == id)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                TaskItemId = c.TaskItemId,
                UserId = c.UserId,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
}