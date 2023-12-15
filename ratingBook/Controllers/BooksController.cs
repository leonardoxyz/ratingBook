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
    public class BooksController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BooksController(DataContext context, IUnitOfWork unitOfWork, IMapper mapper, IBookRepository bookRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var existingBooks = await _bookRepository.GetAll();

            if (existingBooks == null)
            {
                return BadRequest("Nenhum livro encontrado.");
            }

            return Ok(existingBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            return Ok(existingBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBook(Guid id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("O ID do livro na rota não corresponde ao ID do livro no corpo da solicitação.");
            }

            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            _context.Entry(existingBook).CurrentValues.SetValues(book);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("O livro foi modificado ou excluído por outra operação.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook ([FromBody] BookDto bookdto)
        {

            var map = _mapper.Map<Book>(bookdto);
            _context.Books.Add(map);
            await _unitOfWork.SaveChangesAsync();

            var responseBook = _mapper.Map<Book>(map);
            return Ok(responseBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var existingBook = await _context.Books.FindAsync(id);

            if(existingBook == null)
            {
                return NotFound();
            }

            _context.Books.Remove(existingBook);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
