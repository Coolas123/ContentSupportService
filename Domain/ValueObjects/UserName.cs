using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects
{
    public sealed class UserName : ValueObject
    {
        public string Value { get; }

        public const int MaxLength = 64;

        public UserName(string value) {
            Value = value;
        }

        public override IEnumerable<object> GetAtomicValues() {
            yield return Value;
        }

        public static Result<UserName> Create(string value) {

            if (string.IsNullOrWhiteSpace(value)) {
                return Result.Failure<UserName>(DomainError.UserError.UserNameIsEmpty);
            }

            if(value.Length > MaxLength) {
                return Result.Failure<UserName>(DomainError.UserError.UserNameTooLong);
            }

            return Result.Success(new UserName(value));
        }
    }
}
