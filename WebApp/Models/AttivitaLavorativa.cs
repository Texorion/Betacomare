using Microsoft.Data.SqlClient;
using WebApp.BusinessLogic;
using Lib;

namespace WebApp.Models
{
    public class AttivitaLavorativa
    {
        #region Global var
        public int ID { get; set; }
        public string? Matricola { get; set; }
        public string? Reparto { get; set; }
        public string? DataGiornoLavorativo { get; set; }
        public string? GiornoLavorativo { get; set; }
        public double OreLavorate { get; set; }
        #endregion

        #region Constructor
        public AttivitaLavorativa()
        {
            ID = 0;
            Matricola = string.Empty;
            Reparto = string.Empty;
            DataGiornoLavorativo = string.Empty;
            GiornoLavorativo = string.Empty;
            OreLavorate = 0;
        }
        #endregion


        public static void ReadAttivitaLavorative(List<AttivitaLavorativa> list)
        {
            SqlConnection sqlCnn = DbManager.Connection(); // Metodo per la connessione al DataBase

            SqlCommand cmdImpiegati = new SqlCommand("SELECT * FROM AttivitaLavorativa", sqlCnn);

            SqlDataReader reader = cmdImpiegati.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AttivitaLavorativa attLav = new();
                    attLav.ID = Check.CheckValueStringToInt(reader["ID"].ToString(), "ID");
                    attLav.Matricola = reader["Matricola"].ToString();
                    attLav.Reparto = reader["Reparto"].ToString();
                    attLav.DataGiornoLavorativo = reader["DataGiornoLavorativo"].ToString();
                    attLav.GiornoLavorativo = reader["GiornoLavorativo"].ToString();
                    attLav.OreLavorate = Check.CheckValueStringToDouble(reader["OreLavorate"].ToString(), "OreLavorate");

                    list.Add(attLav);
                }
            }
            reader.Close();
        }

        #region ToString() - string filds of AttivitaLavorativa
        public static string ToString(AttivitaLavorativa attLav)
        {
            return $"{attLav.ID};{attLav.Matricola};{attLav.Reparto};{attLav.DataGiornoLavorativo};{attLav.GiornoLavorativo};{attLav.OreLavorate}";
        }
        #endregion
    }
}
