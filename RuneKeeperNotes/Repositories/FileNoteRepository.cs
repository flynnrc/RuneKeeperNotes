using RuneKeeperNotes.Models;
using System.Text.Json;

namespace RuneKeeperNotes.Repositories
{
    public class FileNoteRepository : INoteRepository
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "notes.json");
        private List<Note> _notes;

        public FileNoteRepository()
        {
            _notes = LoadNotes();
        }

        public IEnumerable<Note> GetAll() => _notes;
        public Note? GetById(int id) => _notes.FirstOrDefault(n => n.Id == id);

        public void Add(Note note)
        {
            note = note with { Id = _notes.Any() ? _notes.Max(n => n.Id) + 1 : 1 };
            _notes.Add(note);
            SaveNotes();
        }

        public void Update(Note note)
        {
            var index = _notes.FindIndex(n => n.Id == note.Id);
            if (index >= 0)
            {
                _notes[index] = note;
                SaveNotes();
            }
        }

        public void Delete(int id)
        {
            _notes.RemoveAll(n => n.Id == id);
            SaveNotes();
        }


        private List<Note> LoadNotes()
        {
            if (!File.Exists(FilePath)) return new List<Note>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Note>>(json) ?? new List<Note>();
        }

        private void SaveNotes()
        {
            string directory = Path.GetDirectoryName(FilePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(_notes);
            File.WriteAllText(FilePath, json);
        }

    }
}
