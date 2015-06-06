using System;
using System.Collections.Generic;
namespace QQBot
{
	internal class QQUser
	{
		private static string _QQID;
		private static string _password;
		private static string _message;
		//private static List<ChatForming> chatFormings = new List<ChatForming>();
		public static string QQID
		{
			get
			{
				return QQUser._QQID;
			}
			set
			{
				QQUser._QQID = value;
			}
		}
		public static string Password
		{
			get
			{
				return QQUser._password;
			}
			set
			{
				QQUser._password = value;
			}
		}
		public static string Message
		{
			get
			{
				return QQUser._message;
			}
			set
			{
				QQUser._message = value;
			}
		}
        //internal static List<ChatForming> ChatFormings
        //{
        //    get
        //    {
        //        return QQUser.chatFormings;
        //    }
        //    set
        //    {
        //        QQUser.chatFormings = value;
        //    }
        //}
	}
}
