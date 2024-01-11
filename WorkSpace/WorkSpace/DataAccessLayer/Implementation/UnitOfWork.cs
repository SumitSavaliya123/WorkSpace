using DataAccessLayer.Abstraction;
using DataAccessLayer.Data;

namespace DataAccessLayer.Implementation
{
    public class UnitOfWork:IUnitOfWork
    {
        #region Properties
        private readonly AppDbContext _dbContext;

        #endregion Properties

        #region Constructor

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructor

        #region Interface methods

        public async Task SaveAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        #endregion Interface methods
    }
}
