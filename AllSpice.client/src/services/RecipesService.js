import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class RecipesService{
async getAllRecipes(){
    const res = await api.get('api/recipes')
    const recipes = res.data.map(r => new Recipe(r))
    AppState.recipes = recipes
    logger.log(recipes)
}

async createRecipe(){
    logger.log('create recipe')
}

async setActiveRecipe(id){
    const res = await api.get('api/recipes/' + id)
    AppState.activeRecipe = res.data;
    await this.getIngredients(id)
}
async getIngredients(id) {
    const res = await api.get('api/recipes/' + id + "/ingredients");
    AppState.activeIngredients = res.data;
    logger.log('ingredients', AppState.activeIngredients)
}
async updateRecipe(updateData){
    const res = await api.put('api/recipes', updateData)
    AppState.activeRecipe = res.data
}

}

export const recipesService = new RecipesService