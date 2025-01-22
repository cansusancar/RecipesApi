using Microsoft.AspNetCore.Mvc;
using RecipesApi.Business.Recipe.Models;
using RecipesApi.Data.Entities;
using RecipesApi.Data.Repositories;
using RecipesApi.Models;

namespace RecipesApi.Business.Recipe;

public interface IRecipeService
{
    Task<List<RecipeDataModel>> GetAllRecipes();

    Task<RecipeDataModel> GetRecipeById(int id);
    Task<bool> CreateRecipe(CreateRecipeDataModel model);
    Task<bool> UpdateRecipe(UpdateRecipeDataModel recipeDataModel);
    Task<bool> DeleteRecipe(int id);
}

