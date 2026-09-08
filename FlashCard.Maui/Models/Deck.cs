using SQLite;

namespace FlashCard.Maui.Models;

[Table("Deck")]
public class Deck
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Ignore]
    public int CardCount { get; set; }
}
