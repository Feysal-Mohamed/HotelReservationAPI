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
// NEW CORS - Allow Any Frontend
// =============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()   // Allow any frontend/domain
                  .AllowAnyHeader()   // Allow any headers (Authorization, Content-Type)
                  .AllowAnyMethod();  // Allow GET, POST, PUT, DELETE
        });
});


// =============================
// Authentication JWT
// =============================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!
                    )
                )
        };
    });


// =============================
// Build App
// =============================
var app = builder.Build();


// =============================
// Developer Exception
// =============================
app.UseDeveloperExceptionPage();


// =============================
// Swagger
// =============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// =============================
// Middleware
// =============================
app.UseHttpsRedirection();


// =============================
// NEW CORS Middleware
// Must be before Authentication
// =============================
app.UseCors("AllowAll");


app.UseAuthentication();

app.UseAuthorization();


// =============================
// Controllers
// =============================
app.MapControllers();


// =============================
// Run Application
// =============================
app.Run();