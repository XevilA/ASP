using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อลูกค้า")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอกอีเมล")]
        [EmailAddress(ErrorMessage = "รูปแบบอีเมลไม่ถูกต้อง")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณาเลือกวันที่จอง")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "กรุณาเลือกเวลา")]
        [DataType(DataType.Time)]
        public DateTime ReservationTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "กรุณาระบุจำนวนคน")]
        [Range(1, 10, ErrorMessage = "จำนวนคนต้องอยู่ระหว่าง 1-10 คน")]
        public int NumberOfPeople { get; set; } = 2;

        [Required(ErrorMessage = "กรุณาเลือกหมายเลขโต๊ะ")]
        public string TableNumber { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}