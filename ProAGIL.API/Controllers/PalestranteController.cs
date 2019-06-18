using ProAgil.Repository;
using System.Threading.Tasks;
using ProAgil.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ProAGIL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // exibe erros !
    public class PalestranteController : ControllerBase
    {
        public IProAgilRepository _repo { get; }
        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetALLPalestrantesAsync(true); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        // GET api/values
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = await _repo.GetPalestranteAsyncById(Id, true); 
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync()) {
                     return Created($"/api/palestrante/{model.Id}", model);
                }
               
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int Id, Palestrante model)
        {
            try
            {
                var Palestrante = await _repo.GetPalestranteAsyncById(Id, false);

                if(Palestrante == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync()) {
                     return Created($"/api/palestrante/{model.Id}", model);
                }
               
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id, Palestrante model)
        {
            try
            {
                var Palestrante = await _repo.GetPalestranteAsyncById(Id, false);

                if(Palestrante == null) return NotFound();

                _repo.Delete(model);

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