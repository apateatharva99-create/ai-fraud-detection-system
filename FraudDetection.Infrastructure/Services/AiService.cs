 using System.Net.Http;
using System.Text;
using System.Text.Json;

public class AiService
{
    private readonly HttpClient _httpClient;

    public AiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double> GetFraudScore(decimal amount, DateTime time)
    {
        var request = new
        {
            amount = amount,
            time = time
        };

        var json = JsonSerializer.Serialize(request);

        var response = await _httpClient.PostAsync(
            "http://127.0.0.1:8000/predict",
            new StringContent(json, Encoding.UTF8, "application/json")
        );

        var result = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<PredictionResponse>(result);

        return data.fraud_score;
    }
}

public class PredictionResponse
{
    public int is_fraud { get; set; }
    public double fraud_score { get; set; }
}