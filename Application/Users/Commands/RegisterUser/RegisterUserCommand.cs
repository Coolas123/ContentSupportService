using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : ICommand<ClaimsIdentity>
    {
        [Display(Name = "Прозвище")]
        public string? UserName { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Пол")]
        public string? Gender { get; set; }
        [Display(Name = "Страна")]
        public Country Country { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "Почта")]
        public string? Email { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
