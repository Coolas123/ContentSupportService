using Application.Authors.Commands.ChangeSettings;
using Application.Authors.Commands.RegisterAuthor;
using Application.Users.Commands.ChangeSettings;

namespace Presentation.ViewModels
{
    public class SettingViewModel
    {
        public ChangeUserSettingsCommand? ChangeUserSettingsCommand { get; set; }
        public ChangeAuthorSettingsCommand? ChangeAuthorSettingsCommand { get; set; }
        public SettingUserViewModel? SettingUserViewModel { get; set; }
        public CreateAuthorCommand? CreateAuthorCommand { get; set; }

        public SettingViewModel(SettingUserViewModel settingUserViewModel) {
            SettingUserViewModel = settingUserViewModel;
            CreateAuthorCommand = new();
            ChangeUserSettingsCommand = new();
            ChangeAuthorSettingsCommand= new();
        }
        public SettingViewModel() { }
    }
}
