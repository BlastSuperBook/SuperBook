using SuperBook.Contracts.Services.General;
using SuperBook.ViewModels.Base;
using System.Threading.Tasks;

namespace SuperBook.ViewModels
{
    public class PhotosGridViewModel : ViewModelBase
    {
        public PhotosGridViewModel(IDialogService dialogService, INavigationService navigationService)
            : base(dialogService, navigationService)
        {

        }
    }
}
