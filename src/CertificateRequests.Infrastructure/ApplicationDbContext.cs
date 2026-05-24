using CertificateRequests.Application.Interfaces;
using CertificateRequests.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificateRequests.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<CertificateRequest> CertificateRequests => Set<CertificateRequest>();

    public DbSet<RequestStatusHistory> RequestStatusHistories => Set<RequestStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
