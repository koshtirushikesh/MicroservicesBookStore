using Microsoft.EntityFrameworkCore;
using ProductManagmentCQRS.CommandModel;
using ProductManagmentCQRS.Interface;
using ProductManagmentCQRS.Services;

namespace ProductManagmentCQRS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ProductDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB"));
            });
            builder.Services.AddTransient<ICommandServices, CommandServices>();
            builder.Services.AddTransient<IQueryServices, QueryServices>();

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