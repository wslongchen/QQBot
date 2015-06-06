using System;
using System.Security.Cryptography;
using System.Text;
namespace QQBot
{
	internal class MD5Helper
	{
		public static string ToMD5(string str)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "").ToLower();
		}
	}
}
