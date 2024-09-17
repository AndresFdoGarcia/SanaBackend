
using Microsoft.EntityFrameworkCore;
using SEc.DataAccess.Context;
using SEc.DataAccess.OrderConfiguration.Contract;
using SEc.DataAccess.OrderConfiguration.Implementation;
using SEC.Business.SEcBusiness;
using SEC.Business.Service;
using System.Text.Json.Serialization;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var NewPolice = "_NewPolice";

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: NewPolice, app =>
                {
                    app.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });            


            builder.Services.AddControllers(); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var AutherConfiguration = new AutherManagerConfiguration(builder.Configuration.GetConnectionString("AutherConnetion"));
            builder.Services.AddSingleton(AutherConfiguration);

            builder.Services.AddDbContext<DBSEcContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<SEcOrdersConfigurationBusiness>();

            builder.Services.AddScoped<AuthorizationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(NewPolice);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
