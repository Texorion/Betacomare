using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using AppConsoleDB.Properties;
using System.Runtime.ConstrainedExecution;
using System.Data.Common;
using System.Collections;

namespace AppConsoleDB.Classes
{
    internal class DB
    {
        #region Connect to DB
        public static SqlConnection Connection()
        {
            SqlConnection sqlCnn = new SqlConnection(Resources.SqlConnection); //OGG connessione al DB con Resources che cerca una stringa simile a quella impostata
            if (sqlCnn.State == ConnectionState.Closed)
            {
                sqlCnn.Open();
            }

            return sqlCnn;
        }
        #endregion

        #region Read tables
        public static Azienda ConnectionAndRead()
        {
            SqlConnection sqlCnn = DB.Connection();

            Azienda azienda = new();

            Impiegato.ReadImpiegati(azienda.listImpiegati);
            AttivitaLavorativa.ReadAttivitaLavorative(azienda.listAttLav);
            OreStraordinario.ReadOreStraordinari(azienda.listOreStra);

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();

            return azienda;
        }
        #endregion

        #region Login
        public static KeyValuePair<string, bool> ConnectionAndLogin()
        {
            /* Open connectionto DB */
            SqlConnection sqlCnn = DB.Connection();

            SqlCommand sqlCmdLogin;
            int retry = 0;
            int checkLogin;
            string username = string.Empty;

            Stampa.PrintInfo();

            Console.Write("Are you a new user?[Y/N]");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y) // NEW account (PasswordToHash)
            {
                Stampa.PrintInfo();
                Console.WriteLine("New registration!");

                bool flag = false;
                do
                {
                    if (!flag)
                    {
                        Console.Write("Username: ");
                    }
                    else
                    {
                        Stampa.PrintInfo();
                        Console.WriteLine($"'{username}' already exist. Try again!");
                        Console.Write("Username: ");
                    }
                    username = Console.ReadLine();
                } while (flag = CheckFieldExist(sqlCnn, username, "Username")); // cicla finche' non si inserisce un username non presente nel DB (che deve essere UNICO)

                Console.Write("Password: ");
                KeyValuePair<string, string> hashAndSalt = Encryption.PaswordToHash(Console.ReadLine()); // stringa esadecimale della password criptata in hash e salt

                SqlCommand cmdInsertHash = new SqlCommand($"INSERT INTO Utenti (Username, PswHash, Salt) VALUES ('{username}', '{hashAndSalt.Key}', '{hashAndSalt.Value}')", sqlCnn);

                //Console.WriteLine("hash: "+hashAndSalt.Key.Length);
                //Console.WriteLine("salt: "+hashAndSalt.Value.Length);

                cmdInsertHash.ExecuteNonQuery();
            }
            else // LOGIN (VerifyPsw)
            {
                string pwd = string.Empty;

                do
                {
                    retry++; // 1o tentativo

                    Stampa.PrintInfo();

                    if (retry > 1)
                    {
                        Console.WriteLine($"Wrong username/password[{retry}/4]");
                    }

                    Console.WriteLine("Login to the DataBase...");
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    Console.Write("Password: ");
                    pwd = Console.ReadLine();
                    
                } while (!Encryption.VerifyPwd(username, pwd) && retry < 4);
            }




            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();

            if (retry < 4)
                return new KeyValuePair<string, bool>(username, true);
            else
                return new KeyValuePair<string, bool>(username, false);
        }
        #endregion

        #region Search Menu
        public static void Search()
        {
            string choice = string.Empty;
            string data = string.Empty;
            string column = string.Empty;


            do
            {
                Stampa.PrintInfo();
                Stampa.PrintMenuSearch();

                switch (choice = Console.ReadLine())
                {
                    case "1":
                        // Searial Number of the Employee
                        column = "Matricola";
                        Console.Write($"{column}: ");
                        data = Console.ReadLine();
                        Impiegato.SearchForColumn($"{column}", data);
                        break;

                    case "2":
                        // Date of Recruitment
                        column = "DataAssunzione";
                        Console.Write($"{column}: ");
                        data = Console.ReadLine();
                        Impiegato.SearchForColumn($"{column}", data);
                        break;

                    case "3":
                        // Full Name
                        string name = "Nome";
                        string surname = "Cognome";

                        Console.Write($"{name}: ");
                        string dataName = Console.ReadLine();

                        Console.Write($"{surname}: ");
                        string dataSurname = Console.ReadLine();

                        Impiegato.SearchForColumn(name, dataName, surname, dataSurname);
                        break;

                    case "q":
                    case "Q":
                    case "4":
                        // Exit
                        break;

                    default:
                        break;
                }
                Console.WriteLine("Press ENTER to go back to the Search menu.");
            } while (choice != "q" && choice != "Q" && choice != "4" && Console.ReadKey().Key == ConsoleKey.Enter);
        }
        #endregion

        #region save Exception to DB Log tab
        public static void ExceptionToDBLog(Exception e, string username)
        {
            SqlConnection sqlCnn = DB.Connection();
            
            string[] stackTrace = e.StackTrace.Split("\r\n");       // split the string that give us more information about the location of the error

            string atMethod = e.TargetSite + ";";
            foreach (string stack in stackTrace)
                 atMethod += stack + ";";

            SqlCommand cmdSaveExToLog = new SqlCommand("INSERT INTO Log (Username, Ora, ExceptionName, ExceptionMessage, ExceptionAtMethod) " +
                                                      $"VALUES ('{username}', '{DateTime.Now}', '{e.GetType().Name}', '{e.Message}', '{atMethod}')", sqlCnn);

            //cmdSaveExToLog.Parameters.Add("@Data", SqlDbType.DateTime).Value = DateTime.Now;
            //cmdSaveExToLog.Parameters.Add(new SqlParameter("@Data", DateTime.Now));

            cmdSaveExToLog.ExecuteNonQuery();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion

        # region Check on DB if field (into tab Utenti) exist - RETURN boolean
        public static bool CheckFieldExist(SqlConnection sqlCnn, string data, string field)
        {
            SqlCommand sqlCheckField = new SqlCommand($"SELECT COUNT(*) FROM Utenti WHERE {field}='{data}'", sqlCnn); // ritorna il NUMERO (count) di tuple di utenti con quello Username

            return (Convert.ToInt16(sqlCheckField.ExecuteScalar()) == 1); // se esiste gia' il filed == data, return true
        }
        #endregion
    }
}