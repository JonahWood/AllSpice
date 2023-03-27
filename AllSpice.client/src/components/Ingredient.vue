<template>
    <div class="">
        {{ ingredient.quantity }} {{ ingredient.name }}&nbsp;&nbsp;&nbsp;<span v-if="account.id == recipe.creatorId"
            class="text-danger selectable" @click="deleteIngredient(ingredient.id)">[🗑️]</span>
    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { ingredientsService } from '../services/IngredientsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
    props: {
        ingredient: { type: Object, required: true }
    },
    setup() {
        return {
            account: computed(() => AppState.account),
            recipe: computed(() => AppState.activeRecipe),
            async deleteIngredient(id) {
                try {
                    ingredientsService.deleteIngredient(id)
                    Pop.success('Ingredient removed!')
                } catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>