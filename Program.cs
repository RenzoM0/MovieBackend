using Microsoft.EntityFrameworkCore;
using MovieBackend.Data;
using MovieBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieContext>(DbContextOptions =>
    DbContextOptions.UseSqlite("Data Source=MovieInfo.db"));

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add identity
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<MovieContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<User>();

app.Run();