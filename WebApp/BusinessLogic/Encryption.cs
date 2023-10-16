using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.BusinessLogic
{
    public class Encryption
    {
        public static KeyValuePair<string, string> PaswordToHash(string pwd)
        {
            byte[] salt = new byte[32];
            using (RNGCryptoServiceProvider randCSP = new())
            {
                randCSP.GetNonZeroBytes(salt); // salva in salt una stringa di bytes random
            }
            // Console.WriteLine($"SALE (Salt): {Convert.ToBase64String(byteSalt)}");


            // crea chiave hash da 128bit con i seguenti dati: password + random salt + algorithm SHA256
            string hash = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(
                    password: Encoding.UTF8.GetBytes(pwd),
                    salt: salt,
                    iterations: 100000,
                    hashAlgorithm: HashAlgorithmName.SHA256,
                    outputLength: 128));

            return new KeyValuePair<string, string>(hash, Convert.ToBase64String(salt)); // ritrona la stringa in esadecimale di <hash e salt>
        }

        public static bool VerifyPwd(string username, string pwd)
        {
            SqlConnection sqlCnn = DbManager.Connection();

            SqlCommand cmdGetHashAndSalt = new SqlCommand($"SELECT * FROM Utenti WHERE Username = '{username}'", sqlCnn);
            SqlDataReader reader = cmdGetHashAndSalt.ExecuteReader();
            // reader[1].ToString() e' la stringa hash ottenuta in precedenza in fase di creazione

            bool result = false;

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    // hash ricavato con sale e password utente
                    var hash = Rfc2898DeriveBytes.Pbkdf2(
                        password: Encoding.UTF8.GetBytes(pwd),
                        salt: Convert.FromBase64String(reader[2].ToString()), // salt ottenuto in fase di creazione pwd e account
                        iterations: 100000,
                        hashAlgorithm: HashAlgorithmName.SHA256,
                        outputLength: 128);
                    result = hash.SequenceEqual(Convert.FromBase64String(reader[1].ToString()));
                }
            }

            reader.Close();

            if (sqlCnn.State == ConnectionState.Open)
                sqlCnn.Close();

            return result;
            //var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            //return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
