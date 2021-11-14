using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.JSInterop;

namespace BlazorWebAssemblyApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources/Languages";
            });

            var host = builder.Build();

            CultureInfo culture;
            var js = host.Services.GetRequiredService<IJSRuntime>();

            var module = await js.InvokeAsync<IJSObjectReference>("import", "./js/culture-settings.js");
            var result = module is not null ? await module.InvokeAsync<string>("getCulture") : null;

            if (result is not null)
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("en-US");

                await module.InvokeVoidAsync("setCulture", "en-US");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await host.RunAsync();
        }
    }
}
