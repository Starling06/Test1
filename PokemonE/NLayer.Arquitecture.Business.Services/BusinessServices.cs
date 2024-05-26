
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;



namespace NLayer.Arquitecture.Business.Services
{
    public class BusinessService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Pokemon> GetPokemon(string pokemonName)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon{pokemonName.ToLower()}";

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"No se pudo obtener la información del Pokémon. Status code: {response.StatusCode}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            var pokemonData = JObject.Parse(responseBody);

            var pokemon = new Pokemon
            {
                Nombre= pokemonData["name"]?.ToString(),
                Tipo = pokemonData["types"]?[0]?["type"]?["name"]?.ToString(),
                SpriteUrl = pokemonData["sprites"]?["front_default"]?.ToString(),
                Moves = pokemonData["moves"]?.Select(m => m["move"]?["name"]?.ToString()).ToList()
            };

            return pokemon;
        }

    }

}

    public class Pokemon
    {
        public string Nombre{ get; set; }
        public string Tipo { get; set; }
        public string SpriteUrl { get; set; }
        public List<string> Moves { get; set; }
    }
