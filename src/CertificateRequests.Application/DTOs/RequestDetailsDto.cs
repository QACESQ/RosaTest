using CertificateRequests.Domain.Enums;

namespace CertificateRequests.Application.DTOs;

public class RequestDetailsDto
{
    public Guid Id { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public CertificateType Type { get; set; }

    public int CopiesCount { get; set; }

    public string Reason { get; set; } = string.Empty;

    public RequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}