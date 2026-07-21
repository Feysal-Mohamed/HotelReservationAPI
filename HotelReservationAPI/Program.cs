using HotelReservationAPI.Helper;
using HotelReservationAPI.Repositories;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services;
using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// =============================
// Initialize AppConfig
// =============================
AppConfig.Configuration = builder.Configuration;



// =============================
// JWT
// =============================
builder.Services.AddSingleton(new JwtHelper(
    builder.Configuration["Jwt:Key"],
    builder.Configuration["Jwt:Issuer"],
    builder.Configuration["Jwt:Audience"]
));



// =============================
// Repository Registration
// =============================

builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();



// =============================
// Service Registration
// =============================

builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IUserService, UserService>();




// =============================
// Controllers + Swagger
// =============================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();




// =============================
// CORS
// =============================

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




// =============================
// Authentication JWT
// =============================

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)

.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
    new TokenValidationParameters
    {

        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,


        ValidIssuer =
        builder.Configuration["Jwt:Issuer"],


        ValidAudience =
        builder.Configuration["Jwt:Audience"],


        IssuerSigningKey =
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            ))

    };

});





var app = builder.Build();




// =============================
// Swagger
// =============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}




app.UseHttpsRedirection();



// =============================
// CORS
// =============================

app.UseCors("AllowAll");



app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();


app.Run();