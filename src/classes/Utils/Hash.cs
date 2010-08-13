using System;
using System.Security.Cryptography;
using System.Text;

namespace ITCommunity.Core {
	public static class Hash {

		private static readonly SHA1 _sha1 = SHA1CryptoServiceProvider.Create();
		private static readonly MD5 _md5 = MD5CryptoServiceProvider.Create();

		public static string CalculateSHA1(byte[] message) {
			byte[] hash = _sha1.ComputeHash(message);
			StringBuilder digest = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) {
				digest.Append(hash[i].ToString("x2"));
			}
			return digest.ToString();
		}

		/// <summary>
		/// Рассчитывает SHA-1 хеш для строки
		/// </summary>
		/// <param name="message">строка в UTF-8</param>
		/// <returns>SHA-1 хеш в виде строки</returns>
		public static string CalculateSHA1(string msg) {
			return CalculateSHA1(Encoding.UTF8.GetBytes(msg));
		}

		public static string CalculateMD5(byte[] message) {
			byte[] hash = _md5.ComputeHash(message);
			StringBuilder digest = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) {
				digest.Append(hash[i].ToString("x2"));
			}
			return digest.ToString();
		}

		/// <summary>
		/// Рассчитывает MD5 хеш для строки
		/// </summary>
		/// <param name="message">строка в UTF-8</param>
		/// <returns>MD5 хеш в виде строки</returns>
		public static string CalculateMD5(string message) {
			return CalculateMD5(Encoding.UTF8.GetBytes(message));
		}
	}
}