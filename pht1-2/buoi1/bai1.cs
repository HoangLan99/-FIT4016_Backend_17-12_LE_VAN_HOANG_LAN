// TODO 1: Khai báo namespace
using System;

namespace buoi1
{
    // TODO 2: Khai báo class
    class bai1
    {
        // TODO 3: Khai báo Main method (điểm khởi đầu của chương trình)
        static void Main(string[] args)
        {
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
}