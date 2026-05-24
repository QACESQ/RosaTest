using CertificateRequests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificateRequests.Infrastructure.Configurations;

public class RequestStatusHistoryConfiguration : IEntityTypeConfiguration<RequestStatusHistory>
{
    public void Configure(EntityTypeBuilder<RequestStatusHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OldStatus)
            .IsRequired();

        builder.Property(x => x.NewStatus)
            .IsRequired();

        builder.Property(x => x.ChangedAt)
            .IsRequired();
    }
}
