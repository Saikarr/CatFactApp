using CatFactApp.Models;

namespace CatFactApp.Services
{
	public interface IFileService
	{
		Task SaveCatFactToFileAsync(CatFactResponse catFact);
	}
}
