namespace CertificateRequests.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public List<CertificateRequest> Requests { get; set; } = [];
}
