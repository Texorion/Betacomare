using Lib;
using AppConsoleDB.Classes;
using Microsoft.Data.SqlClient;
using NLog;

namespace AppConsoleDB
{
    internal class Program
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            //logger.Info("Start app con richiesta credenziali");
            //logger.Error("Error");
            //logger.Debug("Debug");
            //logger.Warn("Avviso");
            //logger.Fatal("Fatal Error");

            /*
             * ..\AppConsoleDB\bin\debug\net7.0
             * Parent.Parent == ../..
             * ..\AppConsoleDB
             */
            string parentDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string logDir = parentDir + @"\Log";
            string fileLog = logDir + @"\Log.txt";

            KeyValuePair<string, bool> connection = DB.ConnectionAndLogin();

            try
            {
                /* -- Creo Cartella Log se non esiste -- */
                if (!Directory.Exists(logDir))
                {
                    //throw new DirectoryNotFoundException("Directory non trovata AIUTO!!");
                    Directory.CreateDirectory(logDir);
                }
                else if (connection.Value) // se l'utente inserisce le corrette credenziali per collegarsi: TRUE
                {
                    string choice = string.Empty;
                    do
                    {
                        if(choice != "1")
                        {
                            Stampa.PrintInfo();
                            Stampa.PrintMenu();
                        }
                        else
                        {
                            Stampa.PrintMenu();
                        }

                        switch (choice = Console.ReadLine())
                        {
                            case "1":
                                // Show all Employees
                                /* -- Connect to DB and Read tables -- */
                                Azienda azienda = DB.ConnectionAndRead();
                                Stampa.PrintList(Azienda.ToString(azienda.listImpiegati));
                                //Stampa.PrintObj(Impiegato.ToString(azienda.listImpiegati[0])); // stampa impiegato
                                break;

                            case "2":
                                // Insert new Employee
                                Impiegato.InsertImpiegato(); // Inser Into Impiegato
                                break;

                            case "3":
                                // Search an employee with Serial Number
                                DB.Search();
                                break;

                            case "4":
                                // Update an employee (identify with his Serial Number)
                                Impiegato.UpdateImpiegato();
                                break;

                            case "5":
                                // Delete an employee (identify with his Serial Number
                                Impiegato.DeleteImpiegato();
                                break;

                            case "q":
                            case "Q":
                            case "6":
                                // Exit
                                break;

                            default:
                                break;
                        }
                    } while (choice != "q" && choice != "Q" && choice != "6");
                }

            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);              // stampo il messasggio contenuto nell'eccezione (lanciata con throw) ex
                DB.ExceptionToDBLog(ex, connection.Key);    // print exception to the file log, formatted
                Log.PrintExToFormatLog(ex, fileLog);        // print exception to the file log, formatted
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                DB.ExceptionToDBLog(ex, connection.Key);
                Log.PrintExToFormatLog(ex, fileLog);        // print exception to the file log, formatted
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
                DB.ExceptionToDBLog(ex, connection.Key);
                Log.PrintExToFormatLog(ex, fileLog);        // print exception to the file log, formatted
            }
            catch (Exception ex) // eccezione generica, non raccolta dalle precedenti
            {
                Console.WriteLine(ex.Message);
                DB.ExceptionToDBLog(ex, connection.Key);
                Log.PrintExToFormatLog(ex, fileLog);        // print exception to the file log, formatted
            }
            finally // prima di chiudere l'esecuzione dopo il catch, esegue il codice in finally
            {
                Stampa.PrintInfo();
                Console.WriteLine("Thank you for chosing us. Bye");
            }
        }
    }
}