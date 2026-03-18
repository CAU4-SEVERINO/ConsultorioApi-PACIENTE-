using ConsultorioApi.Data;
using ConsultorioApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ConsultorioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicosController(AppDbContext c)
        {
            _context = c;
        }

        [HttpPost]

        public async Task<IActionResult> CriarMedico(Medico medico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consultorioEx = await _context.Consultorios.FindAsync(medico.ConsultorioId);
            if (consultorioEx == null) return BadRequest("Consultório não encontrado.");

            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarTodosMedicos), new { id = medico.Id }, medico);
        }

        [HttpGet]

        public async Task<IActionResult> ListarTodosMedicos()
        {
            var medicos = await _context.Medicos.Select(m => new { m.Id, m.Nome, m.Crm, m.ConsultorioId, nomeConsultorio = m.Consultorio.Nome }).ToListAsync();
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarMedicoPorId(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null) return NotFound("Médico não encontrado.");

            return Ok(medico);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> EditarMedico(int id, Medico medico)
        {
            if (id != medico.Id)
            {
                return BadRequest("Id do médico não corresponde ao id da URL.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicoExistente = await _context.Medicos.FindAsync(id);

            if (medicoExistente == null) return NotFound("Médico não encontrado.");

            medicoExistente.Nome = medico.Nome;
            medicoExistente.Crm = medico.Crm;
            medicoExistente.ConsultorioId = medico.ConsultorioId;

            _context.Update(medicoExistente);
            await _context.SaveChangesAsync();
            return Ok(medicoExistente);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletarMedico(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null) return NotFound("Médico não encontrado.");
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
