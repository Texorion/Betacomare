namespace Lib
{
    public class Stampa
    {
        #region Formatted Print to Console (string obj)
        /** -- Stampa i campi di un oggetto di classe trasformati in string e formattati -- */
        public static void PrintObj(string obj)
        {
            string[] objField = obj.Split(';');

            foreach (string s in objField)
            {
                Console.WriteLine("{0,5} {1, -20}", " ", s);
            }
        }
        #endregion

        #region Formatted Print to Console (string List<obj>)
        /* -- Stampa tutti gli oggetti diuna classe, come stringhe formattate -- */
        public static void PrintList(string[] list)
        {
            int i = 1;
            foreach (string tupla in list)
            {
                Console.WriteLine($"({i})");
                string[] t = tupla.Split(';');
                foreach (string s in t)
                {
                    Console.WriteLine("{0,5} {1, -20}", " ", s);
                }
                i++;
            }
        }
        #endregion

        #region Formatted Print From Reader to Console (string List<obj>)
        /* -- Stampa tutti gli oggetti diuna classe, come stringhe formattate -- */
        public static void PrintListFromReader(string[] list)
        {
            int numRows = list[0].Split(";").Length - 1; // numero di righe

            string[] rows = new string[numRows];

            for (int i = 0; i < list.Length; i++)
            {
                // monto assieme i campi per riga prima salvati per colonna
                for (int j = 0; j < numRows; j++)
                {
                    rows[j] += list[i].Split(';')[j] + ";";
                }
            }

            PrintList(rows);
        }
        #endregion

        #region Print Info
        public static void PrintInfo()
        {
            Console.Clear();    // pulisce la console

            // Print the header
            Console.WriteLine(" ------------------------------\n" +
                              "| AppConsole v0.1 - 2023/03/10 |\n" +
                              " ------------------------------\n");
        }
        #endregion

        public static void PrintMenu()
        {
            Console.WriteLine("----------------------------------------------------------------\n" +
                              "[1]\tShow all Employees\n" +
                              "[2]\tInsert new Employee\n" +
                              "[3]\tSearch\n" +
                              "[4]\tUpdate an employee (identify with his Serial Number)\n" +
                              "[5]\tDelete an employee (identify with his Serial Number\n" +
                              "[6]\tExit\n" +
                              "----------------------------------------------------------------");
        }

        public static void PrintMenuSearch()
        {
            Console.WriteLine("----------------------------------------------------------------\n" +
                              "SEARCH for:\n" +
                              "[1]\tSearial Number of the Employee\n" +
                              "[2]\tDate of Recruitment\n" +
                              "[3]\tFull Name\n" +
                              "[4]\tReturn Back\n" +
                              "----------------------------------------------------------------");
        }

    }
}