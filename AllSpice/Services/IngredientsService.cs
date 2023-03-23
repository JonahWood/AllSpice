namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient CreateIngredient(Ingredient data)
        {
            Ingredient ingredient = _repo.CreateIngredient(data);
            return ingredient;
        }

        internal string DeleteIngredient(int id)
        {
            Ingredient ingredient = _repo.FindIngredientById(id);
            if (ingredient == null) throw new Exception("No Ingredient by that Id.");
            _repo.DeleteIngredient(id);
            return $"{ingredient.Name} has been eviscerated";
        }

        internal List<Ingredient> GetIngredientsByRecipe(int id)
        {
            List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(id);
            return ingredients;
        }
    }
}