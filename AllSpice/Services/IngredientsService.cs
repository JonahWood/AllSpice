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

        internal List<Ingredient> GetIngredientsByRecipe(int id)
        {
            List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(id);
            return ingredients;
        }
    }
}