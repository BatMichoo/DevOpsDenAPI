using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        private static string GetDevDbConnString(WebApplicationBuilder builder)
        {
            string connString = string.Empty;
            string dbAccessCreds = string.Empty;

            connString = builder.Configuration["ConnectionStrings:DevOpsDen"];

            string envVarName = builder.Configuration["ConnectionStrings:DbAccessEnvName"];

            dbAccessCreds = Environment.GetEnvironmentVariable(envVarName);

            string test2 = string.Format(connString, dbAccessCreds);

            return connString;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
