using HKCRSystem.Blazor;
using HKCRSystem.Blazor.Data.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CarRequestService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<DamageService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<UserManagementService>();
builder.Services.AddScoped<BillingService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<ReturnService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();


