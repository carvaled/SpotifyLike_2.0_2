using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Spotify.Application.Conta;
using Spotify.Application.Conta.Profile;
using Spotify.Application.Streaming;
using SpotifyLike.Options;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

var appName = "Web Api Spotify Like";
var appVersion = "v1";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});
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

builder.Services.Configure<IdentityServerConfigurations>(builder.Configuration.GetSection("IdentityServerConfigurations"));

// Adiciona o serviço de autenticação
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(options =>
{
    var identityServerOptions = builder?.Services?.BuildServiceProvider().GetRequiredService<IOptions<IdentityServerConfigurations>>().Value;
    options.Authority = identityServerOptions.Authority;
    options.ApiName = identityServerOptions.ApiName;
    options.ApiSecret = identityServerOptions.ApiSecret;
    options.RequireHttpsMetadata = identityServerOptions.RequireHttpsMetadata;
    options.LegacyAudienceValidation = identityServerOptions.LegacyAudienceValidation;
});


builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​).RequireAuthenticatedUser().Build());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment()) { 
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {appVersion}"); });
//}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCertificateForwarding();

if (app.Environment.IsProduction())
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
    });
}
else
    app.MapControllers();

app.Run();
