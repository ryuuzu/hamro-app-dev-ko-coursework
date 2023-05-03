using HKCRSystem.Blazor;
using HKCRSystem.Blazor.Data.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped<LoginService>();
<<<<<<< HEAD
builder.Services.AddScoped<CarRequestService>();
=======
builder.Services.AddScoped<RegisterService>();
>>>>>>> 4b98e0241a344aba994d8950a4ad2436b1257390

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();


