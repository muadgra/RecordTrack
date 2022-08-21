using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RecordTrack.Application;
using RecordTrack.Application.Validators.Records;
using RecordTrack.Infrastructure;
using RecordTrack.Infrastructure.Filters;
using RecordTrack.Infrastructure.Services.Storage.Azure;
using RecordTrack.Infrastructure.Services.Storage.Local;
using RecordTrack.Persistance;
using Serilog;
using Serilog.Core;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
));
Logger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("MSSQL"), "Logs", autoCreateSqlTable: true)
    .CreateLogger();
builder.Host.UseSerilog(logger);

builder.Services.AddControllers()
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateRecordValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Olu�turulacak token� hangi originlerin kullanaca��n� belirler
            ValidateIssuer = true, //Token� kim da��tacak?
            ValidateLifetime = true, //Token�n s�resini kontrol et
            ValidateIssuerSigningKey = true, //token�n uygulamam�za ait oldu�unu belirten key verisini do�rular


            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name // JWT'deki Name Claim'ine kar��l�k gelen de�eri User.Identity.Name propertyisinden elde eder.
        
        };
    });
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//call cors options which are described above as middleware
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
