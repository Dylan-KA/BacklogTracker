using SQLite;

namespace BacklogTracker.Models
{
    public class LocalDBService
    {
        private const string DB_NAME = "game_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        private bool _isInitialized = false;

        public LocalDBService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            await _connection.CreateTableAsync<Game>();
            _isInitialized = true;
        }

        public async Task<List<Game>> GetGameListAsync() => await _connection.Table<Game>().ToListAsync();
        public async Task<Game> GetGameByID(int id) => await _connection.Table<Game>().Where(x => x.Id == id).FirstOrDefaultAsync();
        public async Task AddGameAsync(Game game) => await _connection.InsertAsync(game);
        public async Task UpdateGameAsync(Game game) => await _connection.UpdateAsync(game);
        public async Task DeleteGameAsync(Game game) => await _connection.DeleteAsync(game);

    }
}
