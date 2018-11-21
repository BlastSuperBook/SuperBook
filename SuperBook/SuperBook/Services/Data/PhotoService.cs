using SuperBook.Constants;
using SuperBook.Contracts.Repository;
using SuperBook.Contracts.Services.Data;
using SuperBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBook.Services.Data
{
    public class PhotoService : IPhotoService
    {
        private readonly IRepositoryBase repositoryBase;

        public PhotoService(IRepositoryBase repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PhotosEndPoint
            };

            var photos = await this.repositoryBase.GetAsync<List<Photo>>(builder.ToString());

            return photos;
        }
        public async Task<IEnumerable<IGrouping<int, Photo>>> GetAlbumsAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PhotosEndPoint
            };

            var photos = await this.repositoryBase.GetAsync<List<Photo>>(builder.ToString());

            var albums = from photo in photos
                         group photo by photo.AlbumId;

            return albums;
        }
    }
}
