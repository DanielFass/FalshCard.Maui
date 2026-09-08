using SQLite;

namespace FlashCard.Maui.Models;

[Table("Flashcard")]
public class Flashcard
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int DeckId { get; set; }
    public string Front { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;
}
