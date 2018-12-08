using System.Threading.Tasks;
using Walle.Core.Interfaces;

namespace Walle.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        //public IOrganisationRepository Organisations { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _dbContext = context;
            //Organisations = new OrganisationRepository(context);
        }


        public async Task CompleteAsync(int userId)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
