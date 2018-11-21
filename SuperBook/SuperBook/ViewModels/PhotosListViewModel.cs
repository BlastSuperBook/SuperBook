using SuperBook.Contracts.Services.General;
using SuperBook.ViewModels.Base;

namespace SuperBook.ViewModels
{
    public class PhotosListViewModel : ViewModelBase
    {
        public PhotosListViewModel(IDialogService dialogService, INavigationService navigationService) 
            : base(dialogService, navigationService)
        {
        }
    }
}
