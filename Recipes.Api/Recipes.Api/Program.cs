using Microsoft.EntityFrameworkCore;
using RecipesApi;
using RecipesApi.AutoMapper;
using RecipesApi.Business;
using RecipesApi.Business.Recipe;
using RecipesApi.Data;
using RecipesApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();