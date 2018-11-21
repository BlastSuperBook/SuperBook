using Acr.UserDialogs;
using SuperBook.Contracts.Services.Data;
using SuperBook.Contracts.Services.General;
using SuperBook.Models;
using SuperBook.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperBook.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {
        public ICommand OnPhotosFrameTapped => new Command(this.NavigateToAlbums);
        public ICommand OnTodoFrameTapped => new Command(this.NavigateToTodos);
        public ICommand OnLogOut => new Command(this.LogOut);

        private int todosTotal = 10;
        private int photosTotal = 10;

        private readonly IPhotoService photoService;

        public DashBoardViewModel(IDialogService dialogService, INavigationService navigationService,
            IPhotoService photoService) : base(dialogService, navigationService)
        {
            this.photoService = photoService;

            this.GetTotalPhotos();
        }
        public int TodosTotal
        {
            get
            {
                return this.todosTotal;
            }
            set
            {
                this.todosTotal = value;
                base.OnPropertyChanged();
            }
        }
        public int PhotosTotal
        {
            get
            {
                return this.photosTotal;
            }
            set
            {
                this.photosTotal = value;
                base.OnPropertyChanged();
            }
        }
        private async void GetTotalPhotos()
        {
            IEnumerable<Photo> photos = await this.photoService.GetAllPhotosAsync();

            if (photos != null)
            {
                this.PhotosTotal = photos.ToList<Photo>().Count();
                base.OnPropertyChanged();
            }
        }

        private async void GetTotalTodos()
        {
            // must be Todos and TodoService
            IEnumerable<Photo> photos = await this.photoService.GetAllPhotosAsync();

            if (photos != null)
            {
                this.TodosTotal = photos.ToList<Photo>().Count();
                base.OnPropertyChanged();
            }
        }
        private async void NavigateToAlbums()
        {
            await navigationService.NavigateToAsync<AlbumsViewModel>();
        }

        private async void NavigateToTodos()
        {
            // must be TodosViewModel
            await navigationService.NavigateToAsync<ToDoViewModel>();
        }

        private async void LogOut()
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Are you sure you want to log out?",
                OkText = "Yes",
                CancelText = "Cancel"
            });

            if (result)
            {
                await navigationService.NavigateToAsync<LoginViewModel>();
            }
        }
    }
}
