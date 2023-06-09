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
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">{{ activeRecipe?.title }} <button
                            class="btn btn-success" v-if="(activeRecipe?.creator?.id) == (account?.id)" type="button"
                            data-bs-dismiss="modal" data-bs-toggle="modal" data-bs-target="#ingredient"><i
                                class="mdi mdi-plus"></i> Ingredient</button></h1>
                    <button @click="editRecipeModeFalse()" type="button" class="btn-close" data-bs-dismiss="modal"
                        aria-label="Close"></button>
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
                <!-- SECTION [STANDARD] footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning"
                        v-if="(account?.id == activeRecipe?.creator?.id) && (!editMode)"
                        @click="editRecipeMode()">Edit</button>
                    <button type="button" class="btn btn-secondary"
                        v-if="(account?.id == activeRecipe?.creator?.id) && (editMode)"
                        @click="editRecipeMode()">Cancel</button>
                    <button @click="editRecipeModeFalse()" type="button" class="btn btn-secondary"
                        data-bs-dismiss="modal">Close</button>
                </div>

                <div class="container-fluid">
                    <!-- SECTION [EDIT MODE] footer -->
                    <div class="row">
                        <div class="col-12">
                            <div v-if="editMode" class="modal-footer d-flex justify-content-start">
                                <form @submit.prevent="editRecipe(activeRecipe?.id)">
                                    <div class="mb-2">
                                        <label for="title">Title</label>
                                        <input v-model="editable.title" type="text" class="form-control" id="title"
                                            :placeholder="activeRecipe?.title">
                                        <label for="img">Img</label>
                                        <input v-model="editable.img" type="text" class="form-control" id="img"
                                            :placeholder="activeRecipe?.img">
                                        <label for="instructions">Instructions</label>
                                        <input v-model="editable.instructions" type="text" class="form-control editForm"
                                            id="instructions" :placeholder="activeRecipe?.instructions">
                                    </div>
                                    <button type="submit" class="btn btn-success">Save</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- SECTION yet another modal -->
    <div class="modal fade" id="ingredient" tabindex="-1" aria-labelledby="ingredientLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content ingredient-modal">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="ingredientLabel">Add Ingredient</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="addIngredient(activeRecipe?.id)">
                        <label for="name">Ingredient</label>
                        <input v-model="ingredientData.name" type="text" class="form-control" id="name">
                        <label for="quantity">Quantity</label>
                        <input v-model="ingredientData.quantity" type="text" class="form-control" id="quantity">
                        <input v-model="ingredientData.recipeId" type="text" class="form-control d-none" id="recipeId">
                        <button type="submit" class="btn btn-light">Add</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Recipe } from '../models/Recipe';
import { computed, onUnmounted, ref } from 'vue'
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
        const editable = ref({})
        const ingredientData = ref({})
        onUnmounted(() => {
            nullified()
        })
        function nullified() {
            try {
                AppState.activeRecipe = null
            }
            catch (error) {
                Pop.error(error.message)
                logger.error(error)
            }
        }
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
            editRecipeModeFalse() {
                AppState.editMode = false
            },
            async addIngredient(recipeId) {
                try {
                    ingredientData.value.recipeId = recipeId
                    const formData = ingredientData.value
                    logger.log('formdata:', formData)
                    await ingredientsService.addIngredient(formData)
                    Pop.success('Ingredient added!')
                }
                catch (error) {
                    Pop.error(error.message)
                    logger.error(error)
                }
            },
            async editRecipe(recipeId) {
                try {
                    const formData = editable.value
                    if (formData.Img == null) {
                        formData.Img = AppState.activeRecipe.Img
                    }
                    await recipesService.updateRecipe(formData, recipeId)
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
.editForm {
    min-width: 50vw !important;
}

.ingredient-modal {
    background-color: #39399e8b;
    color: white;
    text-shadow: 2px 2px 2px black;
}

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