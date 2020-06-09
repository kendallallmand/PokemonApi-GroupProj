using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gen2PokemonViewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Gen2PokemonViewer.Data;

namespace Gen2PokemonViewer.Controllers
{
    public class PokemonController : Controller
    {

        private readonly PokemonDbContext _context;
        public PokemonController(PokemonDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string q = "", [FromQuery] int page = 0, [FromQuery] int perPage = 25)
        {
            
            List<NameAndUrl> results = CachedPokemonList.Pokemon;

            if (!string.IsNullOrEmpty(q))
            {
                results = results.Where(x => x.Name.ToLower().Contains(q)).ToList();
            }

            ViewBag.Page = page;
            ViewBag.PerPage = perPage;
            ViewBag.Pages = (int)Math.Ceiling((double)results.Count / perPage);
            ViewBag.Query = q;

            int startIndex = (page * perPage > results.Count)
                ? (results.Count - perPage - 1)
                : page * perPage;
            int count = (startIndex + perPage > results.Count)
                ? (results.Count - startIndex)
                : perPage;
            
            ViewBag.StartIndex = startIndex;

            return View(results.GetRange(startIndex, count));
        }


        public async Task<IActionResult> FavoritesPage()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserFavorites> faves = _context.UserFavorites.Where(x=> x.UserId.Equals(userId)).ToList();
            
            List<NameAndUrl> pokemon = new List<NameAndUrl>();
            faves.ForEach(x=>pokemon.Add(CachedPokemonList.Pokemon[x.PokemonId-1]));
            return View(pokemon);
        }
        
        [HttpGet]
        public async Task<IActionResult> PokemonResults([FromQuery] int id = 0)
        {
            Pokemon pokemon = await Utilities.GetApiResponse<Pokemon>(
                "api/v2", "pokemon", "https://pokeapi.co", id.ToString());
            bool auth = User.Identity.IsAuthenticated;
            if (auth)
            {
                ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.IsFavorited = _context.UserFavorites.FirstOrDefault(x => x.PokemonId.Equals(id)) != null;
            }
            else
            {
                ViewBag.UserId = "null";
            }
            return View(pokemon);
        }

        [HttpGet]
        public async Task<IActionResult> AddFavorite([FromQuery] int id = 0)
        {
            UserFavorites newFave = new UserFavorites() { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), PokemonId = id};
            await _context.UserFavorites.AddAsync(newFave);
            await _context.SaveChangesAsync();
            return Redirect("/Pokemon/PokemonResults?id=" + id);
        }
        
        [HttpGet]
        public async Task<IActionResult> RemoveFavorite([FromQuery] int id = 0)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserFavorites fave = _context.UserFavorites.FirstOrDefault(x=>x.UserId.Equals(userId) && x.PokemonId.Equals(id));
            if (fave == null)
            {
                return Forbid();
            }
            _context.UserFavorites.Remove(fave);
            await _context.SaveChangesAsync();
            return Redirect("/Pokemon/PokemonResults?id=" + id);
        }
    }
}