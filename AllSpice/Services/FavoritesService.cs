namespace AllSpice.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _repo;

        public FavoritesService(FavoritesRepository repo)
        {
            _repo = repo;
        }

        internal Favorite CreateFavorite(Favorite data)
        {
            Favorite favorite = _repo.CreateFavorite(data);
            return favorite;
        }

        internal List<FavoriteRecipe> GetFavorites(string accountId)
        {
            List<FavoriteRecipe> favoriteRecipes = _repo.GetFavorites(accountId);
            return favoriteRecipes;
        }

        internal string removeFavorite(int id, string userId)
        {
            Favorite favorite = _repo.GetOne(id);
            if(favorite == null) throw new Exception($"There is no favorite with the id {id}.");
            if(favorite.AccountId != userId) throw new Exception("This is not your favorite, do not try to remove it again.");
            _repo.RemoveFavorite(id);
            return "Favorite has been removed";
        }
    }
}