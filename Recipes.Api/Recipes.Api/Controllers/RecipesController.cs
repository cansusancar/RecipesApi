using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipesApi.Business.Recipe;
using RecipesApi.Business.Recipe.Models;
using RecipesApi.Models;

namespace RecipesApi.Controllers;

[ApiController]
[Route("api/v1.0/recipes")]
public class RecipesController : Controller
{
    private readonly IMapper _mapper;


    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService, IMapper mapper)
    {
        _recipeService = recipeService;
        //RecipeService is injected into the RecipesController constructor.
        _mapper = mapper;
    }


    [HttpGet]
    [ProducesResponseType(typeof(List<RecipeViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> GetList()
    {
        var allRecipes = await _recipeService.GetAllRecipes();
        if (allRecipes == null)
            return NotFound();
        var response = _mapper.Map<List<RecipeViewModel>>(allRecipes);
        return Ok(response);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RecipeViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRecipeById(int id)
    {
        var recipe = await _recipeService.GetRecipeById(id);

        if (recipe == null) return NotFound("recipe not found.");

        var recipeViewModel = _mapper.Map<RecipeViewModel>(recipe);
        return Ok(recipeViewModel);
    }


    [HttpPost]
         [ProducesResponseType((int)HttpStatusCode.Created)]
         [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
         [ProducesResponseType((int)HttpStatusCode.BadRequest)]
         public async Task<ActionResult> CreateRecipe(CreateRecipeViewModel recipeViewModel)
         {
             if (string.IsNullOrEmpty(recipeViewModel.Name)) return BadRequest("recipe Name is null.");
     
             var createRecipeDataModel = _mapper.Map<CreateRecipeDataModel>(recipeViewModel);
             var result = await _recipeService.CreateRecipe(createRecipeDataModel);
     
             if (result) return Created("", null);
     
             return BadRequest("failed to create recipe.");
         }


    [HttpPut]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateRecipe(UpdateRecipeViewModel updateRecipeViewModel)
    {
        if(string.IsNullOrEmpty(updateRecipeViewModel.Name)||string.IsNullOrEmpty(updateRecipeViewModel.Description) )
            return BadRequest("name or description is null");
        
        var recipe = _mapper.Map<UpdateRecipeDataModel>(updateRecipeViewModel);
        var updateResult = await _recipeService.UpdateRecipe(recipe);
        if (updateResult) return Ok("recipe is updated successfully");

        return NotFound("recipe not found.");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> DeleteRecipe(int id)
    {
        var deleteResult = await _recipeService.DeleteRecipe(id);

        if (!deleteResult) return NotFound("recipe not found.");

        return Ok("recipe is deleted successfully.");
    }
}


 