using System;

using System.Text;


//class Program

//{

//    // TODO 1: Viết hàm XepLoai nhận vào điểm và trả về xếp loại (string)

//    static string XepLoai(double diem)

//    {

//        if (diem >= 8.5)

//        {

//            return "Giỏi";

//        }

//        else if (diem >= 7.0)

//        {

//            return "Khá";

//        }

//        else if (diem >= 5.5)

//        {

//            return "Trung bình";

//        }

//        else

//        {

//            return "Yếu";

//        }

//    }


//    // TODO 2: Viết hàm TinhTrungBinh nhận vào mảng điểm và trả về trung bình (double)

//    static double TinhTrungBinh(double[] diem)

//    {

//        double tong = 0;


//        for (int i = 0; i < diem.Length; i++)

//        {

//            tong += diem[i];

//        }
//        if (diem.Length > 0)

//        {

//            return tong / diem.Length;

//        }

//        return 0;

//    }


//    static void InBangDiem(string[] ten, double[] diem)

//    {

//        Console.WriteLine("---------------------------------------------");

//        Console.WriteLine($"| {"Họ và Tên",-20} | {"Điểm",-5} | {"Xếp Loại",-10} |");

//        Console.WriteLine("---------------------------------------------");


//        for (int i = 0; i < ten.Length; i++)

//        {


//            string loai = XepLoai(diem[i]);



//            Console.WriteLine($"| {ten[i],-20} | {diem[i],-5} | {loai,-10} |");

//        }

//        Console.WriteLine("---------------------------------------------");

//    }


//    static void Main()

//    {

//        Console.OutputEncoding = Encoding.UTF8;


//        string[] tenSV = { "Phạm Đức Long", "Nguyễn Quốc Hưng", "Trần Hồng Quân" };

//        double[] diemSV = { 10, 7.9, 5.5 };


//        Console.WriteLine("=== Chương trình Quản lý Sinh viên ===\n");


//        InBangDiem(tenSV, diemSV);


//        double trungBinh = TinhTrungBinh(diemSV);

//        Console.WriteLine($"\nĐiểm trung bình lớp: {trungBinh:F2}");

//        Console.ReadLine();

//    }

//}



namespace HelloWorldCSharp

{


    public class SinhVien

    {

        public string HoTen { get; set; }

        public int Tuoi { get; set; }

        public double Diem { get; set; }


        public SinhVien(string hoTen, int tuoi, double diem)

        {

            HoTen = hoTen;

            Tuoi = tuoi;

            Diem = diem;

        }


        public string XepLoai()

        {

            if (Diem >= 8.5) return "Giỏi";

            else if (Diem >= 7.0) return "Khá";

            else if (Diem >= 5.5) return "Trung bình";

            else return "Yếu";

        }


        public void HienThiThongTin()

        {

            Console.WriteLine($"| {HoTen,-20} | {Tuoi,-4} | {Diem,-4} | {XepLoai(),-10} |");

        }

    }


    class Program

    {

        static void Main(string[] args)

        {

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Quản lý Sinh viên ===\n");


            SinhVien sv1 = new SinhVien("Phạm Đức Long", 21, 10);

            SinhVien sv2 = new SinhVien("Trần Hồng Quân", 21, 7.9);

            SinhVien sv3 = new SinhVien("Nguyễn Quốc Hưng", 21, 5.5);


            Console.WriteLine($"| {"Họ và Tên",-20} | {"Tuổi",-4} | {"Điểm",-4} | {"Xếp Loại",-10} |");

            Console.WriteLine(new string('-', 53));


            sv1.HienThiThongTin();

            sv2.HienThiThongTin();

            sv3.HienThiThongTin();

        }

    }

}

