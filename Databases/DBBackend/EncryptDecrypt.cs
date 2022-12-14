using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Brainstormer.Databases.DBBackend
{
    //Encrypts and decrypts the password using SHA1 when accessing the database
    internal class EncryptDecrypt
    {
        //Using the connection string (which varies) as key doesn't work as it can change, unless it's externally hosted.
        //Credit: https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
        
        //For security reasons, this is a bad idea, so all we can do is limit the access.
        private static readonly string encryptionKey = "P8eAeSdExXHx8Tj";

        //Encrypts a string to cipher text.
        public static string Encrypt(string clearText)
        {
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
            return clearText;
        }

        //Decrypts a string to plain text.
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                CryptoStream cryptoStream = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
                using (CryptoStream cs = cryptoStream)
                {
                    try
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipherText);
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The Encryption key doesn't match!");
                    }
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
            return cipherText;
        }
    }
}

