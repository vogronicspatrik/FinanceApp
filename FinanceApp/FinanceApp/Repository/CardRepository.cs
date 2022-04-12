using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public List<Card> GetAllCards()
        {
            return _context.cards.ToList();
        }

        public Card GetCardById(int Id)
        {
            return _context.cards.FirstOrDefault(x => x.Id == Id);
        }
    }
}
