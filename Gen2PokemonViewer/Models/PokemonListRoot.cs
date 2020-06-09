using System.Collections.Generic;

namespace Gen2PokemonViewer.Models
{
    public class PokemonListRoot
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<NameAndUrl> Results { get; set; }
    }
}