using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperBook.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SuperBookNavigationPage : NavigationPage
	{
		public SuperBookNavigationPage ()
		{
			InitializeComponent();
		}
        public SuperBookNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}