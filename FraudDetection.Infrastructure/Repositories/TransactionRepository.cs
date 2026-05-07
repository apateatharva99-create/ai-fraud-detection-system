 using FraudDetection.Application.Repositories;
using FraudDetection.Domain.Entities;
using FraudDetection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
}