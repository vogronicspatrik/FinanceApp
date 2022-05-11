using System.Security.Claims;
using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanRepository _loanRepository;

    public LoansController(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    // GET: api/<LoansController>
    [HttpGet]
    public async Task<IEnumerable<Loan>> GetLoans()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return await _loanRepository.GetAllLoansForUser(userId);
    }

    // POST: api/<LoansController>
    [HttpPost]
    public async Task<ActionResult<Loan>> CreateLoan([FromBody] Loan loan)
    {
        loan.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var newLoan = await _loanRepository.Create(loan);
        return CreatedAtAction(nameof(GetLoans), new {id = newLoan.Id}, newLoan);
    }

    // PUT api/<LoansController>/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Loan>> PayInstallment(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (await _loanRepository.PayNextInstallment(id, userId))
        {
            return Ok(await _loanRepository.GetLoanById(id));
        }

        return BadRequest();
    }

    // DELETE api/<LoansController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (await _loanRepository.Delete(id, userId))
        {
            return NoContent();
        }

        return BadRequest();
    }
}