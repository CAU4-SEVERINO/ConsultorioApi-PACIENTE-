
using ConsultorioApi.Data;
using ConsultorioApi.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace ConsultorioApi
{
    public class Program
    {



        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddHttpClient<ViaCepService>();

            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddOpenApi();


            var app = builder.Build();

            //app.UseSwagger();
           // app.UseSwaggerUI();

            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Api Consultorio")
                .WithTheme(ScalarTheme.Laserwave);
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
