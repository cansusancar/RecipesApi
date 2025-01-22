using AutoMapper;
using RecipesApi.Business.Recipe.Models;
using RecipesApi.Data.Entities;
using RecipesApi.Models;

namespace RecipesApi.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
       
       CreateMap<RecipeEntity,RecipeDataModel>();
        CreateMap<RecipeDataModel, RecipeViewModel>();
     
        CreateMap<UpdateRecipeViewModel, UpdateRecipeDataModel>();
        CreateMap<UpdateRecipeDataModel, RecipeEntity>();
        
        CreateMap<CreateRecipeViewModel, CreateRecipeDataModel>();
        CreateMap<CreateRecipeDataModel, RecipeEntity>();
        
    }
}