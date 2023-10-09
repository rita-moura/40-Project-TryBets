using System.Net.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace TryBets.Bets.Services;

public class OddService : IOddService
{
    private readonly HttpClient _client;
    public OddService(HttpClient client)
    {
        _client = client;
    }

    public async Task<object> UpdateOdd(int MatchId, int TeamId, decimal BetValue)
    {
        try
        {
            var url = new UriBuilder("http://localhost:5504/odd")
            {
                Path = $"/{MatchId}/{TeamId}/{BetValue}"
            };
            var convert = url.Uri.ToString();
            var request = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync(convert, request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        
    }
}