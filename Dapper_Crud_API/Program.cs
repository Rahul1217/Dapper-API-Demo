using Dapper_Crud_API.Interface;
using Dapper_Crud_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDapperServices, DapperServices>();
builder.Services.AddScoped<IRefreshTokenGenrator, RefreshTokenGenrator>();
builder.Services.AddScoped<IJwtToken, JwtToken>();
builder.Services.AddScoped<ITokenRefresher, TokenRefresher>();
builder.Services.AddScoped<IRefreshTokenGenrator, RefreshTokenGenrator>();

builder.Services.AddAuthentication(Option =>
{
    Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(Option =>
{
    Option.SaveToken = true;
    Option.RequireHttpsMetadata = false;
    Option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
    };

});
//builder.Services.AddSingleton<IJwtToken>(x=>new JwtToken(x.GetService<IRefreshTokenGenrator>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
