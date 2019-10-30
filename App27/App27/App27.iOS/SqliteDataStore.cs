using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App27.iOS;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqliteDataStore))]

namespace App27.iOS
{
    public class SqliteDataStore : IDataStore<Item>
    {
        private SQLiteConnection _db;

        public SqliteDataStore()
        {
            _db = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "items.sqlite"));
            _db.CreateTable<Item>();
            if (_db.Table<Item>().Count().Equals(0))
            {
                _db.InsertAll(new List<Item>
            {
                new Item { Id = 1, Text = "First item", Description = "This is the first item description." },
                new Item { Id = 2, Text = "Second item", Description = "This is the second item description." },
                new Item { Id = 3, Text = "Third item", Description = "This is the third item description." }
            }
                );
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            _db.Insert(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItem(string id)
        {
            _db.Delete<Item>(id);
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(_db.Get<Item>(id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_db.Table<Item>().ToList());
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            _db.Update(item);
            return await Task.FromResult(true);
        }

    }
}