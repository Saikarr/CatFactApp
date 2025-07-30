using CatFactApp.Models;

namespace CatFactApp.Services
{
	public class FileService : IFileService
	{
		private readonly ILogger<FileService> _logger;
		private readonly string _fileName = "catfacts.txt";

		public FileService(ILogger<FileService> logger)
		{
			_logger = logger;
		}

		public async Task SaveCatFactToFileAsync(CatFactResponse catFact)
		{
			try
			{
				var textToSave = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] Fact: {catFact.Fact} | Length: {catFact.Length}";

				await File.AppendAllTextAsync(_fileName, textToSave + Environment.NewLine);

				_logger.LogInformation("Cat fact successfully saved");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while saving a cat fact");
				throw;
			}
		}
	}
}
