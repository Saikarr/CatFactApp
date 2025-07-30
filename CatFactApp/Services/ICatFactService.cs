using CatFactApp.Models;

namespace CatFactApp.Services
{
	public interface ICatFactService
	{
		Task<CatFactResponse?> GetCatFactAsync();
	}
}
