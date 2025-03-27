namespace RuneKeeperNotes.Models
{
    public record Note(int Id, string Title, string Content, string? RuneName = null);
}
