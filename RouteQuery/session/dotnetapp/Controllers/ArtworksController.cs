using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Data;


namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/artworks
        [HttpGet]
        public ActionResult<IEnumerable<Artwork>> GetArtworks()
        {
            return _context.Artworks.ToList();
        }

        // GET: api/artworks/{id}
        [HttpGet("{id}")]
        public ActionResult<Artwork> GetArtworkById(int id)
        {
            var artwork = _context.Artworks.Find(id);

            if (artwork == null)
            {
                return NotFound();
            }

            return artwork;
        }

        // GET: api/artworks/filter?artist={artistName}
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Artwork>> GetArtworksByArtist([FromQuery] string artist)
        {
            var artworks = _context.Artworks.Where(a => a.Artist == artist).ToList();

            if (!artworks.Any())
            {
                return NotFound();
            }

            return artworks;
        }

        // POST: api/artworks
        [HttpPost]
        public ActionResult<Artwork> CreateArtwork(Artwork artwork)
        {
            _context.Artworks.Add(artwork);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetArtworkById), new { id = artwork.ArtworkId }, artwork);
        }
    }
}