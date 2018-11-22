using SuperBook;
using SuperBook.Models;
using System;
using Xamarin.Forms;

namespace Todo
{
    public partial class AddTaskView : ContentPage
    {
        public AddTaskView()
        {
            InitializeComponent(); 
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todo = (ToDo)BindingContext;

            if (title.Text == null)
            {
                await DisplayAlert("Alert", "Title cannot be empty", "OK");

            }
            else
            {
                await App.Database.SaveItemAsync(todo);
                await Navigation.PopAsync();
            }  
        }
    }
}
