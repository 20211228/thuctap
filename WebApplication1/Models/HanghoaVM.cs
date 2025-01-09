namespace WebApplication1.Models
{
    public class HanghoaVM
    {
        public string TenHangHoa { get; set; }
        public double Dongia { get; set; }
    }

    public class Hanghoa : HanghoaVM
    {
        public Guid Mahanghoa { get; set; }
    }
}
