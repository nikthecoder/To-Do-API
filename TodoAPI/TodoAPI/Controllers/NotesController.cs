using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly TodoAPIContext database;

        public NotesController(TodoAPIContext database)
        {
            this.database = database;
        }

        // GET: /notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNote([FromQuery] string completed)
        {
            var query = database.Note.AsNoTracking();

            if (completed == "false")
            {
                return await query.Where(n => n.IsDone == false).ToListAsync();
            }
            else if (completed == "true")
            {
                return await query.Where(n => n.IsDone == true).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        // GET: /notes/123
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await database.Note.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // GET: /notes/remaining
        [HttpGet("/remaining")]
        public async Task<ActionResult<int>> GetRemaining(bool? isDone)
        {
            if (isDone == null)
            {
                var itemsLeft = await database.Note.Where(n => n.IsDone == false).ToListAsync();
                int remaining = itemsLeft.Count();
                return remaining;
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: /notes/123
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.ID)
            {
                return BadRequest();
            }

            database.Entry(note).State = EntityState.Modified;

            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: /notes
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            database.Note.Add(note);
            await database.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.ID }, note);
        }

        // POST: /notes/clear-completed
        [HttpPost("/clear-completed")]
        public async Task<ActionResult> ClearCompleted()
        {
            var completed = await database.Note.Where(n => n.IsDone == true).ToListAsync();
            database.Note.RemoveRange(completed);
            //database.SaveChanges();
            await database.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /notes/123
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await database.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            database.Note.Remove(note);
            await database.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return database.Note.Any(e => e.ID == id);
        }
    }
}
