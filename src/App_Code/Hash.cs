using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

namespace ITCommunity {
    public static class Hash {
        private static readonly SHA1 sha1 = SHA1CryptoServiceProvider.Create();
        public static string CalculateSHA1(byte[] message) {
            byte[] hash = sha1.ComputeHash(message);
            StringBuilder digest = new StringBuilder();
            for( int i=0; i < hash.Length; i++) {
                digest.Append(hash[i].ToString("x2"));
            }
            return digest.ToString();
        }
        /// <summary>
        ///  Рассчитывает SHA-1 хеш для строки
        /// </summary>
        /// <param name="message">строка в UTF-8</param>
        /// <returns>SHA-1 хеш в виде строки</returns>
        public static string CalculateSHA1(String msg) {
            return CalculateSHA1(Encoding.UTF8.GetBytes(msg));
        }
        private static readonly MD5 md5 = MD5CryptoServiceProvider.Create();
        public static string CalculateMD5(byte[] message) {
            byte[] hash = md5.ComputeHash(message);
            StringBuilder digest = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                digest.Append(hash[i].ToString("x2"));
            }
            return digest.ToString();
        }
        /// <summary>
        ///  Рассчитывает MD5 хеш для строки
        /// </summary>
        /// <param name="message">строка в UTF-8</param>
        /// <returns>MD5 хеш в виде строки</returns>
        public static string CalculateMD5(String message) {
            return CalculateMD5(Encoding.UTF8.GetBytes(message));
        }
    }
}