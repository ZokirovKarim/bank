using System;
using System.Data.SqlClient;

namespace bank
{
    class Program
    {
        private static bool IsAdmin { get; set; }
        static void Main(string[] args)
        {
            string ConString = @"Data Source=localhost; Initial Catalog=bankAlif;Integrated Security=true";
            SqlConnection con = new SqlConnection(ConString);



            
                Console.WriteLine("" +
                "1.Регистрация\n" +
                "2.Вход\n" +
                "3.Выход");
                string ris = Console.ReadLine();
                switch (ris)
                {
                    case "1": Registration(con); break;
                    case "2": interUser(con); break;
                    case "3": break;
                }

            
           


        }
        static void interUser(SqlConnection com)
        {
            Console.Clear();
            Console.WriteLine("Введите Логин");
            string Login = Console.ReadLine();
            Console.WriteLine("Введите Пароль");
            string Pass = Console.ReadLine();


            SignIn(Login, Pass, com);


        }
        static void SignIn(string Login, string Pass, SqlConnection con)
        {
            con.Open();
            string cm = $"select * from UserInfo where Login='{Login}' and Pass='{Pass}'";
            SqlCommand cmd = new SqlCommand(cm, con);
            SqlDataReader rd = cmd.ExecuteReader();
            int r = 0;
            while (rd.Read())
            {
                if (rd["RolId"].ToString()=="1")
                {
                    AdminPanel();
           
                }
                else    
                {
                    ClientPanel();

                }
                r++;
                Console.WriteLine("Дабро пожаловать");
                IsAdmin = true;
               
            }
            if (r == 0)
            {
                Console.WriteLine("Логин или пароль был введён неправильно!!!!");
            }
            con.Close();
        }
        static void Registration(SqlConnection con)
        {
            Console.Clear();
            Console.WriteLine("РЕГИСТРАЦИЯ");
            Console.Write("Фамилия:");
            string SurName = Console.ReadLine();
            Console.Write("Имя:");
            string Name = Console.ReadLine();
            Console.Write("Отчество:");
            string MiddleName = Console.ReadLine();

            Console.WriteLine("Номер телефон будеть использоватся как Логин");
            int Phone = int.Parse(Console.ReadLine());
            Console.WriteLine("Пароль");
            string pass = Console.ReadLine();
            Console.WriteLine("Павторине пароль");
            string pass2 = Console.ReadLine();
            if (pass == pass2)
            {

                InsertRegistration(SurName, Name, MiddleName, Phone, pass, con);

            }
            else
            {
                Console.WriteLine("Ваш пароль не савподаеть");
            }


        }
        static void InsertRegistration(string SurName, string Name, string MiddleName, int Phone, string pass, SqlConnection con)
        {
            Console.WriteLine("Выберите роль!");
            Console.WriteLine("" +
            "1.Администратор\n" +
            "2.Клиент\n");
            char x = Console.ReadKey().KeyChar;
            int rid = x == '1' ? 1 : 2;
            con.Open();
            string insertSqlCommand = $"insert into UserInfo(SurName, Name, MiddleName, Login,Pass,RolId,Phone) values('{SurName}','{Name}','{MiddleName}','{Phone}','{pass}',{rid},'{Phone}')";
            SqlCommand command = new SqlCommand(insertSqlCommand, con);
            command = new SqlCommand(insertSqlCommand, con);


            var result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.Clear();
                Console.WriteLine("Вы успешно прошли регистрацию");
                Console.WriteLine($"Фамилия: {SurName} Имя: {Name} Логин: {Phone} Пароль: {pass}");

            }
            else
            {
                Console.WriteLine("Неудачаный регистрация");
            }
            con.Close();



        }
        static void AdminPanel()
        {
            Console.Clear();
            Console.WriteLine(""+
            "1.Список клиентов"+
            "2.Удалить клиента"+
            );
            Console.ReadKey();
        }
        static void ClientPanel()
        {
            Console.Clear();
            Console.WriteLine("Клиент");
            Console.ReadKey();
        }


    }
}
