namespace AllSpice.Repositories
{
    public class FavoritesRepository
    {
        private readonly IDbConnection _db;

        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Favorite CreateFavorite(Favorite data)
        {
            string sql = @"
            INSERT INTO favorites
            (recipeId, accountId)
            VALUES
            (@recipeId, @accountId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }

        internal List<FavoriteRecipe> GetFavorites(string accountId)
        {
            string sql = @"
            SELECT
            rec.*,
            fav.*,
            creator.*
            FROM favorites fav
            JOIN recipes rec ON fav.recipeId = rec.id
            JOIN accounts creator ON rec.creatorId = creator.id
            WHERE fav.accountId = @accountId;
            ";
            List<FavoriteRecipe> favoriteRecipes = _db.Query<FavoriteRecipe, Favorite, Profile, FavoriteRecipe>(sql, (favoriteRecipe, favorite, profile) => 
            {
                favoriteRecipe.favoriteId = favorite.Id;
                favoriteRecipe.Creator = profile;
                return favoriteRecipe;
            }, new {accountId}).ToList();
            return favoriteRecipes;
        }

        internal Favorite GetOne(int id)
        {
            string sql = @"
            SELECT
            *
            FROM favorites
            WHERE id = @id
            ";
            Favorite favorite = _db.Query<Favorite>(sql, new {id}).FirstOrDefault();
            return favorite;
        }

        internal void RemoveFavorite(int id)
        {
            string sql = @"
            DELETE FROM favorites
            WHERE id = @id;
            ";
            _db.Execute(sql, new {id});
        }
    }
}