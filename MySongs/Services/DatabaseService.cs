using MySongs.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySongs.Services
{
    public class DatabaseService
    {
        #region Properties
        static object locker = new object();
        SQLiteAsyncConnection database;
        #endregion

        #region Constructor
        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Song>().Wait();
            database.CreateTableAsync<Artist>().Wait();
            database.CreateTableAsync<Album>().Wait();
            database.CreateTableAsync<SongDAO>().Wait();
        }
        #endregion

        #region Methods
        public async Task<List<Song>> GetItemsAsync()
        {
            List<Song> fav_Songs = await database.Table<Song>().ToListAsync();
            if (fav_Songs.Count == 0)
            {
                App.viewModel.IsFavSongsEmpty = true;
            }
            return fav_Songs;
        }

        public Task<List<Song>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Song>("SELECT * FROM [Song] WHERE [Done] = 0");
        }

        public Task<Song> GetItemAsync(string id)
        {
            return database.Table<Song>().Where(i => i.SongId == id).FirstOrDefaultAsync();
        }

        public Task<List<Artist>> GetArtistNotAsync(string id)
        {
            return database.QueryAsync<Artist>("SELECT * FROM [Artist] WHERE i => i.Id == " + id);
        }

        /*public Task<List<Album>> GetAlbumNotAsync(string id)
        {
            return database.QueryAsync<Album>("SELECT * FROM [Album] WHERE i => i.Id == " + id);
        }*/

        /*public Task<Album> GetAlbumAsync(string id)
        {
            return database.Table<Album>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }*/

        public Task<int> SaveItemAsync(Song item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Song item)
        {
            return database.DeleteAsync(item);
        }

        /*public Task<int> DeleteItemAsync(Album item)
        {
            return database.DeleteAsync(item);
        }*/

        public Task<int> SaveItemAsync(Artist item)
        {
            return database.InsertAsync(item);
        }

        /*public Task<int> SaveItemAsync(Album item)
        {
            return database.InsertAsync(item);
        }*/

        public Task<int> SaveItemAsync(SongDAO item)
        {
            return database.InsertAsync(item);
        }

        #endregion
    }
}
