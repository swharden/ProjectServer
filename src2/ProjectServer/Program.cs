using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ProjectServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            if (args.Length == 1)
            {
                Console.WriteLine($"Listening on: {args[0]}");
                builder.WebHost.UseUrls(args[0]);
            }

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}