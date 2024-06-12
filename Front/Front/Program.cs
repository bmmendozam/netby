using Front.Client.Pages;
using Front.Components;
using Front;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();  

builder.Services.AddScoped<Autorizar>();

builder.Services.AddScoped<HttpClient>();

var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Front.Client._Imports).Assembly);

app.Run();