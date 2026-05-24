using CertificateRequests.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateRequests.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }

    DbSet<CertificateRequest> CertificateRequests { get; }

    DbSet<RequestStatusHistory> RequestStatusHistories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}