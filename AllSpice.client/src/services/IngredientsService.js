import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{
async deleteIngredient(id){
    const res = api.delete('api/ingredients/' + id)
    const index = AppState.activeIngredients.findIndex(i => i.id == id)
    if (index >= 0) {
        AppState.activeIngredients.splice(index, 1)
    }
    
}
async addIngredient(formData){
    const res = await api.post('api/ingredients', formData)
    AppState.activeIngredients.push(res.data)
}
}

export const ingredientsService = new IngredientsService