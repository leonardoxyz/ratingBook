using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ratingBook.Core;
using ratingBook.Data;
using ratingBook.Model;
using ratingBook.Model.Dto;

namespace ratingBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LibraryController(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibrary()
        {
            var existingLibrarys = await _context.Libraries.ToListAsync();

            if (existingLibrarys == null || existingLibrarys.Count == 0)
            {
                return BadRequest("Nenhuma livraria encontrado.");
            }

            return Ok(existingLibrarys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Library>> GetLibrary(Guid id)
        {
            var existingLibrary = await _context.Libraries.FindAsync(id);

            if (existingLibrary == null)
            {
                return NotFound();
            }

            return Ok(existingLibrary);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutLibrary(Guid id, [FromBody] Library library)
        {
            if (id != library.Id)
            {
                return BadRequest("O ID da livraria na rota não corresponde ao ID da livraria no corpo da solicitação.");
            }

            var existingLibrary = await _context.Libraries.FindAsync(id);

            if (existingLibrary == null)
            {
                return NotFound();
            }

            _context.Entry(existingLibrary).CurrentValues.SetValues(library);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A livraria foi modificado ou excluído por outra operação.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Library>> PostLivrary([FromBody] LibraryDto libraryDto)
        {
            var map = _mapper.Map<Library>(libraryDto);
            _context.Libraries.Add(map);
            await _unitOfWork.SaveChangesAsync();

            var responseLivrary = _mapper.Map<Library>(map);
            return Ok(responseLivrary);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibrary(Guid id)
        {
            var existingLibrary = await _context.Libraries.FindAsync(id);

            if (existingLibrary == null)
            {
                return NotFound();
            }

            _context.Libraries.Remove(existingLibrary);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
