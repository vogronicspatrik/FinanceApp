using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardsController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        // GET: api/<CardsController>
        [HttpGet]
        public async Task<IEnumerable<Card>> GetCards()
        {
            return await _cardRepository.GetAllCards();
        }

        // GET api/<CardsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            return await _cardRepository.GetCardById(id);
        }

        // POST api/<CardsController>
        [HttpPost]
        public async Task<ActionResult<Card>> CreateCard([FromBody] Card card)
        {
            var newCard = await _cardRepository.Create(card);
            return CreatedAtAction(nameof(GetCards), new { id = newCard.Id }, newCard);

        }

        // PUT api/<CardsController>/5
        [HttpPut]
        public async Task<ActionResult> UpdateCard(int id, [FromBody] Card card)
        {
            if(id != card.Id)
            {
                return BadRequest();
            }

            await _cardRepository.Update(card);

            return NoContent();
        }

        // DELETE api/<CardsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cardToDelete = await _cardRepository.GetCardById(id);
            if(cardToDelete == null)
            {
                return NotFound();
            }
            await _cardRepository.Delete(cardToDelete.Id);
            return NoContent();
        }
    }
}
