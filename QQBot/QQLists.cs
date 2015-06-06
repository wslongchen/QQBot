using System;
using System.Collections.Generic;
namespace QQBot
{
	internal class QQLists
	{
		private static List<QQList> _qqlists = new List<QQList>();
		public static List<QQList> Qqlists
		{
			get
			{
				return QQLists._qqlists;
			}
			set
			{
				QQLists._qqlists = value;
			}
		}
	}
}
