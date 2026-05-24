using CertificateRequests.Domain.Enums;

namespace CertificateRequests.Application.DTOs;

public class CreateRequestDto
{
    public Guid EmployeeId { get; set; }

    public CertificateType Type { get; set; }

    public int CopiesCount { get; set; }

    public string Reason { get; set; } = string.Empty;
}