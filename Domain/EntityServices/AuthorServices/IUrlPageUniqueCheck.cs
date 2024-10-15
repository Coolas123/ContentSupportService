namespace Domain.EntityServices.AuthorServices
{
    public interface IUrlPageUniqueCheck
    {
        Task<bool> IsUnique(string urlPage);
    }
}
