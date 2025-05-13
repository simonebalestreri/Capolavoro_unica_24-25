using Microsoft.Extensions.FileProviders;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Endpoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//accediamo alla stringa di connessione
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
	builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .LogTo(Console.WriteLine, LogLevel.Information));
//aggiunge il servizio che permette ad OpenAPI di leggere i metadati delle API

var app = builder.Build();

// Configure the HTTP request pipeline.
//qui mettiamo i middleware
if (app.Environment.IsDevelopment())
{
	//permette di ottenere una rotta dove vedere la documentazione delle API secondo lo standard OpenAPI
	app.MapOpenApi();
	
}

app.UseHttpsRedirection();

// 1. Configura il middleware per servire index.html dalla cartella pages alla root
app.UseDefaultFiles(new DefaultFilesOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(builder.Environment.WebRootPath, "pages")
	),
	RequestPath = ""
});

// 2. Middleware per file statici in wwwroot (CSS, JS, ecc.)
app.UseStaticFiles();

// 3. Middleware di fallback per cercare file in wwwroot/pages
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(builder.Environment.WebRootPath, "pages")
	)
});
app.MapUserMessageEndpoints(); // Deve essere chiamato prima di app.Run();
app.Run();