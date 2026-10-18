using Microsoft.AspNetCore.Mvc;
using SprintFlowApp.Application.DTOs.Comments;
using SprintFlowApp.Application.Interfaces;

namespace SprintFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(CreateCommentDto dto)
    {
        var result = await _commentService.CreateAsync(dto);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll()
    {
        var result = await _commentService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommentDto>> GetById(Guid id)
    {
        var result = await _commentService.GetByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}

