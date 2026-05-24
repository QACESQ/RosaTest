using CertificateRequests.Application.DTOs;

namespace CertificateRequests.Application.Interfaces;

public interface IRequestService
{
    Task<Guid> CreateAsync(CreateRequestDto dto, CancellationToken cancellationToken = default);

    Task<List<RequestDto>> GetEmployeeRequestsAsync(Guid employeeId, CancellationToken cancellationToken = default);

    Task<List<RequestDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<RequestDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(Guid id, UpdateStatusDto dto, CancellationToken cancellationToken = default);
}