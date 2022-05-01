using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface IBondRepository
    {
        Task<IEnumerable<Bond>> GetAllBonds();
        Task<IEnumerable<Bond>> GetAllBondsForUser(string userId);
        Task<Bond> GetBondById(int id);
        Task<Bond> Create(Bond bond);
        Task<bool> Update(int id, Bond bond, string userId);
        Task<bool> Delete(int id, string userId);
    }
}