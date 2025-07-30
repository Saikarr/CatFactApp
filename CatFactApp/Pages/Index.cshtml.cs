using CatFactApp.Models;
using CatFactApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatFactApp.Pages
{
    public class IndexModel : PageModel
    {
		private readonly ICatFactService _catFactService;
		private readonly IFileService _fileService;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ICatFactService catFactService, IFileService fileService, ILogger<IndexModel> logger)
		{
			_catFactService = catFactService;
			_fileService = fileService;
			_logger = logger;
		}

		[BindProperty]
		public CatFactResponse? CurrentCatFact { get; set; }

		public string? ErrorMessage { get; set; }
		public string? SuccessMessage { get; set; }

		public void OnGet()
		{
			
		}

		public async Task<IActionResult> OnPostGetCatFactAsync()
		{
			try
			{
				_logger.LogInformation("User requested a cat fact");

				CurrentCatFact = await _catFactService.GetCatFactAsync();

				if (CurrentCatFact == null)
				{
					ErrorMessage = "Requesting a cat fact from API resulted in failure. Try again.";
					return Page();
				}

				await _fileService.SaveCatFactToFileAsync(CurrentCatFact);

				SuccessMessage = "Cat fact received and saved";
				_logger.LogInformation("Cat fact received and saved successfully");

				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while requesting a cat fact");
				ErrorMessage = "There was an error while requesting a cat fact. Try again.";
				return Page();
			}
		}
	}
}
