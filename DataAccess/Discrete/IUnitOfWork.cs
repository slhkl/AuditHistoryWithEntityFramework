namespace DataAccess.Discrete
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
