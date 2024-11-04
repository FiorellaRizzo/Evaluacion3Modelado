using Evaluacion3.BD.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

    //Configuracion de los servicios en el constructor de la apliacion
    var builder = WebApplication.CreateBuilder(args);



    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

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


