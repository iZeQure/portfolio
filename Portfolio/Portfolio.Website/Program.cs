using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.JSInterop;

namespace Portfolio.Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddLogging();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources/Languages";
            });

            if (builder.HostEnvironment.IsDevelopment())
            {

            }

            WebAssemblyHost host = builder.Build();

            CultureInfo culture;

            IJSRuntime js = host.Services.GetRequiredService<IJSRuntime>();

            IJSObjectReference cultureJsModule = await js.InvokeAsync<IJSObjectReference>("import", "./js/culture-settings.js");
            string result = cultureJsModule is not null ? await cultureJsModule.InvokeAsync<string>("getCulture") : null;

            if (result is not null && !string.IsNullOrEmpty(result))
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("en-US");

                await cultureJsModule.InvokeVoidAsync("setCulture", "en-US");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await host.RunAsync();
        }
    }
}
