using CertificateRequests.Domain.Enums;

namespace CertificateRequests.Domain.Entities;

public class CertificateRequest
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;

    public CertificateType Type { get; set; }

    public int CopiesCount { get; set; }

    public string Reason { get; set; } = string.Empty;

    public RequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<RequestStatusHistory> StatusHistory { get; set; } = [];
}
