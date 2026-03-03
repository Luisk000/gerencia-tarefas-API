using GerenciaTarefas.API;
using GerenciaTarefas.API.Models;
using GerenciaTarefas.API.Repository;
using GerenciaTarefas.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters
             .Add(new JsonStringEnumConverter());
     });

builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<IOauthService, OAuthService>();
builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
builder.Services.AddSingleton<IMetadataService, MetadataService>();
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Authentication:authority"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Authentication:client_id"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RequireSignedTokens = true,
        RequireExpirationTime = true
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient<OAuthService>()
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = false
        });


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
