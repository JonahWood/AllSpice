namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipesService _recipesService;

        private readonly Auth0Provider _auth;

        private readonly IngredientsService _ingredientsService;

        public RecipeController(RecipesService recipesService, Auth0Provider auth, IngredientsService ingredientsService){
            _recipesService = recipesService;
            _auth = auth;
            _ingredientsService = ingredientsService;
        }

        [HttpPost]
        [Authorize]
        async public Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            recipe.Creator = userInfo;
            return Ok(recipe);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
        [HttpGet]
        async public Task<ActionResult<List<Recipe>>> Find()
        {
            try 
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipes = _recipesService.Find(userInfo?.Id);
            return Ok(recipes);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Recipe> FindById(int id)
        {
            try 
            {
            Recipe recipe = _recipesService.FindById(id);
            return Ok(recipe);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe updateData)
        {
            try 
            {
              Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

            updateData.Id = id;
            Recipe recipe = _recipesService.Update(updateData, userInfo?.Id, id);
            return Ok(recipe);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
        
    [HttpDelete("{id}")]
    [Authorize]
    async public Task<ActionResult<Recipe>> removeRecipe(int id){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          Recipe recipe = _recipesService.removeRecipe(id,userInfo.Id);
          return Ok(recipe);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/ingredients")]
    public async Task<ActionResult<List<Ingredient>>> GetIngredientsByRecipe(int id)
    {
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          List<Ingredient> ingredients = _recipesService.GetIngredientsByRecipe(id);
          return Ok(ingredients);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
    }
}