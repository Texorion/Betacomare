using Microsoft.Data.SqlClient;
using WebApp.BusinessLogic;
using Lib;

namespace WebApp.Models
{
    public class OreStraordinario
    {
        #region Global var
        public string Matricola { get; set; }
        public string Data { get; set; }
        public double Ore { get; set; }
        #endregion

        #region Constructor
        public OreStraordinario()
        {
            Matricola = string.Empty;
            Data = string.Empty;
            Ore = 0;
        }
        #endregion


        public static void ReadOreStraordinari(List<OreStraordinario> list)
        {
            SqlConnection sqlCnn = DbManager.Connection(); // Metodo per la connessione al DataBase

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
