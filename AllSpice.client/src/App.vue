<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="sticky-bottom d-flex justify-content-end">
    <button @click="createRecipe()" class="btn btn-success add-btn"><i class="mdi mdi-plus-box "></i></button>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { recipesService } from './services/RecipesService'
import { logger } from './utils/Logger'
import Pop from './utils/Pop'

export default {
  setup() {

    return {
      appState: computed(() => AppState),
      async createRecipe() {
        try {
          await recipesService.createRecipe()
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

.add-btn {}

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
