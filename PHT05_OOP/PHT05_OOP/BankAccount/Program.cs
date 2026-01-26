using System;

namespace Encapsulation
{
    public class BankAccount
    {
        private double _balance; // che giấu dữ liệu

        public double Balance { get { return _balance; } } // chỉ đọc

        public void Deposit(double amount)
        {
            if (amount <= 0) throw new Exception("Số tiền gửi phải > 0");
            _balance += amount;
            Console.WriteLine($"Gửi thành công: {amount}, Số dư hiện tại: {_balance}");
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new Exception("Số tiền rút phải > 0");
            if (amount > _balance) throw new Exception("Số dư không đủ");
            _balance -= amount;
            Console.WriteLine($"Rút thành công: {amount}, Số dư còn lại: {_balance}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        { // Cho phép hiển thị tiếng Việt
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            BankAccount account = new BankAccount();

            account.Deposit(1000);
            account.Withdraw(200);
            Console.WriteLine($"Số dư cuối: {account.Balance}");
        }
    }
}
