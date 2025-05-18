namespace BlogApi.Application.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
