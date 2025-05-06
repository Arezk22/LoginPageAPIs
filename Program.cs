using LoginPageAPIs.Helpers;
using LoginPageAPIs.Models;
using LoginPageAPIs.Models.SmtpSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace LoginPageAPIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });
            builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("UsersList"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:Key"])
            )
        };
    });
            builder.Services.Configure<SmtpSettings>(
            builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddTransient<EmailService>();

            builder.Services.AddTransient<EmailService>();


            builder.Services.AddAuthorization();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // <-- مهم
            app.UseAuthorization();                        
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
