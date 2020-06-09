using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gen2PokemonViewer.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public Ability[] Abilities { get; set; }
        public object[] Forms { get; set; }
        public int Height { get; set; }
        
        [JsonProperty("id")]
        public int InternalId { get; set; }
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
        public Species Species { get; set; }
        public Sprite Sprites { get; set; }

        public Type[] Types { get; set; }
        public int Weight { get; set; }
    }

    public class Ability
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class Type
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class Sprite
    {
        [JsonProperty("back_default")]
        public string BackDefault { get; set; }

        [JsonProperty("back_female")]
        public string BackFemale { get; set; }

        [JsonProperty("back_shiny")]
        public string BackShiny { get; set; }

        [JsonProperty("back_shiny_female")]
        public string BackShinyFemale { get; set; }

        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }

        [JsonProperty("front_female")]
        public string FrontFemale { get; set; }

        [JsonProperty("front_shiny")]
        public string FrontShiny { get; set; }

        [JsonProperty("front_shiny_female")]
        public string FrontShinyFemale { get; set; }
    }

    public class Species
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}