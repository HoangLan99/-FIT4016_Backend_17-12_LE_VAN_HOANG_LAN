using System;

class Program
{
    static void Main()
    {
        // Cho phép hiển thị tiếng Việt
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Chương trình Xếp loại Sinh viên ===\n");

        // TODO 1: Khai báo biến thông tin sinh viên
        string hoVaTen = "Phạm Đức Long";
        double diem = 10;

        // TODO 2: In ra thông tin sinh viên
        Console.WriteLine($"Họ tên: {hoVaTen}");
        Console.WriteLine($"Điểm: {diem}");

        // TODO 3: Viết cấu trúc if/else if/else để xếp loại
        Console.Write("Xếp loại cá nhân: ");
        if (diem >= 8.5)
        {
            Console.WriteLine("Giỏi");
        }
        else if (diem >= 7.0)
        {
            Console.WriteLine("Khá");
        }
        else if (diem >= 5.5)
        {
            Console.WriteLine("Trung bình");
        }
        else
        {
            Console.WriteLine("Yếu");
        }

        // TODO 4: Viết vòng lặp for để in ra bảng điểm của 3 sinh viên
        string[] tenSV = { "Nguyễn Văn A", "Trần Thị B", "Lê Văn C" };
        double[] diemSV = { 8.5, 7.2, 5.8 };

        Console.WriteLine("\n=== Bảng Điểm Lớp ===");

        for (int i = 0; i < tenSV.Length; i++)
        {
            // TODO 5: In ra tên, điểm và xếp loại của từng sinh viên
            string xepLoai = "";
            if (diemSV[i] >= 8.5) xepLoai = "Giỏi";
            else if (diemSV[i] >= 7.0) xepLoai = "Khá";
            else if (diemSV[i] >= 5.5) xepLoai = "Trung bình";
            else xepLoai = "Yếu";

            Console.WriteLine($"{i + 1}. {tenSV[i]} - Điểm: {diemSV[i]} - Xếp loại: {xepLoai}");
        }

        // TODO 6: (Tùy chọn) Dùng while loop để tính tổng điểm
        double tongDiem = 0;
        int j = 0;
        while (j < diemSV.Length)
        {
            tongDiem = tongDiem + diemSV[j];
            j++;
        }

        Console.WriteLine($"\nTổng điểm: {tongDiem}");
        Console.WriteLine($"Điểm trung bình: {tongDiem / diemSV.Length:F2}");

    }
}
