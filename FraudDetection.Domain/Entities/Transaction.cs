 namespace FraudDetection.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public bool IsFraud { get; set; }
}