using SQLite;
using SuperBook;
using SuperBook.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace Todo
{
    public partial class ToDoView : ContentPage
    {
        public ToDoView()
        {
            InitializeComponent();

            OnToggle();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        protected void OnToggle()
        {
            doneToggle.PropertyChanged += async (sender, e) =>
            {
                Switch switched = ((Switch)sender);
                if (switched.IsToggled)
                {
                    listView.ItemsSource = await App.Database.GetItemsNotDoneAsync();
                }
                else
                {
                    OnAppearing();
                }
            };
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaskView
            {
                BindingContext = new ToDo()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AddTaskView
                {
                    BindingContext = e.SelectedItem as ToDo
                });
            }
        }

        async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {  
            listView.ItemsSource = await App.Database.GetItemsFiltered(e.NewTextValue);
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            if(searchBar.Text == null)
            {
                listView.ItemsSource = await App.Database.GetItemsAsync();

                listView.IsRefreshing = false;
            }
            listView.EndRefresh();
        }
    }
}
