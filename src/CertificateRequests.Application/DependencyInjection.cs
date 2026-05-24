using CertificateRequests.Application.Interfaces;
using CertificateRequests.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CertificateRequests.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestService, RequestService>();
        

        return services;
    }
}