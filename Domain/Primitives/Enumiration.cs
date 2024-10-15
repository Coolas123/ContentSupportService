using System.Reflection;

namespace Domain.Primitives
{
    public abstract class Enumiration<TEnum> : IEquatable<Enumiration<TEnum>>
        where TEnum : Enumiration<TEnum>
    {
        public int Value { get; protected init; } 

        public string Name { get; protected init; }

        private static readonly Dictionary<int, TEnum> enumiration = CreateEnumiration();

        protected Enumiration(int value, string name) {
            Value = value;
            Name = name;
        }

        public static TEnum? GetFromName(string name) {
            return enumiration.Values.SingleOrDefault(x => x.Name == name);
        }

        public static TEnum? GetFromValue(int value) {
            return enumiration.Values.SingleOrDefault(x => x.Value == value);
        }

        public bool Equals(Enumiration<TEnum>? other) {
            if (other is null) {
                return false;
            }
            if (other.GetType() != GetType()) {
                return false;
            }
            return Value == other.Value;
        }

        public override bool Equals(object? obj) {
            if (obj == this) {
                return true;
            }

            return Equals(obj);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        private static Dictionary<int, TEnum> CreateEnumiration() {
            var enumirationType = typeof(TEnum);

            var FieldsForType = enumirationType
                .GetFields(BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
                .Where(fieldInfo => enumirationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

            return FieldsForType.ToDictionary(x=>x.Value);
        }
    }
}
