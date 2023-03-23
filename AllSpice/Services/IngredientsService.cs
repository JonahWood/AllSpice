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
    }
}