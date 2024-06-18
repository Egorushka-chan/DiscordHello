using DiscordHello.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

builder.Services.AddSingleton<IDiscordSender, FileBetterDiscordSender>();
builder.Services.AddScoped<IDiscordSender, HttpsGetDiscordSender>();
builder.Services.AddScoped<IDiscordSender, HttpsPostDiscordSender>();

var app = builder.Build();
app.UseStaticFiles();

app.Map("test/", (IEnumerable<IDiscordSender> discordSenders) =>
{
    string text = "";
    foreach(var discordSender in discordSenders)
    {
        text += discordSender.ToString();
    }
    return text;
});

app.MapControllerRoute(
    name: "talon",
    pattern: "talonsystem/{action}",
    defaults: new { controller = "Talon", action = "Start" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Begin}/{id?}");

app.Run();
