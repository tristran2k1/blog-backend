using api.Service;
using backend.Data;
using backend.Helpers;
using backend.Helpers.ConfigurationExtensions;
using backend.Interfaces;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

Log.Information("Starting web application");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigSwagger();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddPolicies(); // define roles

builder.Services.AddIdentities();   // password requirements

builder.Services.AddConfigAuthentication(
    Authority: builder.Configuration["JWT:Audience"],
    ValidIssuer: builder.Configuration["JWT:Issuer"],
    ValidAudience: builder.Configuration["JWT:Audience"],
    SigningKey: builder.Configuration["JWT:SigningKey"]!
);

builder.Services.AddScoped<IUserClaimsPrincipalFactory<Users>, UserClaimsPrincipalFactory<Users, IdentityRole>>();
builder.Services.AddScoped<IAccountRepository, AccountServices>();
builder.Services.AddScoped<IBlogRepository, BlogServices>();
builder.Services.AddScoped<IToken, TokenService>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      //.WithOrigins("https://localhost:44351))
      .SetIsOriginAllowed(origin => true));


app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
