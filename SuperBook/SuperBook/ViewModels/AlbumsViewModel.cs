using SuperBook.Contracts.Services.Data;
using SuperBook.Contracts.Services.General;
using SuperBook.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperBook.ViewModels
{
    public class AlbumsViewModel : ViewModelBase
    {
        public List<int> AlbumIdList { get; set; }
        public ICommand OnItemTapped => new Command(this.GoToPhotos);
        
        private readonly IPhotoService photoService;
        public AlbumsViewModel(IDialogService dialogService, INavigationService navigationService, IPhotoService photoService) 
            : base(dialogService, navigationService)
        {
            this.photoService = photoService;
            this.AlbumIdList = new List<int>();

            this.GetAlbumIdList();
        }

        private async void GetAlbumIdList()
        {
            var albums = await this.photoService.GetAlbumsAsync();

            List<int> albumIdList = new List<int>();

            foreach (var album in albums)
            {
                albumIdList.Add(album.Key);
            }

            this.AlbumIdList = albumIdList;
            base.OnPropertyChanged("AlbumIdList");
        }
        private async void GoToPhotos(object albumId)
        {
            await navigationService.NavigateToAsync<PhotosTabbedViewModel>((int)albumId);
        }
    }
}
