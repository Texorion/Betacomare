using Lib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleDB.Classes
{
    internal class Azienda
    {
        #region Global var
        public List<Impiegato> listImpiegati;
        public List<AttivitaLavorativa> listAttLav;
        public List<OreStraordinario> listOreStra;
        #endregion

        public Azienda()
        {
            listAttLav = new List<AttivitaLavorativa>();
            listOreStra = new List<OreStraordinario>();
            listImpiegati = new List<Impiegato>();
        }

        #region List<Impiegati> ToString() - string filds of Impiegati
        public static string[] ToString(List<Impiegato> impiegati)
        {
            string[] stringImpiegati = new string[impiegati.Count];
            int i = 0;
            foreach (Impiegato imp in impiegati)
            {
                stringImpiegati[i] = Impiegato.ToString(imp);
                i++;
            }
            return stringImpiegati;
        }
        #endregion

        #region List<AttivitaLavorativa> ToString() - string filds of AttivitaLavorativa
        public static string[] ToString(List<AttivitaLavorativa> attLav)
        {
            string[] stringAttLav = new string[attLav.Count];
            int i = 0;
            foreach (AttivitaLavorativa a in attLav)
            {
                stringAttLav[i] = AttivitaLavorativa.ToString(a);
                i++;
            }
            return stringAttLav;
        }
        #endregion

        #region List<OreStraordinario> ToString() - string filds of OreStra
        public static string[] ToString(List<OreStraordinario> oreStr)
        {
            string[] stringOre = new string[oreStr.Count];
            int i = 0;
            foreach (OreStraordinario ore in oreStr)
            {
                stringOre[i] = OreStraordinario.ToString(ore);
                i++;
            }
            return stringOre;
        }
        #endregion


    }
}
