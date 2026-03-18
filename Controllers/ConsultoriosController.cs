using ConsultorioApi.Data;
using ConsultorioApi.Models;
using ConsultorioApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ConsultorioApi.Controllers;



[Route("api/[controller]")]
[ApiController]
public class ConsultoriosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ViaCepService _viaCep;

    public ConsultoriosController(AppDbContext context, ViaCepService viaCep)
    {
        _context = context;
        _viaCep = viaCep;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _context.Consultorios.ToListAsync();
        return Ok(lista);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null)
            return NotFound();

        return Ok(consultorio);
    }


    [HttpPost]
    public async Task<IActionResult> Post(Consultorio consultorio)
    {
        
            var response = await _viaCep.ObterEnderecoPorAsync(consultorio.Cep);

            if (response == null) {
                return BadRequest("CEP inválido.");
            }
            consultorio.Logradouro = response.Logradouro;
            consultorio.Bairro = response.Bairro;
            consultorio.Localidade = response.Localidade;
            consultorio.Uf = response.Uf;
        
        

        _context.Consultorios.Add(consultorio);
        await _context.SaveChangesAsync();

        return Ok(consultorio);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Consultorio consultorio)
    {
        if (id != consultorio.Id)
            return BadRequest();

        _context.Entry(consultorio).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(consultorio);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null)
            return NotFound();

        _context.Consultorios.Remove(consultorio);
        await _context.SaveChangesAsync();

        return Ok();
    }
}