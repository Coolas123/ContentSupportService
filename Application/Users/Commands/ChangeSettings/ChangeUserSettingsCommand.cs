using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Application.Users.Commands.ChangeSettings
{
    public sealed class ChangeUserSettingsCommand : ICommand<bool>
    {
        [Display(Name = "Аватарка")]
        public IFormFile? AvatarFile { get; set; }

        [Display(Name = "Сменить прозвище")]
        public string? UserName { get; set; }

        [Display(Name = "Сменить почту")]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Сменть дату рождения")]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Сменить пароль")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Страна")]
        public Country? Country { get; set; }

        public Guid UserId {  get; set; }

        public bool IsSettingChanged { get; set; }
    }
}
