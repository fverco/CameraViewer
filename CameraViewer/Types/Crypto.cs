using System;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace CameraViewer.Types
{
    internal class Crypto
    {
        /// <summary>
        /// Encrypts a given string.
        /// </summary>
        /// <param name="str">The data to be encrypted.</param>
        /// <returns>An encrypted string of the data.</returns>
        public static string Protect(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
            return protectedData;
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="str">The data to be decrypted.</param>
        /// <returns>An decrypted string of the data.</returns>
        public static string Unprotect(string str)
        {
            byte[] protectedData = Convert.FromBase64String(str);
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            string data = Encoding.ASCII.GetString(ProtectedData.Unprotect(protectedData, entropy, DataProtectionScope.CurrentUser));
            return data;
        }
    }
}
