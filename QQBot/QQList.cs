using System;
namespace QQBot
{
	internal class QQList
	{
		private string _uid;
		private string _nickname;
		private int _online = 20;
		public string Uid
		{
			get
			{
				return this._uid;
			}
			set
			{
				this._uid = value;
			}
		}
		public string Nickname
		{
			get
			{
				return this._nickname;
			}
			set
			{
				this._nickname = value;
			}
		}
		public int Online
		{
			get
			{
				return this._online;
			}
			set
			{
				this._online = value;
			}
		}
		public QQList(string uid, string nk)
		{
			this._uid = uid;
			this._nickname = nk;
		}
	}
}
