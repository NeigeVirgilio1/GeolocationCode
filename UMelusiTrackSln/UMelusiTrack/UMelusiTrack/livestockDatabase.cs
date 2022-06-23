﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UMelusiTrack
{
    public class livestockDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly Lazy<livestockDatabase> Instance = new Lazy<livestockDatabase>(async () =>
        {
            var instance = new livestockDatabase();
            CreateTableResult result = await Database.CreateTableAsync<livestock>();
            return instance;
        });

        public livestockDatabase()
        {
            Database = new SQLiteAsyncConnection(Constantss.DatabasePath, Constantss.Flags);
        }

        public Task<List<livestock>> GetItemsAsync()
        {
            return Database.Table<livestock>().ToListAsync();
        }

        public Task<List<livestock>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<livestock>("SELECT * FROM [livestock]");
        }

        public Task<livestock> GetItemAsync(int id)
        {
            return Database.Table<livestock>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(livestock item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(livestock item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
