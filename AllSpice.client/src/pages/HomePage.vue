<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 d-flex justify-content-center">
        <img class="eggi img-fluid" src="eggi.png" alt="">
      </div>
      <div class="col-12 d-flex justify-content-center big-buttons">
        <div class="elevation-5 rounded">
          <button @click="allRecipes()" class="home-btn">Home</button>
          <button @click="myRecipes()" class="home-btn">My Recipes</button>
          <button @click="myFavorites()" class="home-btn">Favorites</button>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div v-for="r in recipes" class="col-4 py-5 recipe-box">
        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onMounted, computed } from 'vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService'
import { AppState } from '../AppState';
import Recipe from '../components/Recipe.vue';


export default {
  setup() {
    const filterType = ref("");
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    }
    onMounted(() => {
      getAllRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes),
      async allRecipes() {
        try {
          logger.log("this is what will get all recipes");
        }
        catch (error) {
          Pop.error(error.message);
          logger.error(error);
        }
      },
      async myRecipes() {
        try {
          logger.log("this is what will get all MY recipes");
        }
        catch (error) {
          Pop.error(error.message);
          logger.error(error);
        }
      },
      async myFavorites() {
        try {
          logger.log("this is what will get all my favorite recipes");
        }
        catch (error) {
          Pop.error(error.message);
          logger.error(error);
        }
      },
    };
  },
  components: { Recipe }
}
</script>

<style scoped lang="scss">
// .recipe-box {}

.home-btn {
  border: 0px solid black;
  min-height: 5vh;
  min-width: 20vh;
  color: #219653;
}

.eggi {
  width: 100%;
}
</style>
