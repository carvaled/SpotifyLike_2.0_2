using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spotify.Application.Conta;
using Spotify.Application.Conta.Profile;
using Spotify.Application.Streaming;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

var appName = "Web Api Spotify Like";
var appVersion = "v1";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{


    c.SwaggerDoc(appVersion,
    new OpenApiInfo
    {
        Title = appName,
        Version = appVersion,
        Description = "API Serviços de Streaming Spotify Like.",
        Contact = new OpenApiContact
        {
            Name = "Eduardo Carvalho ",
            Url = new Uri("https://github.com/carvaled/SpotifyLike_2.0_2")
        }
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Adicione o token JWT para fazer as requisições na APIs",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c.UseLazyLoadingProxies()
     .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));
});


builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);


//Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PlanoRepository>();
builder.Services.AddScoped<BandaRepository>();

//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<BandaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment()) { 
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {appVersion}"); });
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
