using SuperBook.Container;
using SuperBook.Contracts.Services.General;
using SuperBook.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SuperBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            this.InitializeApp();
            this.InitializeNavigation();
        }
        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }
        private void InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            navigationService.InitializeAsync();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
