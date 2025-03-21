using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RestaurantBooking.Data;
using RestaurantBooking.Models;

namespace RestaurantBooking.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminEmail = "dev@dotmini.in.th";
        private const string AdminPassword = "zxcv1234";
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // หน้า Login
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email == AdminEmail && password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index");
            }
            ViewBag.Error = "อีเมลหรือรหัสผ่านไม่ถูกต้อง";
            return View();
        }

        // หน้า Dashboard
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var reservations = _context.Reservations
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return View(reservations);
        }

        // ยืนยันการจอง
        [HttpPost]
        public async Task<IActionResult> Confirm(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = "Confirmed";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ลบการจอง
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ออกจากระบบ
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }
    }
}