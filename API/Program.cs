using BusinessLogic.MapperProfiles;
using BusinessLogic.Repositories;
using BusinessLogic.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        private static string GetDevDbConnString(WebApplicationBuilder builder)
        {
            string connString = builder.Configuration["ConnectionStrings:DevOpsDen"];

            string envVarName = builder.Configuration["ConnectionStrings:DbAccessEnvName"];

            string dbAccessCreds = Environment.GetEnvironmentVariable(envVarName);

            string test = Environment.GetEnvironmentVariable("DB_PASSWORD");

            string formattedConnString = string.Format(connString, dbAccessCreds);

            return formattedConnString;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                (builder.Environment.IsDevelopment()
                    ? GetDevDbConnString(builder)
                : throw new ArgumentNullException("No connection string to the DB."));

            builder.Services.AddDbContext<DenDbContext>(opt =>
            {
                opt.UseSqlServer(dbConnectionString);
            });

            // Add services to the container.

            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ReservationProfile>();
            });

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
