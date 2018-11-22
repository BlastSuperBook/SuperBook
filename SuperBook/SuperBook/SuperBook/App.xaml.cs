using SuperBook.Repository;
using SuperBook.Services;
using System;
using System.IO;
using Todo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SuperBook
{
    public partial class App : Application
    {

        static ToDoService database;

        public App()
        {
            InitializeComponent();
      
            var nav = new NavigationPage(new ToDoView());
            MainPage = nav;
        }

        public static ToDoService Database
        {
            get
            {
                if (database == null)
                {
                    database = new ToDoService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }

        public int ResumeAtTodoId { get; set; }

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
