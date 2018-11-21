using Acr.UserDialogs;
using SuperBook.Contracts.Services.General;
using System.Threading.Tasks;

namespace SuperBook.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
