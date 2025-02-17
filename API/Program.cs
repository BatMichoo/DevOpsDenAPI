using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string dbAccessCreds = Environment.GetEnvironmentVariable(builder.Configuration["ConnectionStrings:DbAccessEnvName"]) ??
                throw new ArgumentNullException("No connection string to the DB.");

            builder.Services.AddDbContext<DenDbContext>(opt =>
            {
                opt.UseSqlServer(string.Format(builder.Configuration.GetConnectionString("DevOpsDen"), dbAccessCreds));
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
