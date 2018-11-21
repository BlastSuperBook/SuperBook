using SuperBook.Contracts.Services.Data;
using SuperBook.Contracts.Services.General;
using SuperBook.Models;
using SuperBook.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBook.ViewModels
{
    public class PhotosTabbedViewModel : ViewModelBase
    {
        public List<Photo> Photos { get; set; }

        private int albumId;
        private readonly IPhotoService photoService;

        public PhotosTabbedViewModel(IDialogService dialogService, INavigationService navigationService,
            IPhotoService photoService) : base(dialogService, navigationService)
        {
            this.photoService = photoService;

            this.Photos = new List<Photo>();

            this.GetAlbumPhotos();
        }

        public override async Task InitializeAsync(object data)
        {
            this.albumId = (int)data;
        }

        private async void GetAlbumPhotos()
        {
            var photos = await this.photoService.GetAllPhotosAsync();

            var albumPhotos = photos.Where((p) => p.AlbumId == this.albumId);

            List<Photo> temp = new List<Photo>();
            foreach (var photo in albumPhotos)
            {
                temp.Add(photo);
            }

            this.Photos = temp;
            base.OnPropertyChanged("Photos");
        }
    }
}
