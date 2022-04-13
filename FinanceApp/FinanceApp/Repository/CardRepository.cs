using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<Card> Create(Card card)
        {
            _context.cards.Add(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task Delete(int Id)
        {
            var cardToDelete = await _context.cards.FindAsync(Id);
            _context.cards.Remove(cardToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            return await _context.cards.ToListAsync();
        }

        public async Task<Card> GetCardById(int Id)
        {
            return await _context.cards.FindAsync(Id);
        }

        public async Task Update(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
