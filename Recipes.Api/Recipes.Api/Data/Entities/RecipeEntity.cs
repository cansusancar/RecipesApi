using System.ComponentModel.DataAnnotations;

namespace RecipesApi.Data.Entities;

public class RecipeEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}