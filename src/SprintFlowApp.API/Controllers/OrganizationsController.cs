using Microsoft.AspNetCore.Mvc;
using SprintFlowApp.Application.DTOs.Organizations;
using SprintFlowApp.Application.Interfaces;

namespace SprintFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
private readonly IOrganizationService _organizationService;

public OrganizationsController(
    IOrganizationService organizationService)
{
    _organizationService = organizationService;
}

// POST: api/Organizations
[HttpPost]
public async Task<ActionResult<OrganizationDto>> Create(
    [FromBody] CreateOrganizationDto dto)
{
    var organization =
        await _organizationService.CreateAsync(dto);

    return CreatedAtAction(
        nameof(GetById),
        new { id = organization.Id },
        organization);
}

// GET: api/Organizations
[HttpGet]
public async Task<ActionResult<List<OrganizationDto>>> GetAll()
{
    var organizations =
        await _organizationService.GetAllAsync();

    return Ok(organizations);
}

// GET: api/Organizations/{id}
[HttpGet("{id:guid}")]
public async Task<ActionResult<OrganizationDto>> GetById(Guid id)
{
    var organization =
        await _organizationService.GetByIdAsync(id);

    if (organization is null)
    {
        return NotFound();
    }

    return Ok(organization);
}

// PUT: api/Organizations/{id}
[HttpPut("{id:guid}")]
public async Task<ActionResult<OrganizationDto>> Update(
    Guid id,
    [FromBody] UpdateOrganizationDto dto)
{
    var organization =
        await _organizationService.UpdateAsync(id, dto);

    if (organization is null)
    {
        return NotFound();
    }

    return Ok(organization);
}

// DELETE: api/Organizations/{id}
[HttpDelete("{id:guid}")]
public async Task<IActionResult> Delete(Guid id)
{
    var deleted =
        await _organizationService.DeleteAsync(id);

    if (!deleted)
    {
        return NotFound();
    }

    return NoContent();
}

}

// POST now returns 201 Created, with a location pointing to the new organization.

// PUT returns 200 OK when updated, or 404 Not Found when the ID doesn't exist.

// DELETE returns 204 No Content on success, or 404 Not Found for a missing organization.

// [FromBody] explicitly identifies the request DTO as coming from the JSON body.