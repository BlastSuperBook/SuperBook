
using SQLite;
using SuperBook.Models;
using SuperBook.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperBook.Services
{
    public class ToDoService : IToDoRepository
    {
        readonly SQLiteAsyncConnection database;

        public ToDoService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDo>().Wait();
        }

        public  Task<List<ToDo>> GetItemsAsync()
        {
            return database.Table<ToDo>().ToListAsync();
        }

        public Task<List<ToDo>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ToDo>("SELECT * " +
                                             "FROM [ToDo] " +
                                             "WHERE [Completed] = 0");
        }

        public Task<List<ToDo>> GetItemsFiltered(string searchText = null)
        {
            return database.QueryAsync<ToDo>("SELECT * " +
                                             "FROM [ToDo] " +
                                             "WHERE [Title] LIKE '%" + searchText + "%'");
        }

        public Task<ToDo> GetItemAsync(int id)
        {
            return database.Table<ToDo>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public  Task<int> SaveItemAsync(ToDo task)
        {
            if (task.Id != 0)
            {
                return database.UpdateAsync(task);
            }
            else
            {
                return database.InsertAsync(task);
            }
        }
    }
}
