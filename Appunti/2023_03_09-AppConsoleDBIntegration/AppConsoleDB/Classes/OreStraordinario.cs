using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace AppConsoleDB.Classes
{
    internal class OreStraordinario
    {
        public string Matricola { get; set; }
        public string Data { get; set; }
        public double Ore { get; set; }


        public OreStraordinario()
        {
            Matricola = string.Empty;
            Data = string.Empty;
            Ore = 0;
        }


        public static void ReadOreStraordinari(List<OreStraordinario> list)
        {
            SqlConnection sqlCnn = DB.Connection(); // Metodo per la connessione al DataBase

            SqlCommand cmdOreStraordinario = new SqlCommand("SELECT * FROM OreStraordinarie", sqlCnn);

            SqlDataReader reader = cmdOreStraordinario.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OreStraordinario oreStr = new();
                    oreStr.Matricola = reader.GetValue(0).ToString();
                    oreStr.Data = reader.GetValue(1).ToString();
                    oreStr.Ore = Check.CheckValueStringToDouble(reader.GetValue(2).ToString(), "Ore");

                    list.Add(oreStr);
                }
            }
            reader.Close();
        }

        public static string ToString(OreStraordinario oreStr)
        {
            return $"{oreStr.Matricola};{oreStr.Data};{oreStr.Ore}";
           
        }

    }
}
