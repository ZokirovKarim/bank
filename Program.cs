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
        private static bool work { get; set; }
        private static bool Reg { get; set; }




        static void Main(string[] args)
        {
            Reg = true;
            work = true;
            string ConString = @"Data Source=localhost; Initial Catalog=bankAlif;Integrated Security=true";
            SqlConnection con = new SqlConnection(ConString);
            while (Reg)
            {
                Console.WriteLine("" +
                "1.Регистрация\n" +
                "2.Вход\n" +
                "3.Выход");
                string ris = Console.ReadLine();
                switch (ris)
                {
                    case "1": Registration(con); break;
                    case "2": interUser(con); break;
                    case "3": Reg = false; break;
                }
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
                    con.Close();
                    AdminPanel(con);
                    break;
                }
                else
                {
                    con.Close();
                    ClientPanel(con);
                    break;

                }
                r++;
                Console.WriteLine("Добро пожаловать");
                IsAdmin = true;
            }
            if (r == 0)
            {
                con.Close();
                Console.WriteLine("Логин или пароль был введён неправильно!!!!");
            }
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
            con.Close();
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
                    case "1": ClientList(con); break;
                    case "2":; break;
                    case "3": break;
                }

            }
        }
        static void ClientList(SqlConnection con)
        {
            con.Open();
            string Cl = string.Format($"select * from UserInfo where RolId={2}");
            SqlCommand command = new SqlCommand(Cl, con);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                System.Console.WriteLine($"Name: {reader.GetValue("Name")}SurName: {reader.GetValue("SurName")}MiddleName: {reader.GetValue("MiddleName")}");
            }
        }
        static void ClientPanel(SqlConnection con)
        {
            while (work)
            {
                Console.WriteLine("" +
                "1.Запольнение заявки\n" +
                "2.Погошение кридита\n" +
                "3.Выход\n");
                string cli = Console.ReadLine();
                switch (cli)
                {
                    case "1": AddAnketaList(con); break;
                    case "2": Graphik(con); break;
                    case "3": work = false; break;
                }
            }
            static void AddAnketaList(SqlConnection con)
            {
                int UserInfoId = UserID;
                Console.WriteLine("А Н К Е Т А");
                Console.WriteLine("Выбирите пол\n" +
                "1.Муж\n" +
                "2.Жен\n");
                int FloorId = int.Parse(Console.ReadLine());
                if (FloorId == 1)
                {
                    Score++;
                }
                else
                {
                    Score += 2;
                }
                Console.WriteLine("Семейные положение Выберите из списка\n" +
                "1.Холост\n" +
                "2.Семеянин\n" +
                "3.Вразводе\n" +
                "4.Вдовец/Вдова\n");
                int MaritalStatusId = int.Parse(Console.ReadLine());
                if (MaritalStatusId == 1)
                {
                    Score++;
                }
                if (MaritalStatusId == 2)
                {
                    Score += 2;
                }
                if (MaritalStatusId == 3)
                {
                    Score++;
                }
                if (MaritalStatusId == 4)
                {
                    Score += 2;
                }
                Console.WriteLine("Сколько вам лет");
                int Age = int.Parse(Console.ReadLine());
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
                int CitizenshipId = int.Parse(Console.ReadLine());
                if (CitizenshipId == 1)
                {
                    Score++;
                }
                Console.WriteLine("Цель Вашого кредита\n" +
                "1.Бытовая техника\n" +
                "2.Ремонт\n" +
                "3.Телефон\n" +
                "4.Прочие\n");
                int CredGoalId = int.Parse(Console.ReadLine());
                if (CredGoalId == 1)
                {
                    Score += 2;
                }
                if (CredGoalId == 1)
                {
                    Score++;
                }
                if (CredGoalId == 1)
                {
                    Score--;
                }
                Console.WriteLine("Выберите из списка, срок кредита по месяцам ");
                Console.WriteLine("" +
                "1. 6 Месяцев\n" +
                "2. 9 Месяцев\n" +
                "3. 12 Месяцев\n" +
                "4. 24 Месяцев");
                string CredSroc = Console.ReadLine();
                Score++;
                Console.WriteLine("Укажите сумму Вашей зарплаты");
                double Zarplata = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите сумму кредита");
                double SummaCredit = int.Parse(Console.ReadLine());
                if (Zarplata * 0.8 > SummaCredit) { Score += 4; }
                if (Zarplata * 0.8 <= SummaCredit && SummaCredit * 1.5 > SummaCredit) { Score += 3; }
                if (Zarplata * 1.5 <= SummaCredit && Zarplata * 2.5 >= Zarplata) { Score += 2; }
                if (Zarplata * 2.5 < SummaCredit) { Score += 1; }
                AddAnketa(UserInfoId, Zarplata, FloorId, MaritalStatusId, Age, CitizenshipId, CredGoalId, CredSroc, SummaCredit, con);
            }
            static void AddAnketa(int UserInfoId, double Zarplata, int FloorId, int MaritalStatusId, int Age, int CitizenshipId, int CredGoalId, string CredSroc, double SummaCredit, SqlConnection con)
            {
                con.Open();
                string inserAddAnketa = $"insert into Application(Zarplata, UserInfoId, FloorId, MaritalStatusId, Age,CitizenshipId,CredGoalId,CredSroc,SummaCredit,Status) values({Zarplata},{UserInfoId},{FloorId},{MaritalStatusId},{Age},{CitizenshipId},{CredGoalId},'{CredSroc}',{SummaCredit},'НеОдобрено')";
                if (Score > 12)
                {
                    inserAddAnketa = $"insert into Application(Zarplata, UserInfoId,  FloorId, MaritalStatusId, Age,CitizenshipId,CredGoalId,CredSroc,SummaCredit,Status) values({Zarplata},{UserInfoId},{FloorId},{MaritalStatusId},{Age},{CitizenshipId},{CredGoalId},'{CredSroc}',{SummaCredit},'Одобрено')";
                    Console.WriteLine("Вам разрешенно на кредит");
                }
                else
                {
                    Console.WriteLine("Вам отказанно на кредить");
                }
                SqlCommand command = new SqlCommand(inserAddAnketa, con);
                command = new SqlCommand(inserAddAnketa, con);
                var result = command.ExecuteNonQuery();
                Console.WriteLine("Успешно запольнена анкета");
                con.Close();
            }
        }
        static void Graphik(SqlConnection con)
        {
            con.Open();
            System.Console.WriteLine("========================");
            System.Console.WriteLine("График");
            System.Console.WriteLine("========================");
            string cmo = string.Format($"select * from Application where UserInfoId={UserID} and Status='Одобрено'");
            SqlCommand command = new SqlCommand(cmo, con);
            SqlDataReader reader = command.ExecuteReader();
            decimal Value = 0;
            decimal Month = 0;
            while (reader.Read())
            {
                Value = decimal.Parse(reader["SummaCredit"].ToString());
                Month = decimal.Parse(reader["CredSroc"].ToString());
                System.Console.WriteLine($"Вы должны платить {Value / Month} в течении {reader["CredSroc"].ToString()} месяцев Итог{reader["SummaCredit"].ToString()}");
            }
            System.Console.WriteLine("==================");
            con.Close();
        }
    }
}
