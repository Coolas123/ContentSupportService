namespace Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly List<IDomainEvent> domainEvents = new();

        protected Entity(Guid id) {
            Id = id;
        }

        public Guid Id { get; private init; }

        public override bool Equals(object? obj) {

            if (obj == this) {
                return true;
            }

            if (obj is null) {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Id == ((Entity)obj).Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode() * 33;
        }

        public bool Equals(Entity? other) {

            if (other == this) {
                return true;
            }

            if (other is null) {
                return false;
            }

            if (other.GetType() != this.GetType()) {
                return false;
            }

            return Id == other.Id;
        }

        public void AddDomainEvent(IDomainEvent domainEvent) {
            domainEvents.Add(domainEvent);
        }
    }
}
