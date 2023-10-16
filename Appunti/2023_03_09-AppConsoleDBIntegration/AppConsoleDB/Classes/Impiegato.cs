using Azure.Core;
using Lib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppConsoleDB.Classes
{
    internal class Impiegato
    {
        #region Global var
        public string Matricola { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataAssunzione { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        #endregion

        #region Constructor
        public Impiegato()
        {
            Matricola = string.Empty;
            Nome = string.Empty;
            Cognome = string.Empty;
            DataAssunzione = string.Empty;
            Indirizzo = string.Empty;
            Citta = string.Empty;
            CAP = string.Empty;
            Provincia = string.Empty;
            Telefono = string.Empty;
        }
        #endregion


        #region Raed Impiegati and save into obj Azienda
        public static void ReadImpiegati(List<Impiegato> list)
        {
            SqlConnection sqlCnn = DB.Connection();

            SqlCommand cmdImpiegati = new SqlCommand("SELECT * FROM Impiegati", sqlCnn);

            SqlDataReader reader = cmdImpiegati.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Impiegato imp = new();
                    imp.Matricola = reader.GetValue(0).ToString();
                    imp.Nome = reader.GetValue(1).ToString();
                    imp.Cognome = reader.GetValue(2).ToString();
                    imp.DataAssunzione = reader.GetValue(3).ToString();
                    imp.Indirizzo = reader.GetValue(4).ToString();
                    imp.Citta = reader.GetValue(5).ToString();
                    imp.CAP = reader.GetValue(6).ToString();
                    imp.Provincia = reader.GetValue(7).ToString();
                    imp.Telefono = reader.GetValue(8).ToString();

                    list.Add(imp);
                }
            }
            reader.Close();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion


        #region Insert Impiegato into DB Azienda
        public static void InsertImpiegato()
        {
            SqlConnection sqlCnn = DB.Connection();

            string matricola = string.Empty;
            //chiedo parametri
            bool flag = false;
            do
            {
                if (!flag)
                {
                    Console.Write("Matricola: ");
                }
                else
                {
                    Console.Clear();
                    Stampa.PrintMenu();
                    Console.WriteLine($"'{matricola}' already exist. Try again!");
                    Console.Write("Matricola: ");
                }
                matricola = Console.ReadLine();
            } while (flag = CheckFieldExist(sqlCnn, matricola, "Matricola")); // cicla finche' non si inserisce una matricola diversa da quelle gia' presenti nel DB (che devono essere UNICHE)

            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Cognome: ");
            string cognome = Console.ReadLine();
            Console.Write("Data Assunzione: ");
            string dataAssunzione = Console.ReadLine();
            Console.Write("Indirizzo: ");
            string indirizzo = Console.ReadLine();
            Console.Write("CAP: ");
            string cap = Console.ReadLine();
            Console.Write("Citta': ");
            string citta = Console.ReadLine();
            Console.Write("Provincia: ");
            string provincia = Console.ReadLine();
            Console.Write("Telefono: ");
            string telefono = Console.ReadLine();

            //preparo la query
            SqlCommand cmdImpiegati = new SqlCommand("EXEC sp_InsertImpiegato @matricola,@nome,@cognome,@dataAssunzione,@indirizzo,@CAP,@citta,@provincia,@telefono", sqlCnn);

            cmdImpiegati.Parameters.Add("@matricola", SqlDbType.NChar).Value = matricola;
            cmdImpiegati.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nome;
            cmdImpiegati.Parameters.Add("@cognome", SqlDbType.NVarChar).Value = cognome;
            cmdImpiegati.Parameters.Add(new SqlParameter("@dataAssunzione", dataAssunzione));
            cmdImpiegati.Parameters.Add("@indirizzo", SqlDbType.NVarChar).Value = indirizzo;
            cmdImpiegati.Parameters.Add("@CAP", SqlDbType.NVarChar).Value = cap;
            cmdImpiegati.Parameters.Add("@citta", SqlDbType.NVarChar).Value = citta;
            cmdImpiegati.Parameters.Add("@provincia", SqlDbType.NChar).Value = provincia;
            cmdImpiegati.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = telefono;

            cmdImpiegati.ExecuteNonQuery();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion


        #region Update Impiegato (DB Azienda)
        public static void UpdateImpiegato()
        {
            SqlConnection sqlCnn = DB.Connection();

            string matricola = string.Empty;
            bool flag = true;
            int retry = 1;
            do
            {
                retry++;

                if (flag)
                {
                    Console.Write("Inserisci la MATRICOLA dell'impiegato da modificare: ");
                }
                else
                {
                    Console.Clear();
                    Stampa.PrintMenu();
                    Console.WriteLine($"'{matricola}' not exist. Try again!");
                    Console.Write("Inserisci la MATRICOLA dell'impiegato da modificare: ");
                }
                matricola = Console.ReadLine();
            } while (!(flag = CheckFieldExist(sqlCnn, matricola, "Matricola")) && retry < 4); // cicla finche' non si trova la matricola di un utente esistente da modificare
            
            // stampa numerata per campi di impiegato

            Console.Write($"Quale campo di '{matricola}' desidera aggiornare?");
            do
            {
                // stampa numerata per campi di impiegato

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        UpdateField(sqlCnn, "Nome", nome, "Matricola", matricola);
                        break;

                    case "2":
                        Console.Write("Cognome: ");
                        UpdateField(sqlCnn, "Cognome", Console.ReadLine(), "Matricola", matricola);
                        break;

                    case "3":
                        Console.Write("Data Assunzione: ");
                        UpdateField(sqlCnn, "DataAssunzione", Console.ReadLine(), "Matricola", matricola);
                        break;

                    case "4":
                        Console.Write("Indirizzo: ");
                        UpdateField(sqlCnn, "Indirizzo", Console.ReadLine(), "Matricola", matricola);
                        break;

                    case "5":
                        Console.Write("CAP: ");
                        UpdateField(sqlCnn, "CAP", Console.ReadLine(), "Matricola", matricola);
                        break;

                    case "6":
                        Console.Write("Citta': ");
                        UpdateField(sqlCnn, "Citta", Console.ReadLine(), "Matricola", matricola);

                        break;

                    case "7":
                        Console.Write("Provincia: ");
                        UpdateField(sqlCnn, "Provincia", Console.ReadLine(), "Matricola", matricola);
                        break;

                    case "8":
                        Console.Write("Telefono: ");
                        UpdateField(sqlCnn, "Telefono", Console.ReadLine(), "Matricola", matricola);
                        break;

                    default:
                        break;
                }

                Console.Write($"Desidera aggiornare un altro campo di '{matricola}'?[Y/N]");
                string loop = Console.ReadLine();
                flag = (loop != "n" && loop != "N");
            }while (flag);

            Console.Write("Impiegato aggiornato con successo");

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion


        #region Delete Impiegato (DB Azienda)
        public static void DeleteImpiegato()
        {
            SqlConnection sqlCnn = DB.Connection();

            string matricola = string.Empty;
            bool flag = false;
            int retry = 1;
            do
            {
                retry++;

                if (!flag)
                {
                    Console.Write("Matricola: ");
                }
                else
                {
                    Console.Clear();
                    Stampa.PrintMenu();
                    Console.WriteLine($"'{matricola}' not exist. Try again!");
                    Console.Write("Matricola: ");
                }
                matricola = Console.ReadLine();
            } while (!(flag = CheckFieldExist(sqlCnn, matricola, "Matricola")) && retry < 4); // cicla finche' non si trova la matricola da rimuovere

            // Elimina matricola dalla tabella padre 'Impiegato'
            SqlCommand cmdDelImpiegato = new SqlCommand($"DELETE FROM Impiegati WHERE Matricola = '{matricola}'", sqlCnn);

            cmdDelImpiegato.ExecuteNonQuery();


            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion


        public static void UpdateField(SqlConnection sqlCnn, string field, string data, string fieldWhere, string dataWhere)
        {
            SqlCommand cmdUpdate = new SqlCommand($"UPDATE Impiegati SET {field}='{data}' WHERE {fieldWhere} = '{dataWhere}'", sqlCnn);
            cmdUpdate.ExecuteNonQuery();
        }

        # region Check on DB if field (into tab Impiegati) exist - RETURN boolean
        public static bool CheckFieldExist(SqlConnection sqlCnn, string data, string field)
        {
            SqlCommand checkMatricola = new SqlCommand($"SELECT * FROM Impiegati WHERE {field}='{data}'", sqlCnn);
            SqlDataReader reader = checkMatricola.ExecuteReader();
            bool flag = reader.HasRows;
            reader.Close();

            return flag; // true if matricola exist
        }
        #endregion


        # region Cerca in Impiegata e stampa le tuple per il campo richiesto, aggiungendo le informazioni riguardo la loro AttivitaLavorativa
        public static void SearchForColumn(string column, string data)
        {
            SqlConnection sqlCnn = DB.Connection();

            SqlCommand checkMatricola = new SqlCommand("SELECT * " +
                                                       "FROM Impiegati AS I " +
                                                            "JOIN AttivitaLavorativa AS A ON I.Matricola=A.Matricola " +
                                                      $"WHERE I.{column}='{data}'", sqlCnn);
            SqlDataReader reader = checkMatricola.ExecuteReader();

            if (reader.HasRows)
            {
                string[] rows = new string[reader.FieldCount]; // 15 campi: 9 (Impiegato) + 6 (AttivitaLavorativa)
                while (reader.Read()) // per ogni campo
                {
                    for (int i = 0; i < reader.FieldCount; i++) // salvo insieme ogni colonna
                    {
                        rows[i] += $"{reader[i].ToString().TrimEnd()}" + ";";
                    }
                }
                // stampa lista risultati
                Console.WriteLine($"There are [{rows[0].Split(";").Length - 1}] results for {column}: {data}");
                Stampa.PrintListFromReader(rows);
            }
            else
            {
                Console.WriteLine("No match found.");
            }

            reader.Close();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion


        # region Cerca in Impiegata e stampa le tuple per Nome Cognome, aggiungendo le informazioni riguardo la loro AttivitaLavorativa
        public static void SearchForColumn(string columnName, string name, string columnSurname, string surname)
        {
            SqlConnection sqlCnn = DB.Connection();

            SqlCommand checkMatricola = new SqlCommand("SELECT * " +
                                                       "FROM Impiegati AS I " +
                                                            "JOIN AttivitaLavorativa AS A ON I.Matricola=A.Matricola " +
                                                      $"WHERE I.{columnName}='{name}' AND I.{columnSurname}='{surname}'", sqlCnn);
            SqlDataReader reader = checkMatricola.ExecuteReader();

            if (reader.HasRows)
            {
                string[] rows = new string[reader.FieldCount];
                while (reader.Read()) // per ogni campo
                {
                    for (int i = 0; i < reader.FieldCount; i++) // salvo insieme ogni colonna
                    {
                        rows[i] += $"{reader[i].ToString().TrimEnd()}" + ";";
                    }
                }
                // stampa lista risultati
                Console.WriteLine($"There are [{rows[0].Split(";").Length - 1}] results for {columnName} {columnSurname}: {name} {surname}");
                Stampa.PrintListFromReader(rows);
            }
            else
            {
                Console.WriteLine("No match found.");
            }

            reader.Close();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();
        }
        #endregion

        #region Formatted Impiegato
        public static void ToStringFormatted(SqlConnection sqlCnn, string field, string data)
        {
            SqlCommand cmdGetImpiegato = new SqlCommand($"SELECT * FROM Impiegati WHERE {field}='{data}'", sqlCnn);
            SqlDataReader reader = cmdGetImpiegato.ExecuteReader();

            reader.Close();

        }
        #endregion

        #region ToString() - string filds of Friend obj
        public static string ToString(Impiegato imp)
        {
            return $"{imp.Matricola};{imp.Nome};{imp.Cognome};{imp.DataAssunzione};{imp.Indirizzo};{imp.Citta};{imp.CAP};{imp.Provincia};{imp.Telefono}";
        }
        #endregion
    }
}
