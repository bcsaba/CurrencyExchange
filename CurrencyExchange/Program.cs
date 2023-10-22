using CurrencyExchange.Application.mnb;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExchangeRateDbContext>();

// builder.Services.AddIdentityServer()
//     .AddApiAuthorization<ApplicationUser, ExchangeRateDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMnbExchangeRateService, MnbExchangeRateService>();
builder.Services.AddScoped<MNBArfolyamServiceSoap, MNBArfolyamServiceSoapClient>();
builder.Services.AddDbContext<ExchangeRateDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExchangeRateDbConnection"));
});
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetLocalCurrenciesQuery).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
