using CertificateRequests.Application.DTOs;
using CertificateRequests.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CertificateRequests.API.Controllers;

[ApiController]
[Route("api/accountant/requests")]
public class AccountantController(IRequestService requestService) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var requests = await requestService.GetAllAsync(cancellationToken);

        return Ok(requests);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = await requestService.GetByIdAsync(id, cancellationToken);

        return Ok(request);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateStatusDto dto,
        CancellationToken cancellationToken)
    {
        await requestService.UpdateStatusAsync(id, dto, cancellationToken);

        return NoContent();
    }
}