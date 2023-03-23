namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient CreateIngredient(Ingredient data)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }

        internal List<Ingredient> GetIngredientsByRecipe(int id)
        {
            string sql = @"
        SELECT 
        *
        FROM ingredients
        Where recipeId = @id;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new {id}).ToList();
        return ingredients;
        }
    }
}