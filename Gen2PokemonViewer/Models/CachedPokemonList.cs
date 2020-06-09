using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gen2PokemonViewer.Models
{
    public static class CachedPokemonList
    {
        public static List<NameAndUrl> Pokemon { get; set; }

        static CachedPokemonList()
        {
            Task<PokemonListRoot> listResponse = Utilities.GetApiResponse<PokemonListRoot>(
                "api/v2", "pokemon", "https://pokeapi.co", "", "offset", "0", "limit", "251");

            listResponse.Wait();
            Pokemon = listResponse.Result.Results;
        }
    }

    public class NameAndUrl
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}