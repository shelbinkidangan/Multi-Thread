using System.Threading.Tasks;

namespace Walle.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync(int userId);
    }
}
