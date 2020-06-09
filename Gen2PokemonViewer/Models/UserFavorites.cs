using System;
using System.Collections.Generic;

namespace Gen2PokemonViewer.Models
{
    public partial class UserFavorites
    {
        public string UserId { get; set; }
        public int PokemonId { get; set; }
        public int Id { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
