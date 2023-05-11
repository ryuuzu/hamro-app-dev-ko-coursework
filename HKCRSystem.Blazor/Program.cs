using HKCRSystem.Blazor;
using HKCRSystem.Blazor.Data.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.NetworkInformation;
using HKCRSystem.Blazor.Data;
using HKCRSystem.Blazor.Data.DTO;
using HKCRSystem.Blazor.Data.Services;
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
builder.Services.AddScoped<UpdateProfileService>();
builder.Services.AddScoped<BillingService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<ReturnService>();
builder.Services.AddScoped<Authorize>();

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
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();


