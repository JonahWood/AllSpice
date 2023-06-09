namespace AllSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;
        private readonly IngredientsService _ingredientsService;

        public RecipesService(RecipesRepository repo, IngredientsService ingredientsService){
            _ingredientsService = ingredientsService;
            _repo = repo;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            Recipe recipe = _repo.CreateRecipe(recipeData);
            return recipe;
        }

        internal List<Recipe> Find(string userId)
        {
            List<Recipe> recipes = _repo.FindAll();
            return recipes;
        }

        internal Recipe FindById(int id)
        {
            Recipe recipe = _repo.FindById(id);
            if(recipe == null) throw new Exception($"There is no recipe at the id: {id}");
            return recipe;
        }

        internal List<Ingredient> GetIngredientsByRecipe(int id)
        {
            Recipe recipe = this.FindById(id);
            List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipe(id);
            return ingredients;
        }

        internal Recipe removeRecipe(int id, string userId)
        {
            Recipe recipe = this.FindById(id);
            if(recipe.CreatorId != userId) throw new Exception("This is not your grandmamas recipe you scoundrel");
            _repo.removeRecipe(recipe.Id);
            return recipe;
        }


        internal Recipe Update(Recipe updateData, string userId, int id)
        {
            Recipe original = this.FindById(id);
            if (original.CreatorId != userId) throw new Exception("no bueno my friend, get your own recipe");
            original.Title = updateData.Title != null ? updateData.Title : original.Title;
            original.Instructions = updateData.Instructions != null ? updateData.Instructions : original.Instructions;
            original.Img = updateData.Img != null ? updateData.Img : original.Img;
            original.Category = updateData.Category != null ? updateData.Category : original.Category;
            int rowsAffected = _repo.Update(original);
            if(rowsAffected == 0) throw new Exception($"{updateData.Title} could not be modified");
            if(rowsAffected > 1) throw new Exception($"Something messed up chief, {rowsAffected} rows were affected.");
            return original;
        }
    }
}