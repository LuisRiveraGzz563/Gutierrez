var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configurar HttpClient para la api
builder.Services.AddHttpClient("MiApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();



app.Run();
