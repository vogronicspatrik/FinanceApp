using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ICardRepository
    {
        Task <IEnumerable<Card>> GetAllCards();
        Task <Card> GetCardById(int Id);
        Task<Card> Create(Card card);
        Task Update(Card card);
        Task Delete(int Id);
    }
}
