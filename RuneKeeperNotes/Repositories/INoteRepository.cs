using RuneKeeperNotes.Models;

namespace RuneKeeperNotes.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll();
        Note? GetById(int id);
        void Add(Note note);
        void Update(Note note);
        void Delete(int id);
    }
}
