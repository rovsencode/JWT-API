using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P326FirstWebAPI.DAL;
using P326FirstWebAPI.Dtos.ProductDtos;
using P326FirstWebAPI.Models;
using P326FirstWebAPI.Profiles;

namespace P326FirstWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var _config = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(c=>c.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
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

            app.Run();
        }
    }
}