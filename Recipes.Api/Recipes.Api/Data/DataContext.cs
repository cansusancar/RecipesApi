using Microsoft.EntityFrameworkCore;
using RecipesApi.Data.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace RecipesApi.Data;

public class DataContext : DbContext
{
    public Microsoft.EntityFrameworkCore.DbSet<RecipeEntity> Recipe { get; set; }


    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}