using BankAccountService.Application.Services;
using BankAccountService.Infrastructure.Data;
using BankAccountService.Infrastructure.Messaging;
using BankAccountService.Infrastructure.Repository;
using Messaging.Shared.Extensions;
using Messaging.Shared.Interfaces;
using Messaging.Shared.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBankService, BankService>();
<<<<<<< Updated upstream
builder.Services.AddSqlServer<BankAccountDbContext>(builder.Configuration.GetConnectionString("DefaultConnection") ,
=======
builder.Services.AddSqlServer<BankAccountDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"),
>>>>>>> Stashed changes
    optionsAction => optionsAction.MigrationsAssembly("BankAccountService.Infrastructure"));
builder.Services.AddHostedService<MessagingConsumerService>();

builder.Services.AddRabbitMQMessaging();

<<<<<<< Updated upstream
=======
// Register MessageProducer to resolve the dependency
builder.Services.AddScoped<MessageProducer>();

>>>>>>> Stashed changes

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
