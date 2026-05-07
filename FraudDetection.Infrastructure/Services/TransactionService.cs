using FraudDetection.Application.Interfaces;
using FraudDetection.Domain.Entities;
using FraudDetection.Application.Repositories;

namespace FraudDetection.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

public TransactionService(ITransactionRepository repo)
{
    _repo = repo;
}

public async Task<bool> CheckFraudAsync(Transaction transaction)
{
    bool isFraud = transaction.Amount > 10000;

    transaction.IsFraud = isFraud;

    await _repo.AddAsync(transaction);

    return isFraud;
}
}