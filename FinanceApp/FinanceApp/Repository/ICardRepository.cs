using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ICardRepository
    {
        public List<Card> GetAllCards();
        public Card GetCardById(int Id);
    }
}
