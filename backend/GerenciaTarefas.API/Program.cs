using GerenciaTarefas.API;
using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters
             .Add(new JsonStringEnumMemberConverter());
     });

builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreConnection"),
        o => {
            o.MapEnum<StatusTipo>("status_tipo");
            o.MapEnum<PrioridadeTipo>("prioridade_tipo");
        }
    )
);
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        }
    )
);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();


app.Run();
