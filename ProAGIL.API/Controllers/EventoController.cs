using ProAgil.Repository;
using System.Threading.Tasks;
using ProAgil.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ProAGIL.API.Controllers
{
    [Route("api/[controller]")] // rota
    [ApiController] // tratamento de erros
    public class EventoController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public EventoController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetALLEventoAsync(true); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        // GET api/values
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var result = await _repo.GetAEventoAsyncById(EventoId, true); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet("getByTema{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _repo.GetALLEventoAsyncByTema(tema, true); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync()) {
                     return Created($"/api/evento/{model.EventoId}", model);
                }
               
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut("{EventoID}")]
        public async Task<IActionResult> Put(int EventoID, Evento model)
        {
            try
            {
                var evento = await _repo.GetAEventoAsyncById(model.EventoId, false);

                if(evento == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync()) {
                     return Created($"/api/evento/{model.EventoId}", model);
                }
               
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }
        
        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {

                var evento = await _repo.GetAEventoAsyncById(EventoId, false);

                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync()) {
                     return Ok();
                }
               
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }
    }
}