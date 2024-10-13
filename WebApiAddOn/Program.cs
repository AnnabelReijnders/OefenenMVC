using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OefenenMVC.Db;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VoorbeeldDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Voeg HttpClient toe voor gebruik in ApiEventsController
builder.Services.AddHttpClient();

// Configureer authenticatie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// Configureer Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Events API",
        Description = "An ASP.NET Core Web API for managing events",
        TermsOfService = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://storage.googleapis.com/pod_public/1300/175426.jpg")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://i.etsystatic.com/15378848/r/il/8a0d3a/5035247887/il_570xN.5035247887_jeia.jpg")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
