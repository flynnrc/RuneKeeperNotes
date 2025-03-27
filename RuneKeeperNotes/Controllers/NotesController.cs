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
            if (!string.IsNullOrEmpty(note.RuneName) && !RuneData.IsValidRuneByName(note.RuneName))
            {
                return BadRequest(new { message = "Invalid rune name", rune = note.RuneName });
            }

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

        [HttpGet("validate/{rune}")]
        public IActionResult ValidateRune(string rune)
        {
            if (RuneData.IsValidRuneByName(rune))
            {
                //could turn this into tryget to make it cleaner
                var r = RuneData.GetRuneByName(rune);
                return Ok(new { message = "Valid rune", rune, r.Name, r.Symbol, r.Meaning});
            }
            if(RuneData.IsValidRuneBySymbol(rune))
            {
                var r = RuneData.GetRuneByName(rune);
                return Ok(new { message = "Valid rune", r.Name, r.Symbol, r.Meaning});
            }

            return BadRequest(new { message = "Invalid rune", rune });
        }

    }

}
