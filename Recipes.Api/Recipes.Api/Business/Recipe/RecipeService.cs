using AutoMapper;
using RecipesApi.Business.Recipe.Models;
using RecipesApi.Data.Entities;
using RecipesApi.Data.Repositories;

namespace RecipesApi.Business.Recipe;

public class RecipeService : IRecipeService
{
    private readonly IRepository<RecipeEntity> _repository;
    private readonly IMapper _mapper;

    public RecipeService(IRepository<RecipeEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RecipeDataModel>> GetAllRecipes()
    {
        var allRecipes = _repository.All();
        var response = _mapper.Map<List<RecipeDataModel>>(allRecipes);
        if (response == null)
            return null;

        return response.ToList();
    }


    public async Task<RecipeDataModel> GetRecipeById(int id)
    {
        var recipe = _repository.Where(recipe => recipe.Id == id).FirstOrDefault();

        RecipeDataModel recipeDataModel =  _mapper.Map<RecipeDataModel>(recipe);
        return recipeDataModel;
    }


    public async Task<bool> CreateRecipe(CreateRecipeDataModel model)
    {
        if (_repository.All().Any(r => r.Name == model.Name))
        {
            return false;
        }

        var recipeEntity = _mapper.Map<RecipeEntity>(model);
        bool addResult = await _repository.Add(recipeEntity);
        return addResult;
    }

    public async Task<bool> UpdateRecipe(UpdateRecipeDataModel updateRecipeDataModel)
    {
        var recipe = _mapper.Map<RecipeEntity>(updateRecipeDataModel);
        bool updateResult = await _repository.Update(recipe);
        return updateResult;
    }

    public async Task<bool> DeleteRecipe(int id)
    {
        var recipe = _repository.Where(recipe => recipe.Id == id).FirstOrDefault();
        bool deleteResult = await _repository.Delete(recipe);
        return deleteResult;
    }
}