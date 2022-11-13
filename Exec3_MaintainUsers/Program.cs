using ISpan.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exec3_MaintainUsers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                      Member member = new Member();
            DateTime dateTime = new DateTime(1999,11,11);
            //member.Insert(1, "Allen", "abc123", "123456", dateTime, 175);
            member.Insert(5, "Allen", "abc123", "123456", dateTime, 175);
            //member.Delete(1);
            member.Select(1);
            member.Update(2, "John Wick", "wtf546", "abc789", new DateTime(1965, 01, 01), 185);
            member.Update(3, "Tom Cruise", "wtf1111", "abc0102", new DateTime(1935, 05, 05), 185);
        }

    }

    public  class Member
    {
        public  void Insert(int num,string Name,string Account,string Password, DateTime DateOfBirthd,int Height)
        {
            string sql = @"INSERT INTO Users(Name, Account, Password, DateOfBirthd, Height)
                                       VALUES(@Name, @Account, @Password, @DateOfBirthd, @Height)";
            var dbHelper = new SqlDbHelper("default");
            try
            {
                var parameters = new SqlParameterBuilder()
                    .AddNVarchar("Name", 50, Name)
                    .AddNVarchar("Account", 3000, Account)
                    .AddNVarchar("Password", 3000, Password)
                    .AddNDateTime("DateOfBirthd", DateOfBirthd)
                    .AddInt("Height", Height)
                    .Build();

                dbHelper.ExecuteNonQuery(sql, parameters);

                Console.WriteLine("記錄已新增");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
            }
        }


        public void Update(int num, string Name, string Account, string Password, DateTime DateOfBirthd, int Height)
        {
            string sql = @"UPDATE Users SET Name=@Name, Account=@Account, 
                         Password=@Password, DateOfBirthd=@DateOfBirthd, Height=@Height 
                         WHERE Id=@Id";

            var dbHelper = new SqlDbHelper("default");
            try
            {
                var parameters = new SqlParameterBuilder()
                    .AddNVarchar("Name", 50, Name)
                    .AddNVarchar("Account", 50, Account)
                    .AddNVarchar("Password", 50, Password)
                    .AddNDateTime("DateOfBirthd", DateOfBirthd)
                    .AddInt("Height", Height)
                    .AddInt("id", num)
                    .Build();

                dbHelper.ExecuteNonQuery(sql, parameters);

                Console.WriteLine("記錄已 update");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
            }
        }

        public void Delete(int id)
        {
            string sql = @"DELETE FROM Users WHERE Id=@Id";

            var dbHelper = new SqlDbHelper("default");
            try
            {
                var parameters = new SqlParameterBuilder()
                    .AddInt("id", id)
                    .Build();

                dbHelper.ExecuteNonQuery(sql, parameters);

                Console.WriteLine("記錄已 delete");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
            }
        }

        public void Select(int Id)
        {
            var dbHelper = new SqlDbHelper("default");
            string sql = "SELECT Id,Account FROM Users WHERE Id> @Id  ORDER BY Id ASC";
            try
            {
                var parameters = new SqlParameterBuilder().AddInt("Id", Id).Build();
                DataTable news = dbHelper.Select(sql, parameters);
                foreach (DataRow row in news.Rows)
                {
                    int id = row.Field<int>("id");
                    string Account = row.Field<string>("Account");
                    Console.WriteLine($"Id={id},Account={Account}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"連線失敗, 原因 :{ex.Message}");
            }
        }


    }




}
