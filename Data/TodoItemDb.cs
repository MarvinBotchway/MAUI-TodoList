using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoItemDb
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoItemDb> Instance =
            new AsyncLazy<TodoItemDb>(async () =>
            {
                var instance = new TodoItemDb();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<TodoItemModel>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;
            });


        public TodoItemDb()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TodoItemModel>> GetItemsAsync()
        {
            return Database.Table<TodoItemModel>().ToListAsync();
        }

        public Task<List<TodoItemModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItemModel>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItemModel> GetItemAsync(int id)
        {
            return Database.Table<TodoItemModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItemModel item)
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

        public Task<int> DeleteItemAsync(TodoItemModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
