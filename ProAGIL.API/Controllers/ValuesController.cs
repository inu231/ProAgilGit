using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;

namespace ProAGIL.API.Controllers
{
    [Route("site/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ProAgilContext _context { get; }
        public ValuesController(ProAgilContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _context.Eventos.ToListAsync(); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            

            // return new Evento[] {
            //     new Evento() {
            //         EventoID = 1,
            //         Tema = "Angular E .Net Core",
            //         Local = "Belo Horizonte",
            //         Lote = "1 Lote",
            //         QtdPessoas = 250,
            //         DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
            //     },
            //     new Evento() {
            //         EventoID = 2,
            //         Tema = "Angular E suas novidades",
            //         Local = "São Paulo",
            //         Lote = "2 Lote",
            //         QtdPessoas = 150,
            //         DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
            //     },
            // };
        }

        // GET api/values/5 -- // ***
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try //
            {
                var result = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
