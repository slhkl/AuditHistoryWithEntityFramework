using Data.Entities.Audit;
using DataAccess.Discrete;

namespace DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            _context.AddAuditCustomField(nameof(AuditHistory.ChangedTime), DateTime.Now);

            return await _context.SaveChangesAsync();
        }
    }
}
