using SuperBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperBook.Repository
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetItemsAsync();
        Task<List<ToDo>> GetItemsNotDoneAsync();
        Task<List<ToDo>> GetItemsFiltered(string searchText = null);
        Task<ToDo> GetItemAsync(int id);
        Task<int> SaveItemAsync(ToDo task);
    }
}
