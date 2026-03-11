using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsultorioApi.Data;
using ConsultorioApi.Models;

namespace ConsultorioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> Get()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return pacientes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPorId(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
                return NotFound();

            return paciente;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return Ok(paciente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Paciente paciente)
        {
            var existente = await _context.Pacientes.FindAsync(id);

            if (existente == null)
                return NotFound();

            existente.Nome = paciente.Nome;
            existente.Email = paciente.Email;
            existente.Cpf = paciente.Cpf;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
                return NotFound();

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}