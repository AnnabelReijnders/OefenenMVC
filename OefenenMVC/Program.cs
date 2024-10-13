using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OefenenMVC.Db;

var builder = WebApplication.CreateBuilder(args);

// Voeg services toe aan de container.
builder.Services.AddControllersWithViews();

// Voeg sessie-ondersteuning toe
builder.Services.AddDistributedMemoryCache(); // In-memory cache voor sessie-opslag
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sessie verloopt na 30 minuten inactiviteit
    options.Cookie.HttpOnly = true; // Maak de sessie cookie niet toegankelijk via JavaScript
    options.Cookie.IsEssential = true; // Zorg ervoor dat de sessiecookie essentieel is
});

// Voeg de DbContext toe en configureer de databaseprovider
builder.Services.AddDbContext<VoorbeeldDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configureer authenticatie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Route naar de loginpagina
        options.LogoutPath = "/Account/Logout"; // Route naar de logoutpagina
        options.AccessDeniedPath = "/Home/AccessDenied"; // Optioneel, route voor toegang geweigerd
    });

var app = builder.Build();

// Configureer de HTTP-verzoekpijplijn.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Zet sessies aan
app.UseSession();

// Zet authenticatie en autorisatie aan
app.UseAuthentication();
app.UseAuthorization();

// Stel de standaardroute in naar de Start actie van de HomeController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Start}/{id?}");

app.Run();
