using Microsoft.AspNetCore.Mvc;
using RestaurantBooking.Data;
using RestaurantBooking.Models;

namespace RestaurantBooking.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (!ModelState.IsValid) return View(reservation);

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Confirmation", new { id = reservation.Id });
        }

        public IActionResult Confirmation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            return View(reservation);
        }

        public IActionResult MyReservations(string email)
        {
            var reservations = _context.Reservations
                .Where(r => r.Email == email)
                .OrderByDescending(r => r.ReservationDate)
                .ToList();
            return View(reservations);
        }
    }
}