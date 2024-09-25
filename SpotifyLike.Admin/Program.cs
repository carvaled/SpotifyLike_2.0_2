using Spotify.Application.Admin.Interfaces;
using Spotify.Application.Admin;
using Spotify.Application.Streaming;
using SpotifyLike.Repository.Admin;
using SpotifyLike.Repository.Repository;
using Application.Administrative.Profile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SpotifyLike.Repository;
using Spotify.Application.Streaming.Profile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Repository Injection
builder.Services.AddScoped(typeof(ContaAdminRepository));
builder.Services.AddScoped(typeof(BandaRepository));
builder.Services.AddScoped(typeof(MusicaRepository));
builder.Services.AddScoped(typeof(AlbumRepository));

// Add Services Injection
builder.Services.AddScoped<ContaAdminService>();
builder.Services.AddScoped<IContaAdminAuthService, ContaAdminService>();
builder.Services.AddScoped<BandaService>();
builder.Services.AddScoped<MusicaService>();
builder.Services.AddScoped<AlbumService>();

// Add Auto Mapper
builder.Services.AddAutoMapper(typeof(ContaAdminProfile).Assembly);
builder.Services.AddAutoMapper(typeof(BandaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MusicaProfile).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/account";
    options.AccessDeniedPath = "/account";
    options.LogoutPath = "/account/logout";
    options.SlidingExpiration = true;
});

// Add Register Context
builder.Services.AddDbContext<SpotifyLikeContextAdmin>(options => 
{ options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddDbContext<SpotifyLikeContext>(options =>
{
    options.UseLazyLoadingProxies()
     .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
