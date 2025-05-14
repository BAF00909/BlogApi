namespace BlogApi.Application.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IPostRepository Posts { get; }
        Task<int> SaveChangesAsync();
    }
}
