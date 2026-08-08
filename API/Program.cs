using ClassLib.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DatingDbContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(opts => opts
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200")
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
