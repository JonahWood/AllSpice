<!-- <template>
    <div class="modal fade" id="recipeDetails" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    {{ activeRecipe.title }}
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Understood</button>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { ref } from 'vue';
import { AppState } from '../AppState';

export default {
    setup() {
        const ingredientData = new ref({})
        return {
            ingredientData,
            activeRecipe: computed(() => AppState.activeRecipe),
            ingredients: computed(() => AppState.activeIngredients),
            account: computed(() => AppState.account),
        }
    }
}
</script>


<style lang="scss" scoped></style> -->