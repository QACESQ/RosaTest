using CertificateRequests.Domain.Enums;

namespace CertificateRequests.Application.DTOs;

public class UpdateStatusDto
{
    public RequestStatus Status { get; set; }
}