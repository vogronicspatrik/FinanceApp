using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class BondRepository : IBondRepository
    {
        private readonly ApplicationDbContext _context;

        public BondRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<IEnumerable<Bond>> GetAllBonds()
        {
            return await _context.bonds.ToListAsync();
        }

        public async Task<IEnumerable<Bond>> GetAllBondsForUser(string userId)
        {
            return await _context.bonds
                .Where(bond => bond.UserId == userId)
                .ToListAsync();
        }

        public async Task<Bond> GetBondById(int id)
        {
            return await _context.bonds
                .Where(bond => bond.Id == id)
                .FirstAsync();
        }

        public async Task<Bond> Create(Bond bond)
        {
            _context.bonds.Add(bond);
            await _context.SaveChangesAsync();

            return bond;
        }

        public async Task<bool> Update(int id, Bond bond, string userId)
        {
            var bondToUpdate = await _context.bonds.FindAsync(id);

            if (bondToUpdate == null || bondToUpdate.UserId != userId) return false;

            bondToUpdate.ReturnRate = bond.ReturnRate;
            bondToUpdate.ReturnInterval = bond.ReturnInterval;
            bondToUpdate.BasePrice = bond.BasePrice;
            bondToUpdate.BondName = bond.BondName;
            bondToUpdate.Count = bond.Count;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var bondToDelete = await _context.bonds.FindAsync(id);

            if (bondToDelete == null || bondToDelete.UserId != userId) return false;

            _context.bonds.Remove(bondToDelete);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}