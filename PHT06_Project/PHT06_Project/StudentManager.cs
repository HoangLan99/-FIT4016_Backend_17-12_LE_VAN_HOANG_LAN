using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHT06_Project
{
    public class StudentManager
    {
        private Student[] students = new Student[50];
        private int count = 0;

        // Thêm sinh viên
        public void AddStudent(string id, string name, double score)
        {
            if (count >= 50)
                throw new Exception("Danh sách đã đầy");

            if (FindStudentById(id) != null)
                throw new Exception("ID sinh viên đã tồn tại");

            students[count++] = new Student(id, name, score);
            Console.WriteLine("✅ Thêm sinh viên thành công");
        }

        // Xóa sinh viên theo ID
        public void RemoveStudent(string id)
        {
            for (int i = 0; i < count; i++)
            {
                if (students[i].StudentId == id)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        students[j] = students[j + 1];
                    }
                    students[--count] = null;
                    Console.WriteLine("✅ Xóa sinh viên thành công");
                    return;
                }
            }
            throw new Exception("Không tìm thấy sinh viên");
        }

        // Cập nhật điểm
        public void UpdateScore(string id, double newScore)
        {
            if (newScore < 0 || newScore > 10)
                throw new Exception("Điểm phải từ 0 - 10");

            Student s = FindStudentById(id);
            if (s == null)
                throw new Exception("Không tìm thấy sinh viên");

            s.Score = newScore;
            Console.WriteLine("✅ Cập nhật điểm thành công");
        }

        // Điểm trung bình
        public double GetAverageScore()
        {
            if (count == 0) return 0;

            double sum = 0;
            for (int i = 0; i < count; i++)
                sum += students[i].Score;

            return sum / count;
        }

        // Điểm cao nhất
        public double GetMaxScore()
        {
            if (count == 0)
                throw new Exception("Danh sách rỗng");

            double max = students[0].Score;
            for (int i = 1; i < count; i++)
                if (students[i].Score > max)
                    max = students[i].Score;

            return max;
        }

        // Tìm sinh viên theo ID
        public Student FindStudentById(string id)
        {
            for (int i = 0; i < count; i++)
                if (students[i].StudentId == id)
                    return students[i];

            return null;
        }

        // In danh sách sinh viên
        public void DisplayAllStudents()
        {
            if (count == 0)
            {
                Console.WriteLine("Danh sách sinh viên trống");
                return;
            }

            Console.WriteLine("\n📋 DANH SÁCH SINH VIÊN:");
            for (int i = 0; i < count; i++)
                students[i].Display();
        }
    }
}
