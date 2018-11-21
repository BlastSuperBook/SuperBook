using SQLite;
using SuperBook.Contracts.Services.General;
using SuperBook.Models;
using SuperBook.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SuperBook.ViewModels
{
    public class ToDoViewModel : ViewModelBase
    {
        public List<ToDo> ToDoList { get; set; }
        SQLiteAsyncConnection database;
        public ToDoViewModel(IDialogService dialogService, INavigationService navigationService) 
            : base(dialogService, navigationService)
        {
           
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDoSQLite.db3");

            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<ToDo>();

        }

        public override async Task InitializeAsync(object data)
        {
            this.InitializeTodos();
        }

        public async void InitializeTodos()
        {
            int initialNumberOfTitles = 10;
            for (int i = 0; i < initialNumberOfTitles; i=i+2)
            {
                await database.InsertAsync(new ToDo
                {
                    Title = string.Format("Xamarin Activity {0}", (i)),
                    IsCompleted = false
                });

                await database.InsertAsync(new ToDo
                {
                    Title = string.Format("Xamarin Activity {0}", (i+1)),

                    IsCompleted = true
                });
            }

            var query = database.Table<ToDo>().ToListAsync();

            var todos = await query;
            List<ToDo> temp = new List<ToDo>();

            foreach (var item in todos)
            {
                temp.Add(item);
            }
            var total = temp.Count;
            this.ToDoList = temp;
            OnPropertyChanged("ToDoList");
        }
    }
}
