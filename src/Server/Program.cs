var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (args is null || args.Length != 1)
{
    Console.WriteLine($"No argument given. Listening on default localhost URLs.");
}
else
{
    string url = args[0];
    Console.WriteLine($"Argument provided. Listening on {url}");
    app.Urls.Add(url);
}

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();