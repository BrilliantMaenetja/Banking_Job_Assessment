using AuditService.Application.Services;
using AuditService.Infrastructure.Data;
using AuditService.Infrastructure.Messaging;
using AuditService.Infrastructure.Repository;
using Messaging.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuditService, AuditServicee>();

builder.Services.AddSqlServer<AuditDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"),
         opts => opts.MigrationsAssembly("AuditService.Infrastructure"));

builder.Services.AddHostedService<MessageConsumerService>();

builder.Services.AddRabbitMQMessaging();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


