using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FudeeTestApi.Data;
using FudeeTestApi.Models;
using FudeeTestApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using FudeeTestApi.Infrastructure;
using System.Data.Common;

namespace FudeeTestApi.Controllers
{
	
	public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private IWebHostEnvironment _hostEnvironment;

		public RestaurantsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(string Fraza, string Manager, int? Kategoria, int PageNumber = 1)
        {
			var SelectedRestaurants = _context.Restaurants?
				.Include(t => t.Category)
				.Include(t => t.User)
				.OrderByDescending(t => t.AddedDate);

			if (Kategoria != null)
			{
				SelectedRestaurants = (IOrderedQueryable<Restaurant>)SelectedRestaurants.Where(r => r.Category.CategoryId == Kategoria);
			}
			if (!String.IsNullOrEmpty(Manager))
			{
				SelectedRestaurants = (IOrderedQueryable<Restaurant>)SelectedRestaurants.Where(r => r.User.Id == Manager);
			}
			if (!String.IsNullOrEmpty(Fraza))
			{
				SelectedRestaurants = (IOrderedQueryable<Restaurant>)SelectedRestaurants.Where(r => r.RestaurantDescription.Contains(Fraza));
			}

			RestaurantsViewModel restaurantsViewModel = new();
			restaurantsViewModel.RestaurantsView = new RestaurantsView();

			restaurantsViewModel.RestaurantsView.RestaurantCount = SelectedRestaurants.Count();
			restaurantsViewModel.RestaurantsView.PageNumber = PageNumber;
			restaurantsViewModel.RestaurantsView.Manager = Manager;
			restaurantsViewModel.RestaurantsView.Phrase = Fraza;
			restaurantsViewModel.RestaurantsView.Category = Kategoria;


			restaurantsViewModel.Restaurants = (IEnumerable<Restaurant>?)await SelectedRestaurants
				.Skip((PageNumber - 1) * restaurantsViewModel.RestaurantsView.PageSize)
				.Take(restaurantsViewModel.RestaurantsView.PageSize)
				.ToListAsync();

			ViewData["Category"] = new SelectList(_context.Categories?
				.Where(c => c.Active == true),
				"CategoryId", "Name", Kategoria);

			ViewData["Manager"] = new SelectList(_context.Restaurants
				.Include(u => u.User)
				.Select(u => u.User)
				.Distinct(),
				"Id", "FullName", Manager);

			return View(restaurantsViewModel);
		}

		// GET: List
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.Restaurants.Include(r => r.Address).Include(r => r.Category).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: ListSelektywna
     
        public async Task<IActionResult> ListSelektywna()
        {

            var applicationDbContext = _context.Restaurants.Include(r => r.Address).Include(r => r.Category).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Restaurants == null)
			{
				return NotFound();
			}

			var restaurant = await _context.Restaurants
				.Include(r => r.Address)
				.Include(r => r.Category)
				.Include(r => r.User)
				.FirstOrDefaultAsync(m => m.RestaurantId == id);
			if (restaurant == null)
			{
				return NotFound();
			}
			

            RestaurantWithOpinions restaurantWithOpinions = new RestaurantWithOpinions();

			restaurantWithOpinions.SelectedRestaurant = await _context.Restaurants
				.Include(r => r.Category)
				.Include(r => r.User)
				.Include(r => r.Address)
                .Include(r => r.Dishes)
                .Include(r => r.Opinions)
				.ThenInclude(c => c.User)
				.FirstOrDefaultAsync(m => m.RestaurantId == id);

			if (restaurantWithOpinions.SelectedRestaurant == null)
			{
				return NotFound();
			}
            restaurantWithOpinions.NewOpinion = new Opinion { RestaurantId = (int)id, Id = restaurantWithOpinions.SelectedRestaurant.Id };

            restaurantWithOpinions.CommentsNumber = _context.Opinions
				.Where(x => x.RestaurantId == id)
				.Count();

			if (restaurantWithOpinions.CommentsNumber != 0)
			{
				restaurantWithOpinions.OpinionsNumber = _context.Opinions.Where(o => o.RestaurantId == id).Where(x => x.Rating != null).Count();
				if (restaurantWithOpinions.OpinionsNumber != 0)
				{
					restaurantWithOpinions.AverageScore = (float)(restaurantWithOpinions.OpinionsNumber > 0 ? _context.Opinions.Where(o => o.RestaurantId == id).Where(x => x.Rating != null).Average(x => (int)x.Rating) : 0);
				}
			}
            restaurantWithOpinions.Menu = new Dish { RestaurantId = restaurantWithOpinions.SelectedRestaurant.RestaurantId };

            restaurantWithOpinions.Description = Variaty.Phrase("komentarz", "komentarze", "komentarzy", restaurantWithOpinions.CommentsNumber);

			return View(restaurantWithOpinions);
		}

		// GET: Restaurants/Create
		[Authorize(Roles = "admin, manager")]
		public IActionResult Create()
        {
            
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "NameCategory");
            return View();
        }

		// POST: Restaurants/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin, manager")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,RestaurantName,AddressId,Logo,RestaurantDescription,OpenHours,CloseHours,HasDelivery,HasCatering,Events,CategoryId,SocialMedia")] Restaurant restaurant, IFormFile? logo)
        {
            if (ModelState.IsValid)
            {
				restaurant.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
				restaurant.AddedDate = DateTime.Now;

				if (logo != null && logo.Length > 0)
				{
					ImageFileUpload imageFileResult = new(_hostEnvironment);
					FileSendResult fileSendResult = imageFileResult.SendFile(logo, "img", 600);
					if (fileSendResult.Success)
					{
						restaurant.Logo = fileSendResult.Name;
					}
					else
					{
						ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
						ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "NameCategory", restaurant.CategoryId);
						return View(restaurant);
					}
				}

				_context.Add(restaurant);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "NameCategory", restaurant.CategoryId);
			return View(restaurant);
		}

		// GET: Restaurants/Edit/5
		[Authorize(Roles = "admin, manager")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

			if (string.Compare(User.FindFirstValue(ClaimTypes.NameIdentifier), restaurant.Id) == 0 || User.IsInRole("admin"))
			{
				ViewData["AddressId"] = restaurant.Address;
				ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "NameCategory", restaurant.CategoryId);
				ViewData["Id"] = restaurant.Id;
				return View(restaurant);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: Restaurants/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin, manager")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantId,RestaurantName,AddressId,Logo,RestaurantDescription,OpenHours,CloseHours,HasDelivery,HasCatering,Events,Id,CategoryId,SocialMedia,AddedDate")] Restaurant restaurant, IFormFile? logo)
        {
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
			{ 
				if(logo != null && logo.Length > 0)
				{
					ImageFileUpload imageFileResult = new(_hostEnvironment);
					FileSendResult fileSendResult = imageFileResult.SendFile(logo, "img", 600);
					if (fileSendResult.Success)
					{
						restaurant.Logo = fileSendResult.Name;
					}
					else
					{
						ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
						ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", restaurant.CategoryId);
						ViewData["Manager"] = restaurant.Id;
						return View(restaurant);
					}
				}
				try
				{
					_context.Update(restaurant);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RestaurantExists(restaurant.RestaurantId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["AddressId"] = restaurant.Address;
			ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "DescriptionCategory", restaurant.CategoryId);
			ViewData["Id"] = restaurant.Id;
			return View(restaurant);
			
        }

		// GET: Restaurants/Delete/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

		// POST: Restaurants/Delete/5
		[Authorize(Roles = "admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurants'  is null.");
            }
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
          return _context.Restaurants.Any(e => e.RestaurantId == id);
        }
    }
}
