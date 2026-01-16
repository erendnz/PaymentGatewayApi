using Business.DTOs.PaymentDTOs;
using Business.Mapping;
using Business.PaymentService;
using Business.ValidationRules;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Business.Services.BankAuthorizeService;
using Business.Services.BankService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IValidator<PaymentRequestDto>, PaymentRequestValidator>();
builder.Services.AddScoped<BankAService>();
builder.Services.AddScoped<BankBService>();
builder.Services.AddScoped<BankCService>();

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


// Migration Otomation
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PaymentDbContext>();

        // If there is no db then create and apply migrations
        context.Database.Migrate();

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration sýrasýnda hata oluþtu: {ex.Message}");
    }
}

app.Run();
