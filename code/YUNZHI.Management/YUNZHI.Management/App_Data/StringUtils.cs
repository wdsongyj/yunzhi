using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.Configuration;

namespace YUNZHI.Management
{
	/// <summary>
	/// 字符串工具类
	/// </summary>
	public class StringUtils
	{
		/// <summary>
		/// 加密类型
		/// </summary>
		public enum CryptoType
		{
			MD5,
			SHA1,
			SHA256,
			SHA512,
			Base64
		}
		/// <summary>
		/// 工具类不允许实例化
		/// </summary>
		private StringUtils()
		{
		}

		/// <summary>
		/// 判断字符串是否为null
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <returns>True为null，False不为null</returns>
		public static bool IsNull(string s)
		{
			if (s == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 判断字符串是否不为null
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <returns>True不为null，False为null</returns>
		public static bool IsNotNull(string s)
		{
			return !IsNull(s);
		}

		/// <summary>
		/// 判断字符串是否为null或空
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <returns>True为空，False为不空</returns>
		public static bool IsNullOrEmpty(string s)
		{
			return String.IsNullOrEmpty(s);
		}

		/// <summary>
		/// 判断字符串是否不为null或空
		/// </summary>
		/// <param name="s">原字符串</param>
		/// <returns>True为不空，False为空</returns>
		public static bool IsNotNullOrEmpty(string s)
		{
			return !IsNullOrEmpty(s);
		}

		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="plaintext">明文</param>
		/// <param name="cryptoType">加密类型</param>
		/// <returns>密文</returns>
		public static string Encrypt(string plaintext, CryptoType cryptoType)
		{
			if (plaintext == null || plaintext == "")
				return "";
			if (cryptoType == CryptoType.Base64)
				return EncryptByBase64(plaintext);

			Byte[] plaintextBytes = Encoding.ASCII.GetBytes(plaintext);
			Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName(cryptoType.ToString())).ComputeHash(plaintextBytes);

			StringBuilder ciphertext = new StringBuilder();
			for (int i = 0; i < hashedBytes.Length; i++)
			{
				ciphertext.Append(hashedBytes[i].ToString("X2"));
			}
			return ciphertext.ToString();
		}
		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="ciphertext"></param>
		/// <param name="cryptoType"></param>
		/// <returns></returns>
		public static string Decode(string ciphertext, CryptoType cryptoType)
		{
			if (ciphertext == null || ciphertext == "")
				return "";

			if (cryptoType == CryptoType.Base64)
				return DecodeByBase64(ciphertext);

			throw new Exception(cryptoType.ToString()+" 类型无法解密");
		}

		/// <summary>
		/// 将明文转换成MD5密文
		/// </summary>
		/// <param name="plaintext">明文</param>
		/// <returns>MD5密文</returns>
		public static string EncryptByMD5(string plaintext)
		{
			return Encrypt(plaintext, CryptoType.MD5);
		}

		/// <summary>
		/// 将明文转换成SHA1密文
		/// </summary>
		/// <param name="plaintext">明文</param>
		/// <returns>SHA1密文</returns>
		public static string EncryptBySHA1(string plaintext)
		{
			return Encrypt(plaintext, CryptoType.SHA1);
		}

		/// <summary>
		/// 将明文转换成Base64密文
		/// </summary>
		/// <param name="plaintext">明文</param>
		/// <returns>Base64密文</returns>
		public static string EncryptByBase64(string plaintext)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// 将Base64密文转换成明文
		/// </summary>
		/// <param name="ciphertext">Base64密文</param>
		/// <returns>明文</returns>
		public static string DecodeByBase64(string ciphertext)
		{
			byte[] bytes = Convert.FromBase64String(ciphertext);
			return Encoding.UTF8.GetString(bytes);
		}

		/// <summary>
		/// 格式化字符串
		/// </summary>
		/// <param name="s">源字符串</param>
		/// <param name="formatType">格式化类型</param>
		/// <returns>格式化字符串值</returns>
		public static string FormatString(string s, string formatType)
		{
			return String.Format(formatType, s);
		}

		public static string FormatString(int s, string formatType)
		{
			return string.Format(formatType, s);
		}

        public static string AppendParamForUrl(string url, string paramName, string paramValue)
        {
            string param = paramName + "=" + paramValue;
            string connector;
            if (url.Contains("?"))
                connector = "&amp;";
            else
                connector = "?";

            if (url.Contains(paramName + "="))
            {
                int startIndex = url.IndexOf(paramName + "=");
                string paramSection = url.Substring(startIndex);
                int paramSectionEndIndex = paramSection.IndexOf("&");
                if (paramSectionEndIndex != -1)
                    paramSection = paramSection.Substring(0, paramSectionEndIndex);
                url = url.Replace(paramSection, param);
            }
            else
            {
                url += connector + param;
            }
            return url;
        }

        public static string GuidString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 将xml转换成带有转义字符的string.
        /// <Root>a</Root>  ==> &lt;Root&gt;a&lt;/Root&gt;
        /// </summary>
        public static string XmlToStringWithESC(string source, bool replaceQuot)
        {
            source = source.Replace("&", "&amp;");
            source = source.Replace("<", "&lt;");
            source = source.Replace(">", "&gt;");
            if (replaceQuot)
                source = source.Replace("\"", "&quot;");
            return source;
        }

        public static string ConvertPlatfromUserName(string name)
        {
            if (name.ToUpper().Contains("K2SQL:"))
                name = name.Replace("K2SQL:", "SQL:");
            if (!name.ToUpper().Contains("SQL:"))
                name = "SQL:" + name;
            return name;
        }
	}
}
