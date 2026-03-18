using ConsultorioApi.Data;
using ConsultorioApi.DTOs;
using ConsultorioApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultasController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var consultas = await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .ThenInclude(m => m.Consultorio)
                .Select(c => new ConsultaResponseDTO
                {
                    Id = c.Id,
                    DataHora = c.DataHora,
                    Observacoes = c.Observacoes,
                    PacienteNome = c.Paciente.Nome,
                    MedicoNome = c.Medico.Nome,
                    ConsultorioNome = c.Medico.Consultorio.Nome
                })
                .ToListAsync();

            return Ok(consultas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var consulta = await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .ThenInclude(m => m.Consultorio)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consulta == null)
                return NotFound();

            return Ok(consulta);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Consulta c)
        {
            var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == c.PacienteId);
            if (!pacienteExiste)
                return BadRequest("Paciente não existe");

            var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == c.MedicoId);
            if (!medicoExiste)
                return BadRequest("Médico não existe");


            _context.Consultas.Add(c);
            await _context.SaveChangesAsync();

            return Ok(c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Consulta consulta)
        {
            if (id != consulta.Id)
                return BadRequest();

            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(consulta);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
                return NotFound();

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
