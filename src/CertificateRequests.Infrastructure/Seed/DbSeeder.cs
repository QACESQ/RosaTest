using CertificateRequests.Domain.Entities;

namespace CertificateRequests.Infrastructure.Persistence.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (dbContext.Employees.Any())
        {
            return;
        }

        var employees = new List<Employee>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FullName = "Ivan Ivanov"
            },

            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FullName = "Petr Petrov"
            }
        };

        dbContext.Employees.AddRange(employees);

        await dbContext.SaveChangesAsync();
    }
}