using CatFactApp.Models;
using System.Text.Json;

namespace CatFactApp.Services
{
	public class CatFactService : ICatFactService
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<CatFactService> _logger;
		private readonly string _catFactApiUrl = "https://catfact.ninja/fact";
		private readonly JsonSerializerOptions _jsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		public CatFactService(HttpClient httpClient, ILogger<CatFactService> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}

		public async Task<CatFactResponse?> GetCatFactAsync(int? maxLength = null)
		{
			try
			{
				var url = _catFactApiUrl;
				if (maxLength.HasValue && maxLength.Value > 0)
				{
					url += $"?max_length={maxLength.Value}";
					_logger.LogInformation("Requesting a cat fact with max_length: {MaxLength}", maxLength.Value);
				}
				else
				{
					_logger.LogInformation("Requesting a cat fact");
				}

				var response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();

				var jsonContent = await response.Content.ReadAsStringAsync();
				var catFact = JsonSerializer.Deserialize<CatFactResponse>(jsonContent, _jsonSerializerOptions);

				_logger.LogInformation("Received a fact: {Fact}", catFact?.Fact);
				return catFact;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while requesting a cat fact");
				return null;
			}
		}

	}
}
