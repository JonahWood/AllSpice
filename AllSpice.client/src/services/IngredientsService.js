import { logger } from "../utils/Logger"

class IngredientsService{
async deleteIngredient(){
    logger.log('zis one will delete ze ingredient')
}
}

export const ingredientsService = new IngredientsService