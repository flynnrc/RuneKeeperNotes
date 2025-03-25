using Microsoft.AspNetCore.Mvc;
using RuneKeeperNotes.Models;
using RuneKeeperNotes.Repositories;

namespace RuneKeeperNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : Controller // ControllerBase
    {
        private readonly INoteRepository _repository;

        public NotesController(INoteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetNotes() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Note> GetNoteById(int id)
        {
            var note = _repository.GetById(id);
            return note != null ? Ok(note) : NotFound();
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            _repository.Add(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Note note)
        {
            if (id != note.Id) return BadRequest();
            _repository.Update(note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }

}
