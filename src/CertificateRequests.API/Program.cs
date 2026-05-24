using CertificateRequests.Infrastructure;
using CertificateRequests.Application;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers(); 
builder.Services.AddOpenApi(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build(); 

app.MapOpenApi(); 
app.UseSwagger(); 
app.UseSwaggerUI();

app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers(); 

app.Run();
