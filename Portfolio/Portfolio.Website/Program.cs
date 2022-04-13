using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Website.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace Portfolio.Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            //builder.RootComponents.Add<HeadOutlet>("head:after");

            var services = builder.Services;

            services.AddLogging();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources/Languages";
            });

            WebAssemblyHost host = builder.Build();

            await host.SetDefaultHostCulture();

            await host.RunAsync();
        }
    }
}
