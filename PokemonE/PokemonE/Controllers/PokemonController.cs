
using Microsoft.AspNetCore.Mvc;
using NLayer.Arquitecture.Business.Services;

namespace PokemonE.controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PokemonController : ControllerBase
    {
        private readonly BusinessService _businessService;

        public PokemonController(BusinessService businessService)
        {
            _businessService = businessService;
        }


        [HttpGet("{pokemonName}")]
        public async Task<IActionResult> GetPokemon(string pokemonName)
        {
            try
            {
                var pokemon = await _businessService.GetPokemon(pokemonName);
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

