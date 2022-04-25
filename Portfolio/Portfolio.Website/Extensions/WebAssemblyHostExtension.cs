using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Portfolio.Website.Extensions
{
    public static class WebAssemblyHostExtension
    {
        public static async Task SetDefaultHostCulture(this WebAssemblyHost host)
        {
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var cultureJsModule = await js.InjectJsObjectReference("import", "./js/culture-settings.js");

            var result = await cultureJsModule.InvokeAsync<string>("getCulture");

            CultureInfo culture;

            if (result is not null && !string.IsNullOrEmpty(result))
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("en-US");

                await cultureJsModule.InvokeVoidAsync("setCulture", culture.Name);
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
