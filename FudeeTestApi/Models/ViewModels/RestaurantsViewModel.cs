namespace FudeeTestApi.Models.ViewModels
{
	public class RestaurantsViewModel
	{
		public IEnumerable<Restaurant>? Restaurants { get; set; }
		public RestaurantsView? RestaurantsView { get; set; }
	}
}
