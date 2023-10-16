using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Betacomare.Classes
{
    internal class Encryption
    {
        public static KeyValuePair<string, string> PaswordToHash(string pwd)
        {
            byte[] salt = new byte[32];
            using (RNGCryptoServiceProvider randCSP = new())
            {
                randCSP.GetNonZeroBytes(salt); // salva in salt una stringa di bytes random
            }

            // crea chiave hash da 128bit con i seguenti dati: password + random salt + algorithm SHA256
            string hash = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(
                    password: Encoding.UTF8.GetBytes(pwd),
                    salt: salt,
                    iterations: 100000,
                    hashAlgorithm: HashAlgorithmName.SHA256,
                    outputLength: 128));

            return new KeyValuePair<string, string>(hash, Convert.ToBase64String(salt)); // ritorna la stringa in esadecimale di <hash e salt>
        }

        public static bool VerifyPwd(KeyValuePair<string, string> data, string pwd)   
        {
            // hash ricavato con sale e password utente
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password: Encoding.UTF8.GetBytes(pwd),
                salt: Convert.FromBase64String(data.Key), // salt ottenuto in fase di creazione pwd e account
                iterations: 100000,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: 128);
            return hash.SequenceEqual(Convert.FromBase64String(data.Value));
        }
    }
}
