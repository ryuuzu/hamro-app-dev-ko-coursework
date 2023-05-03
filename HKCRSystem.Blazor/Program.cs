using HKCRSystem.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.NetworkInformation;
using HKCRSystem.Blazor.Data;
using HKCRSystem.Blazor.Data.DTO;
using HKCRSystem.Blazor.Data.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddScoped<BillingService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DamageCustomerService>();
builder.Services.AddScoped<DamageService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<RequestCustomerService>();
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<ReturnService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<DashboardService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();


