using Domain.Shared;

namespace Domain.Errors
{
    public static class PersistenceError
    {
        public static class User {
            public static readonly Error UserNotFound = new Error
            (
                "UserRepository.GetByIdAsync",
                "User was not found"
            );

            public static readonly Error UserCouldNotRegister = new Error
            (
                "UserRepository.GetByIdAsync",
                "User was not found"
            );
        }

        public static class Author
        {
            public static readonly Error AuthorNotFound = new Error
            (
                "AuthorRepository.GetByIdAsync",
                "Author was not found"
            );
        }
    }
}
