using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SuperBook.Contracts.Services.General;

namespace SuperBook.Services.General
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings settings;
        private const string UserName = "UserName";

        public SettingsService()
        {
            this.settings = CrossSettings.Current;
        }
        public string UserNameSetting
        {
            get => this.GetItem(UserName);
            set => this.AddItem(UserName, value);
        }

        public void AddItem(string key, string value)
        {
            this.settings.AddOrUpdateValue(key, value);
        }

        public string GetItem(string key)
        {
            return this.settings.GetValueOrDefault(key, string.Empty);
        }
    }
}
