using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DenounceBeastsApi.Models;
using DenounceBeastsApi.DTOs;

namespace DenounceBeastsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly DenounceBeastsDbContext _context;

        public SectorsController(DenounceBeastsDbContext context)
        {
            _context = context;
        }

        // GET: api/Sectors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectorReadDto>>> GetSectors()
        {
            return await _context.Sectors
                .Select(s => new SectorReadDto
                {
                    SectorId = s.SectorId,
                    Name = s.Name
                })
                .ToListAsync();
        }

        // GET: api/Sectors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SectorReadDto>> GetSector(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);

            if (sector == null)
                return NotFound();

            return new SectorReadDto
            {
                SectorId = sector.SectorId,
                Name = sector.Name
            };
        }

        // POST: api/Sectors
        [HttpPost]
        public async Task<ActionResult> PostSector(SectorCreateDto dto)
        {
            var sector = new Sector
            {
                Name = dto.Name
            };

            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync();

            return Ok(sector);
        }

        // PUT: api/Sectors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSector(int id, SectorCreateDto dto)
        {
            var sector = await _context.Sectors.FindAsync(id);

            if (sector == null)
                return NotFound();

            sector.Name = dto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Sectors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSector(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);

            if (sector == null)
                return NotFound();

            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
