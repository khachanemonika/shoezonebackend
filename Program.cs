using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shoezone.Data;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow",
        policy =>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var connecionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySQL(connecionString));    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});


app.UseCors("Allow");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
