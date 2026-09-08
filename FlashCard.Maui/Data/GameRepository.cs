using FlashCard.Maui.Models;
using SQLite;

namespace FlashCard.Maui.Data
{
public class GameRepository
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _connection;

    public GameRepository(string dbPath) => _dbPath = dbPath;

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_connection is null)
        {
            _connection = new SQLiteAsyncConnection(_dbPath);
            await CreateTablesAsync();
        }

        return _connection;
    }

    public async Task CreateTablesAsync()
    {
        _connection ??= new SQLiteAsyncConnection(_dbPath);
        await _connection.CreateTableAsync<Deck>();
        await _connection.CreateTableAsync<Flashcard>();
    }

    public async Task<List<Deck>> GetDecksAsync() => await (await GetConnectionAsync()).Table<Deck>().ToListAsync();

    public async Task<int> AddDeckAsync(Deck deck) => await (await GetConnectionAsync()).InsertAsync(deck);

    public async Task DeleteDeckAsync(int deckId)
    {
        var connection = await GetConnectionAsync();
        await connection.Table<Flashcard>().DeleteAsync(card => card.DeckId == deckId);
        await connection.DeleteAsync(new Deck { Id = deckId });
    }

    public async Task<List<Flashcard>> GetCardsForDeckAsync(int deckId) =>
        await (await GetConnectionAsync()).Table<Flashcard>().Where(card => card.DeckId == deckId).ToListAsync();

    public async Task<int> AddCardAsync(Flashcard card) => await (await GetConnectionAsync()).InsertAsync(card);

    public async Task<int> GetCardCountForDeckAsync(int deckId) =>
        await (await GetConnectionAsync()).Table<Flashcard>().Where(card => card.DeckId == deckId).CountAsync();
}
}
