using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Dignite.SiteBuilding.Admin.Blazor.Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var application = builder.AddApplication<SiteBuildingBlazorHostModule>(options =>
            {
                options.UseAutofac();
            });

            var host = builder.Build();

            await application.InitializeApplicationAsync(host.Services);

            await host.RunAsync();
        }
    }
}
