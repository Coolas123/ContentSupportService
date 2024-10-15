using Domain.Entities;
using Domain.Enums;

namespace Presentation.ViewModels
{
    public sealed class SettingUserViewModel
    {
        public string UserName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Email { get; init; }
        public Country Country { get; init; }
        public string? AvatarPath { get; init; }
        public string? Description { get; init; }
        public string? BanerPath { get; init; }

        public static SettingUserViewModel Create(User patron) {
            return new SettingUserViewModel
            {
                UserName = patron.UserName,
                DateOfBirth = patron.DateOfBirth,
                Email = patron.Email,
                Country = patron.Country,
                AvatarPath = patron.ProfileMaterials?.SingleOrDefault(x => x.MaterialType == MaterialType.Avatar)?.Path
            };
        }

        public static SettingUserViewModel Create(Author author) {
            return new SettingUserViewModel
            {
                UserName = author.User.UserName,
                DateOfBirth = author.User.DateOfBirth,
                Email = author.User.Email,
                Country = author.User.Country,
                AvatarPath = author.User.ProfileMaterials?.FirstOrDefault(x => x.MaterialType == MaterialType.Avatar)?.Path,
                Description =author.Description,
                BanerPath= author.User.ProfileMaterials?.FirstOrDefault(x => x.MaterialType == MaterialType.Banner)?.Path
            };
        }
    }
}
