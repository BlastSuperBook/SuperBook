using Autofac;
using SuperBook.Contracts.Repository;
using SuperBook.Contracts.Services.Data;
using SuperBook.Contracts.Services.General;
using SuperBook.Repository;
using SuperBook.Services.Data;
using SuperBook.Services.General;
using SuperBook.ViewModels;
using System;

namespace SuperBook.Container
{
    public class AppContainer
    {
        private static IContainer container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<DashBoardViewModel>();
            builder.RegisterType<AlbumsViewModel>();
            builder.RegisterType<PhotosTabbedViewModel>();
            builder.RegisterType<PhotosListViewModel>();
            builder.RegisterType<PhotosGridViewModel>();
            builder.RegisterType<ToDoViewModel>();

            // Services - General
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
         //   builder.RegisterType<SettingsService>().As<ISettingsService>();

            // Services - Data
            builder.RegisterType<PhotoService>().As<IPhotoService>();

            // General
            builder.RegisterType<RepositoryBase>().As<IRepositoryBase>();
            container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
