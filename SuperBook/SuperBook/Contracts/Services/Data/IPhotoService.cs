using SuperBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBook.Contracts.Services.Data
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
        Task<IEnumerable<IGrouping<int, Photo>>> GetAlbumsAsync();
    }
}
