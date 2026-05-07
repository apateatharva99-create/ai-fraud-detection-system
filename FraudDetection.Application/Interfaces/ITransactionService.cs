namespace FraudDetection.Application.Interfaces;

using FraudDetection.Domain.Entities;

public interface ITransactionService
{
    Task<bool> CheckFraudAsync(Transaction transaction);
}