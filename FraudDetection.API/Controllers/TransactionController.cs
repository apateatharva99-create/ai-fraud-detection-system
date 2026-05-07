 using Microsoft.AspNetCore.Mvc;
using FraudDetection.Application.Interfaces;
using FraudDetection.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

     [HttpPost]
[Authorize]
public async Task<IActionResult> CreateTransaction(TransactionDto dto)
{
    var score = await _aiService.GetFraudScore(dto.Amount, dto.Time);

    var transaction = new Transaction
    {
        Amount = dto.Amount,
        Time = dto.Time,
        IsFraud = score > 0.7,
        Location = dto.Location,
        Device = dto.Device
    };

    _dbContext.Transactions.Add(transaction);
    await _dbContext.SaveChangesAsync();

    return Ok(new
    {
        message = "Transaction processed",
        fraudScore = score
    });
}
}