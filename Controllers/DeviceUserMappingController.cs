using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UserCrudWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceUserMappingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeviceUserMappingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceUserMapping
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceUserMapping>>> GetMappings()
        {
            return await _context.DeviceUserMappings.ToListAsync();
        }

        // GET: api/DeviceUserMapping/5
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<DeviceUserMapping>> GetMapping(int deviceId)
        {
            var mapping = await _context.DeviceUserMappings.FindAsync(deviceId);

            if (mapping == null)
            {
                return NotFound();
            }

            return mapping;
        }

        // POST: api/DeviceUserMapping
        [HttpPost]
        public async Task<ActionResult<DeviceUserMapping>> CreateMapping(DeviceUserMapping mapping)
        {
            _context.DeviceUserMappings.Add(mapping);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMapping), new { deviceId = mapping.DeviceId }, mapping);
        }

        // PUT: api/DeviceUserMapping/5
        [HttpPut("{deviceId}")]
        public async Task<IActionResult> UpdateMapping(int deviceId, DeviceUserMapping mapping)
        {
            if (deviceId != mapping.DeviceId)
            {
                return BadRequest();
            }

            _context.Entry(mapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DeviceUserMappings.Any(e => e.DeviceId == deviceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/DeviceUserMapping/5
        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> DeleteMapping(int deviceId)
        {
            var mapping = await _context.DeviceUserMappings.FindAsync(deviceId);
            if (mapping == null)
            {
                return NotFound();
            }

            _context.DeviceUserMappings.Remove(mapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
