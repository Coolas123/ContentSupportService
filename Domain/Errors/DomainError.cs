using Domain.Shared;

namespace Domain.Errors
{
    public static class DomainError
    {
        public static class UserError {

            public static readonly Error UserNameIsEmpty = new Error
            (
                "UserName.Empty",
                "User name is empty"
            );

            public static readonly Error UserNameTooLong = new Error
            (
                "UserName.TooLong",
                "User name is too long"
            );

            public static readonly Error EmailIsArleadyUsed = new Error
            (
                "User.Create",
                "The email is arleady used"
            );
        }

        public static class Author
        {
            public static readonly Error UrlPageIsArleadyUsed = new Error
            (
                "Author.GetByUrlPage",
                "The urlPage is arleady used"
            );
        }
    }
}
