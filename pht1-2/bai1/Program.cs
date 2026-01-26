using System;

class Program
{
    static void Main(string[] args)
    {
        // Cho phép hiển thị tiếng Việt
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        // TODO 4: In ra một thông điệp
        Console.WriteLine("Chào mừng đến với C#!");

        // TODO 5: Khai báo 3 biến và hiển thị chúng
        string ten = "Nguyễn Văn A"; // Chuỗi ký tự
        int tuoi = 20;               // Số nguyên
        double diem = 8.5;           // Số thực

        Console.WriteLine("Tên: " + ten);
        Console.WriteLine("Tuổi: " + tuoi);
        Console.WriteLine("Điểm: " + diem);

        // TODO 6: Sử dụng string interpolation (cách hiện đại)
        Console.WriteLine($"Thông tin: {ten}, tuổi {tuoi}, điểm {diem}");
    }
}