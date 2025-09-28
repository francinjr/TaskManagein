
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagein.Data;
using TaskManagein.Exceptions;
using TaskManagein.Exceptions.Handler;
using TaskManagein.Repositories;
using TaskManagein.Repositories.Interfaces;
using TaskManagein.Services.Interfaces;
using TaskManagein.Services;

namespace TaskManagein
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /* Para usar SQL Server:
             * builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<TaskManageinDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );*/

            // Para usar o posgresql
            builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<TaskManageinDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase"))
    );

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            builder.Services.AddScoped<ITaskService, TaskService>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .SelectMany(m => m.Value.Errors.Select(e => new ValidationField(m.Key, e.ErrorMessage)))
                        .ToList();

                    // Ao inves de retornar uma resposta com os campos invalidos, lanço uma exceção pois irá cair no
                    // exception handler, logo se houver um serviço de logs, será executado adequadamente.
                    throw new InvalidFieldException("Há campos que não foram preenchidos corretamente", errors);
                    //return new BadRequestObjectResult(errors);
                };
            });


            // Handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Handler
            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
