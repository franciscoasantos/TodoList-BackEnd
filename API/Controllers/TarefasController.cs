using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Tarefas")]
    public class TarefasController : ControllerBase
    {
        private readonly TarefaContext _context;

        public TarefasController(TarefaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetTodoItems()
        {
            return await _context.Tarefas
                .Select(x => TarefaToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> Get(long id)
        {
            var todoItem = await _context.Tarefas.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> Post(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Tarefa tarefa)
        {
            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExiste(tarefa.Id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = await _context.Tarefas.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static TarefaDTO TarefaToDTO(Tarefa tarefa) =>
            new TarefaDTO
            {
                Id = tarefa.Id,
                Nome = tarefa.Nome,
                IsComplete = tarefa.IsComplete
            };

        private bool TarefaExiste(long id) =>
            _context.Tarefas.Any(e => e.Id == id);
    }
}
