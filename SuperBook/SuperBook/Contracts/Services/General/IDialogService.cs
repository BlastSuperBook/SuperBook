using System.Threading.Tasks;

namespace SuperBook.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);
    }
}
