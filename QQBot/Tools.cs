using System;
using System.Globalization;
using System.IO;
namespace QQBot
{
	public class Tools
	{
		public static byte[] HexStringToBytes(string hex)
		{
			byte[] result;
			if (hex.Length == 0)
			{
				byte[] array = new byte[1];
				result = array;
			}
			else
			{
				if (hex.Length % 2 == 1)
				{
					hex = "0" + hex;
				}
				byte[] array2 = new byte[hex.Length / 2];
				for (int i = 0; i < hex.Length / 2; i++)
				{
					array2[i] = byte.Parse(hex.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
				}
				result = array2;
			}
			return result;
		}
		public static void SaveImage(byte[] img)
		{
			FileStream fileStream = new FileStream(DateTime.Now.ToString("MMddhhmmss") + ".png", FileMode.Create);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.Write(img);
			streamWriter.Close();
			fileStream.Close();
		}
	}
}
