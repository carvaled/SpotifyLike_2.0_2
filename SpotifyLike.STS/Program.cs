using SpotifyLike.STS.Data.Interfaces;
using SpotifyLike.STS.Data.Options;
using SpotifyLike.STS.Data;
using SpotifyLike.STS.GrantType;
using SpotifyLike.STS.ProfileService;
using SpotifyLike.STS;
using SpotifyLike.STS.SwaggerUIDocumentation;

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
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddDocumentFilterInstance<AuthenticationOperationFilter>(new AuthenticationOperationFilter());
});

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.Configure<DataBaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();

builder.Services
    .AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(IdentityServerConfigurations.GetIdentityResource())
    .AddInMemoryApiResources(IdentityServerConfigurations.GetApiResources())
    .AddInMemoryApiScopes(IdentityServerConfigurations.GetApiScopes())
    .AddInMemoryClients(IdentityServerConfigurations.GetClients())
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddProfileService<ProfileService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// http://localhost:5105/connect/token