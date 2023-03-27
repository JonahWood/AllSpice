<template>
    <img @click="setActiveRecipe(recipe.id)" data-bs-toggle="modal" data-bs-target="#recipeDetails" class="selectable"
        :src="recipe.img" alt="">
    <h5 class="bg-dark"><span @click="setActiveRecipe(recipe.id)" data-bs-toggle="modal" data-bs-target="#recipeDetails"
            class="selectable recipe-title">{{
                recipe.title }} - [{{ recipe.category
    }}]</span>&nbsp;&nbsp;&nbsp;<i v-if="account" class="mdi mdi-heart-outline"></i></h5>
    <div class="modal fade" id="recipeDetails" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true">

        <!-- SECTION modal -->
        <div class="modal-dialog recipe-modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">{{ activeRecipe?.title }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body container-fluid">
                    <div class="row">
                        <div class="col-5">
                            <img :src="activeRecipe?.img" alt="">
                        </div>
                        <div class="col-7">
                            <div class="row">
                                <div class="col-6 bg-light py-1">
                                    <div class="minor-headers d-flex justify-content-center rounded">
                                        <h3 class="p-2 text-light">INGREDIENTS</h3>
                                    </div>
                                    <h6 class="px-2" v-for="i in ingredients">
                                        <Ingredient :ingredient="i" />
                                    </h6>
                                </div>
                                <div class="col-6 bg-light py-1">
                                    <div class="minor-headers d-flex justify-content-center rounded">
                                        <h3 class="p-2 text-light">STEPS</h3>
                                    </div>
                                    <h6 class="px-2">{{ activeRecipe?.instructions }}</h6>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning"
                        v-if="(account.id == activeRecipe?.creator.id) && (!editMode)"
                        @click="editRecipeMode()">Edit</button>
                    <button type="button" class="btn btn-secondary"
                        v-if="(account.id == activeRecipe?.creator.id) && (editMode)"
                        @click="editRecipeMode()">Cancel</button>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>


                <div v-if="editMode" class="modal-footer">
                    <form @submit.prevent="editRecipe()">
                        <label for="name">Name</label>
                        <input required v-model="editable.name" type="text" class="form-control" id="name"
                            placeholder="Name">

                        <button type="submit" class="btn btn-success">Save</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Recipe } from '../models/Recipe';
import { computed, ref } from 'vue'
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
import Ingredient from './Ingredient.vue';
import { ingredientsService } from '../services/IngredientsService';


export default {
    props: {
        recipe: { type: Recipe, required: true }
    },
    setup() {
        const editable = new ref({})
        const ingredientData = new ref({})
        return {
            account: computed(() => AppState.account),
            activeRecipe: computed(() => AppState.activeRecipe),
            editMode: computed(() => AppState.editMode),
            ingredientData,
            editable,
            ingredients: computed(() => AppState.activeIngredients),
            async setActiveRecipe(id) {
                try {
                    recipesService.setActiveRecipe(id);
                }
                catch (error) {
                    Pop.error(error.message);
                    logger.error(error);
                }
            },
            async getIngredientsByRecipeId() {
                try {
                }
                catch (error) {
                    Pop.error(error.message);
                    logger.error(error);
                }
            },
            editRecipeMode() {
                AppState.editMode = !AppState.editMode
            },
            async addIngredient(recipeId) {
                try {
                    await ingredientsService.addIngredient(recipeId)
                    Pop.success('Ingredient added!')
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async editRecipe() {
                try {
                    await recipesService.updateRecipe(editable.value)
                    AppState.editMode = false
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            }
        };
    },
    components: { Ingredient, Ingredient, Ingredient }
}
</script>


<style lang="scss" scoped>
.minor-headers {
    background-color: #219653;
}

.recipe-modal {
    height: 60vh;
    min-width: 80%;
}

.recipe-title {
    text-decoration: underline;
    color: #219653;
}

img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    background-position: center;
}
</style>