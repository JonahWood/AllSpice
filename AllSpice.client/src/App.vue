<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="sticky-bottom d-flex justify-content-end add-btn">
    <button class="btn btn-success" data-bs-toggle="modal" data-bs-target="#addRecipe" type="button"><i
        class="mdi mdi-plus-box "></i></button>
  </footer>

  <!-- Modal -->
  <div class="modal fade" id="addRecipe" tabindex="-1" aria-labelledby="addRecipeLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="addRecipeLabel">Add Recipe</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createRecipe()">
            <div class="form-floating mb-3">
              <input required v-model="editable.title" type="text" class="form-control bg-btnnav" id="title"
                placeholder="title">
              <label for="title">Title</label>
            </div>
            <div class="form-floating mb-3">
              <input required v-model="editable.instructions" type="text" class="form-control bg-btnnav" id="instructions"
                placeholder="instructions">
              <label for="instructions">Instructions</label>
            </div>
            <div class="form-floating mb-3">
              <input required v-model="editable.Img" type="text" class="form-control bg-btnnav" id="Img"
                placeholder="Img">
              <label for="Img">ImgUrl</label>
            </div>
            <div class="form-floating mb-3">
              <input required v-model="editable.category" type="text" class="form-control bg-btnnav" id="category"
                placeholder="category">
              <label for="category">Category</label>
            </div>
            <button type="submit" class="btn btn-success">Create</button>
          </form>
        </div>
        <div class="modal-footer">
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Modal } from 'bootstrap'
import { computed, ref } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { recipesService } from './services/RecipesService'
import { logger } from './utils/Logger'
import Pop from './utils/Pop'

export default {
  setup() {
    const editable = ref({})

    return {
      appState: computed(() => AppState),
      editable,
      async createRecipe() {
        try {
          const formData = editable.value
          await recipesService.createRecipe(formData)
          editable.value = {}
          Modal.getOrCreateInstance('#addRecipe').hide()
        }
        catch (error) {
          Pop.error(error.message)
          logger.error(error)
        }
      }
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.add-btn {
  min-height: 8vh;
  min-width: 8vh !important;
}

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
