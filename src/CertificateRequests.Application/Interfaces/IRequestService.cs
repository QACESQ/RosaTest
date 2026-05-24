using CertificateRequests.Application.DTOs;

namespace CertificateRequests.Application.Interfaces;

public interface IRequestService
{
    Task<Guid> CreateAsync(CreateRequestDto dto);

    Task<List<RequestDto>> GetEmployeeRequestsAsync(Guid employeeId);

    Task<List<RequestDto>> GetAllAsync();

    Task<RequestDetailsDto> GetByIdAsync(Guid id);

    Task UpdateStatusAsync(Guid id, UpdateStatusDto dto);
}