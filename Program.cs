using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Data;

var builder = WebApplication.CreateBuilder(args);

// เพิ่มบรรทัดเหล่านี้
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// เพิ่ม middleware สำหรับ Session
app.UseSession();

// ... ส่วนที่เหลือเหมือนเดิม ...
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reservation}/{action=Create}/{id?}");

app.Run();