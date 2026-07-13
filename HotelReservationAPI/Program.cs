using HotelReservationAPI.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Initialize AppConfig
// =============================
AppConfig.Configuration = builder.Configuration;

// =============================
// Database (EF Core)
// =============================
builder.Services.AddDbContext<userHelper>(options =>
    options.UseSqlServer(AppConfig.ConnectionString));

// =============================
// JWT
// =============================
builder.Services.AddSingleton(new JwtHelper(
    builder.Configuration["Jwt:Key"],
    builder.Configuration["Jwt:Issuer"],
    builder.Configuration["Jwt:Audience"]
));

// =============================
// Services
// =============================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================
// Authentication
// =============================
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// =============================
// Build App
// =============================
var app = builder.Build();
app.UseDeveloperExceptionPage();
// =============================
// Middleware
// =============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();