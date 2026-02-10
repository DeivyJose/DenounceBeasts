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
