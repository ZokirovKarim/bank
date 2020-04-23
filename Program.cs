using System;
using System.Data.SqlClient;

namespace bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConString = @"Data Source=localhost; Initial Catalog=bankAlif;Integrated Security=true";
            SqlConnection con = new SqlConnection(ConString);
            bool sss = true;
            while (sss)
            {
                Console.WriteLine("" +
                "1.Регистрация\n" +
                "2.Вход\n" +
                "3.Выход");
                string ris = Console.ReadLine();
                switch (ris)
                {
                    case "1": Registration( con); break;
                    case "2": break;
                    case "3": sss = false; break;
                }

            }

        }
        static void SignIn(SqlConnection con,string Login,string Pass)
        {
            string cm = $"select * from UserInfo where Login={Login}&&Pass={Pass}";
            SqlCommand cmd = new SqlCommand(cm,con);
            SqlDataReader rd = cmd.ExecuteReader();
            int r = 0;
            while(rd.Read())
            {
                r++;
            }
            if(r==0)
            {
                Console.WriteLine("Логин или пароль был введён неправильно!!!!");
            }
        }
        static void Registration( SqlConnection con)
        {
            Console.Clear();
            Console.WriteLine("РЕГИСТРАЦИЯ");
            Console.Write("Фамилия:");
            string SurName=Console.ReadLine();
            Console.Write("Имя:");
            string Name=Console.ReadLine();
            Console.Write("Отчество:");
            string MiddleName=Console.ReadLine();
          
            Console.WriteLine("Номер телефон будеть использоватся как Логин");
            int Phone=int.Parse(Console.ReadLine());
            Console.WriteLine("Пароль");
            string pass=Console.ReadLine();
            Console.WriteLine("Павторине пароль");
            string pass2=Console.ReadLine();1
            if (pass==pass2)
            {

            InsertRegistration(SurName,Name,MiddleName,Phone,pass,con);

            }
            else 
            {
                Console.WriteLine("Ваш пароль не савподаеть");
            }


        }
        
            


        }
        
    }
}
