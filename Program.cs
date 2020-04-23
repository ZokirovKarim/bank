using System;
using System.Data;
using System.Data.SqlClient;

namespace bank
{
    class Program
    {
        private static bool IsAdmin { get; set; }
        private static int UserID { get; set; }
        private static int Score { get; set; }


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
                UserID = int.Parse(rd.GetValue("Id").ToString());
                if (rd["RolId"].ToString() == "1")
                {
                    AdminPanel(con);

                }
                else
                {
                    ClientPanel(con);

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
            string cm = $"select * from UserInfo where Login = {Phone}";
            command.CommandText = cm;
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                UserID = int.Parse(rd.GetValue("Id").ToString());

            }

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
        static void AdminPanel(SqlConnection con)
        {
            Console.Clear();
            bool ss = true;
            while (ss)
            {

                Console.WriteLine("" +
                "1.Список клиентов\n" +
                "2.Удалить клиента\n" +
                "3.Выход\n"
                );
                String ad = Console.ReadLine();
                switch (ad)
                {
                    case "1": ClientPanel(con); break;
                    case "2": break;
                    case "3": break;
                }

            }
        }
        static void ClientPanel(SqlConnection con)
        {
            Console.WriteLine("" +
            "1.Запольнение заявки\n" +
            "2.Погошение кридита\n" +
            "3.Выход\n");
            string cli = Console.ReadLine();
            switch (cli)
            {
                case "1": AddAnketaList(con); break;
                case "2": break;
                case "3": break;
            }
        }
        static void AddAnketaList(SqlConnection con)
        {
            int UserInfoId = UserID;
            Console.WriteLine("А Н К Е Т А");
            Console.WriteLine("Выбирите поль\n" +
            "1.Муж\n" +
            "2.Жен\n");
            char pol = Console.ReadKey().KeyChar;
            int FloorId = pol == '1' ? 1 : 2;
            if (pol == 1)
            {
                Score++;
            }
            else
            {
                Score += 2;
            }

            Console.WriteLine("Серия и Номер Паспорта");
            string Pasport = Console.ReadLine();

            Console.WriteLine("Семейные положение Выберите из списка\n" +
            "1.Холост\n" +
            "2.Семеянин\n" +
            "3.Вразводе\n" +
            "4.Вдовец/Вдова\n");
            char Mir = Console.ReadKey().KeyChar;
            int MaritalStatusId = int.Parse(Mir.ToString());
            if (Mir == 1)
            {
                Score++;
            }
            if (Mir == 2)
            {
                Score += 2;
            }
            if (Mir == 3)
            {
                Score++;
            }
            if (Mir == 4)
            {
                Score += 2;
            }
            Console.WriteLine("Сколько ва лет");
            int Age = int.Parse(Console.ReadLine());
            if (Age <= 25)
            {


            }
            if (Age >= 26 && Age <= 35)
            {
                Score++;
            }
            if (Age >= 36 && Age <= 62)
            {
                Score += 2;
            }
            if (Age > 63)
            {
                Score++;
            }
            Console.WriteLine("Гражданин какой Вы страны");
            Console.WriteLine("1.Таджикистан\n" +
            "2.зарубеж\n");
            char Mir2 = Console.ReadKey().KeyChar;
            int CitizenshipId = int.Parse(Mir2.ToString());

            if (CitizenshipId == 1)
            {
                Score++;

            }


            Console.WriteLine("Цель Вашого кредита" +
            "1.Бытовая техника\n" +
            "2.Ремонт\n" +
            "3.Телефон\n" +
            "4.Прочие\n");
            char cred = Console.ReadKey().KeyChar;
            int CredGoalId = int.Parse(cred.ToString());
            if (cred == 1)
            {
                Score += 2;
            }
            if (cred == 2)
            {
                Score++;
            }
            if (cred == 4)
            {
                Score = -1;
            }
            Console.WriteLine("Выберите из списка, срок кредита по месяцам ");
            Console.WriteLine("" +
            "1. 6 Месяцев\n" +
            "2. 9 Месяцев\n" +
            "3. 12 Месяцев\n" +
            "4. 24 Месяцев");
            string CredSroc = Console.ReadLine();
            Score++;


            Console.WriteLine("Укажите сумму Вашаго зарплаты");
            double Zarplata = int.Parse(Console.ReadLine());

            Console.WriteLine("Укажите сумму кредита");
            double SummaCredit = int.Parse(Console.ReadLine());

            if (Zarplata * 0.8 > SummaCredit) { Score += 4; }
            if (Zarplata * 0.8 <= SummaCredit && SummaCredit * 1.5 > SummaCredit) { Score += 3; }
            if (Zarplata * 1.5 <= SummaCredit && Zarplata * 2.5 >= Zarplata) { Score += 2; }
            if (Zarplata * 2.5 < SummaCredit) { Score += 1; }






            AddAnketa(UserInfoId, Zarplata, Pasport, FloorId, MaritalStatusId, Age, CitizenshipId, CredGoalId, CredSroc, SummaCredit, con);
        }
        static void AddAnketa(int UserInfoId, double Zarplata, string Pasport, int FloorId, int MaritalStatusId, int Age, int CitizenshipId, int CredGoalId, string CredSroc, double SummaCredit, SqlConnection con)
        {
            con.Close();
            con.Open();
            string inserAddAnketa = $"insert into Application(Zarplata, UserInfoId, Pasport, FloorId, MaritalStatusId, Age,CitizenshipId,credPros,CredGoalId,CredSroc,SummaCredit) values('{Zarplata}''{UserInfoId}','{Pasport}''{FloorId}','{MaritalStatusId}','{Age}','{CitizenshipId}','{CredGoalId}','{CredSroc}','{SummaCredit}')";
            SqlCommand command = new SqlCommand(inserAddAnketa, con);
            command = new SqlCommand(inserAddAnketa, con);
            var result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Успешно запольнена анкета");
                if (Score > 1.2)
                {
                    Console.WriteLine("Вам разрешенно на кредит");
                }
                else
                {
                    Console.WriteLine("Вам отказанно на кредить");
                }

            }
            con.Close();


        }
        static void ClientPanel()
        {

        }



    }
}
