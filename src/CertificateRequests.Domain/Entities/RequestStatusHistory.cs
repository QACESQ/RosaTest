

using CertificateRequests.Domain.Enums;

namespace CertificateRequests.Domain.Entities;

public class RequestStatusHistory
{
    public Guid Id { get; set; }

    public Guid RequestId { get; set; }

    public CertificateRequest Request { get; set; } = null!;

    public RequestStatus OldStatus { get; set; }

    public RequestStatus NewStatus { get; set; }

    public DateTime ChangedAt { get; set; }
}
