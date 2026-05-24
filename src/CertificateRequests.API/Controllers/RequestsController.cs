using CertificateRequests.Application.DTOs;
using CertificateRequests.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CertificateRequests.API.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestsController(IRequestService requestService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequestDto dto,
        CancellationToken cancellationToken)
    {
        var id = await requestService.CreateAsync(dto, cancellationToken);

        return Created($"/api/requests/{id}", new
        {
            id
        });
    }

    [HttpGet("/api/employees/{employeeId:guid}/requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeRequests(
        Guid employeeId,
        CancellationToken cancellationToken)
    {
        var requests =
            await requestService.GetEmployeeRequestsAsync(employeeId, cancellationToken);

        return Ok(requests);
    }
}