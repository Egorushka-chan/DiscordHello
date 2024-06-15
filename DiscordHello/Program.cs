var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name:"talon",
    pattern:"talonsystem/{action}",
    defaults: new { controller="Talon", action="Start"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Begin}/{id?}");

app.Run();
