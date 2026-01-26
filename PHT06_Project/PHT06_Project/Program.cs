using PHT06_Project;
using System;

namespace PHT06_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cho phép hiển thị tiếng Việt
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            StudentManager manager = new StudentManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========== MENU ==========");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xóa sinh viên");
                Console.WriteLine("3. Cập nhật điểm");
                Console.WriteLine("4. In danh sách");
                Console.WriteLine("5. Tính điểm trung bình");
                Console.WriteLine("6. Tìm điểm cao nhất");
                Console.WriteLine("7. Tìm sinh viên theo ID");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("========================");
                Console.Write("Chọn: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("ID: ");
                            string id = Console.ReadLine();
                            Console.Write("Tên: ");
                            string name = Console.ReadLine();
                            Console.Write("Điểm: ");
                            double score = double.Parse(Console.ReadLine());
                            manager.AddStudent(id, name, score);
                            break;

                        case 2:
                            Console.Write("Nhập ID cần xóa: ");
                            manager.RemoveStudent(Console.ReadLine());
                            break;

                        case 3:
                            Console.Write("ID sinh viên: ");
                            string uid = Console.ReadLine();
                            Console.Write("Điểm mới: ");
                            double newScore = double.Parse(Console.ReadLine());
                            manager.UpdateScore(uid, newScore);
                            break;

                        case 4:
                            manager.DisplayAllStudents();
                            break;

                        case 5:
                            Console.WriteLine($"📊 Điểm trung bình: {manager.GetAverageScore():0.00}");
                            break;

                        case 6:
                            Console.WriteLine($"🏆 Điểm cao nhất: {manager.GetMaxScore()}");
                            break;

                        case 7:
                            Console.Write("Nhập ID: ");
                            Student s = manager.FindStudentById(Console.ReadLine());
                            if (s == null)
                                Console.WriteLine("Không tìm thấy sinh viên");
                            else
                                s.Display();
                            break;

                        case 0:
                            running = false;
                            Console.WriteLine("👋 Thoát chương trình");
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi: " + ex.Message);
                }
            }
        }
    }
}
