using System.Text;
using API.Data;
using API.Interfaces;
using API.Services ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();


// Registering ITokenService to the DI (Dependency Injection) container.
// Whenever a class asks for 'ITokenService', the system provides an instance of 'TokenService'.
// 'AddScoped' means one instance is created per HTTP Request (stays alive for the duration of 
// the click).

builder.Services.AddScoped<ITokenService , TokenService>() ; 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
(options =>
{
    var tokenkey = builder.Configuration["TokenKey"] ?? throw new Exception("Token key not availaible - Program.cs") ; 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true , 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)) , 
        ValidateIssuer = false , 
        ValidateAudience = false

    } ; 
}) ; 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseRouting(); // ✅ ADD THIS

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

// Adding authetication middleware 
app.UseAuthentication() ; 
app.UseAuthorization() ; 

app.MapControllers();  



app.Run();
