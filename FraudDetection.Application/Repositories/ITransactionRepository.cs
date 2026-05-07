 using FraudDetection.Domain.Entities;

namespace FraudDetection.Application.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetAllAsync();
}