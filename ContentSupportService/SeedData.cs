using Application.Users.Commands.RegisterUser;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SeedData
    {
        
        public async void fill(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
               .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            ISender sender = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ISender>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();

            }
            if (!context.Users.Any()) {
                var users = new List<RegisterUserCommand>{
                    new RegisterUserCommand
                    {
                        UserName="Anton",
                        DateOfBirth = DateTime.Now.AddYears(-21).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "anton@gmail.com",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-26).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "dima@gmail.com",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Dima",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-21).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "artyom@gmail.com",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Artyom",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-23).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "anna@mail.ru",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Anna",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-20).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "alena@mail.ru",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Alena",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-25).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sesa@mail.ru",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Jenya",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-26).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "dina@mail.ru",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Dina",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-27).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "olga@mail.ru",
                        Password="aA1@123456",
                        ConfirmPassword="aA1@123456",
                        UserName="Olga",
                        Country = Country.Russia
                    },
                    new RegisterUserCommand
                    {
                        DateOfBirth = DateTime.Now.AddYears(-28).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "danil@mail.ru",
                        ConfirmPassword="aA1@123456",
                        Password="aA1@123456",
                        UserName="Danil",
                        Country = Country.Russia
                    }
                };
                foreach (var user in users) {
                    var r = await sender.Send(user);
                }
            }
        }
    }
}
