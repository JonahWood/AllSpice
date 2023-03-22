namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipesService _recipesService;

        private readonly Auth0Provider _auth;

        public RecipeController(RecipesService recipesService, Auth0Provider auth){
            _recipesService = recipesService;
            _auth = auth;
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
        // FIXME this edit does not work
        [HttpPut("{id}")]
        public ActionResult<Recipe> Update(int id, [FromBody] Recipe updateData)
        {
            try 
            {
            updateData.Id = id;
            Recipe recipe = _recipesService.Update(updateData);
            return Ok(recipe);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
        
    }
}