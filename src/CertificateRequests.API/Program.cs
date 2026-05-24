using CertificateRequests.Infrastructure;
using CertificateRequests.Application;
using CertificateRequests.API.Middleware;
using System.Text.Json.Serialization;
using CertificateRequests.Infrastructure.Persistence;
using CertificateRequests.Infrastructure.Persistence.Seed;
using FluentValidation;
using FluentValidation.AspNetCore;
using CertificateRequests.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRequestValidator>();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await DbSeeder.SeedAsync(dbContext);

app.Run();
