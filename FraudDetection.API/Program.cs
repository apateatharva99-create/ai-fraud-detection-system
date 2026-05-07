using FraudDetection.Application.Interfaces;
using FraudDetection.Infrastructure.Services;
using FraudDetection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FraudDetection.Application.Repositories;
using FraudDetection.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();               // Enables controllers
builder.Services.AddEndpointsApiExplorer();      // Required for Swagger
builder.Services.AddSwaggerGen();                // Adds Swagger generator
builder.Services.AddScoped<ITransactionService, TransactionService>();
 builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
   options.TokenValidationParameters = new TokenValidationParameters
   {
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true,

       ValidIssuer = builder.Configuration["Jwt:Issuer"],
       ValidAudience = builder.Configuration["Jwt:Audience"],
       IssuerSigningKey = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
   };
});

builder.Services.AddHttpClient<AiService>();
builder.Services.AddScoped<AiService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Enable Swagger middleware
    app.UseSwaggerUI();      // Enable Swagger UI
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();        // Maps TransactionController

app.Run();
