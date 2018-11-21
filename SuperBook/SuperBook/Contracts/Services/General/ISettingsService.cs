namespace SuperBook.Contracts.Services.General
{
    public interface ISettingsService
    {
        string UserNameSetting { get; set; }
        string GetItem(string key);
        void AddItem(string key, string value);
    }
}
