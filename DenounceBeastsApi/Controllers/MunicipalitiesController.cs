using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DenounceBeastsApi.Models;
using DenounceBeastsApi.DTOs;

namespace DenounceBeastsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly DenounceBeastsDbContext _context;

        public MunicipalitiesController(DenounceBeastsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipalityReadDto>>> GetMunicipalities()
        {
            return await _context.Municipalities
                .Select(m => new MunicipalityReadDto
                {
                    MunicipalityId = m.MunicipalityId,
                    Name = m.Name,
                    SectorId = m.SectorId
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MunicipalityReadDto>> GetMunicipality(int id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
                return NotFound();

            return new MunicipalityReadDto
            {
                MunicipalityId = municipality.MunicipalityId,
                Name = municipality.Name,
                SectorId = municipality.SectorId
            };
        }

        [HttpPost]
        public async Task<IActionResult> PostMunicipality(MunicipalityCreateDto dto)
        {
            var municipality = new Municipality
            {
                Name = dto.Name,
                SectorId = dto.SectorId
            };

            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return Ok(municipality);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipality(int id, MunicipalityCreateDto dto)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
                return NotFound();

            municipality.Name = dto.Name;
            municipality.SectorId = dto.SectorId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipality(int id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
                return NotFound();

            _context.Municipalities.Remove(municipality);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
