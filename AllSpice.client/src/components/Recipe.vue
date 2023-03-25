<template>
    <img class="selectable" :src="recipe.img" alt="">
    <h5 class=""><span class="selectable recipe-title">{{ recipe.title }} - [{{ recipe.category
    }}]</span>&nbsp;&nbsp;&nbsp;<i v-if="account" class="mdi mdi-heart-outline"></i></h5>
</template>


<script>
import { Recipe } from '../models/Recipe';
import { computed } from 'vue'
import { AppState } from '../AppState';


export default {
    props: {
        recipe: { type: Recipe, required: true }
    },
    setup() {
        return {
            account: computed(() => AppState.account)
        }
    }
}
</script>


<style lang="scss" scoped>
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