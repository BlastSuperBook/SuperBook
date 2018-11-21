using SuperBook.Contracts.Services.General;
using SuperBook.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperBook.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand => new Command(OnLogin);

        private string username;
        private string password;

        public LoginViewModel(IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
        {
            
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                base.OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                base.OnPropertyChanged();
            }
        }

        private async void OnLogin()
        {
            await this.Authenticate(this.username, this.password);
        }

        private async Task Authenticate(string username, string password)
        { 
            if ((this.username == "admin") && (this.password == "admin"))
            {
                await navigationService.NavigateToAsync<DashBoardViewModel>();
            }
            else if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password))
            {
                await dialogService.ShowDialog("Please enter Username or Password", " ", "OK");
            }
            else if ((this.username != "admin") || (this.password != "admin"))
            {
                await dialogService.ShowDialog("Invalid Username or Password", " ", "Try Again");
            }
        }
    }
}
