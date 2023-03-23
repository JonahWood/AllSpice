namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/ingredients")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth;

        public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
        {
            _ingredientsService = ingredientsService;
            _auth = auth;
        }

[HttpPost]
[Authorize]
        public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient data)
        {
            try 
            {
            // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Ingredient ingredient = _ingredientsService.CreateIngredient(data);
            return Ok(ingredient);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Ingredient>> DeleteIngredient(int id)
        {
            try 
            {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            String message = _ingredientsService.DeleteIngredient(id);
            return Ok(message);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
    }
}