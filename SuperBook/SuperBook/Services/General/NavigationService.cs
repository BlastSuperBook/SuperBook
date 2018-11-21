using SuperBook.Container;
using SuperBook.Contracts.Services.General;
using SuperBook.ViewModels;
using SuperBook.ViewModels.Base;
using SuperBook.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperBook.Services.General
{
    public class NavigationService : INavigationService
    {
        protected Application CurrentApplication => Application.Current;

        private readonly Dictionary<Type, Type> mappings;

        public NavigationService()
        {
            this.mappings = new Dictionary<Type, Type>();
            this.CreatePageViewModelMappings();
        }

        private void CreatePageViewModelMappings()
        {
            this.mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            this.mappings.Add(typeof(DashBoardViewModel), typeof(DashBoardView));
            this.mappings.Add(typeof(AlbumsViewModel), typeof(AlbumsView));
            this.mappings.Add(typeof(PhotosTabbedViewModel), typeof(PhotosTabbedPage));
            this.mappings.Add(typeof(PhotosListViewModel), typeof(PhotosListView));
            this.mappings.Add(typeof(PhotosGridViewModel), typeof(PhotosGridView));
            this.mappings.Add(typeof(ToDoViewModel), typeof(ToDoView));
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = this.GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;

            page.BindingContext = viewModel;

            return page;
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!this.mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return this.mappings[viewModelType];
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = this.CreateAndBindPage(viewModelType, parameter);

            if (page is LoginView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is DashBoardView)
            {
                SuperBookNavigationPage navigationPage = new SuperBookNavigationPage(page);
                CurrentApplication.MainPage = navigationPage;
            }
            else if (CurrentApplication.MainPage is SuperBookNavigationPage)
            {
                var mainPage = CurrentApplication.MainPage;

                if (mainPage is SuperBookNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new SuperBookNavigationPage(page);
                    CurrentApplication.MainPage = navigationPage;
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        public Task ClearBackStack()
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<LoginViewModel>();
        }

        public Task NavigateBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
