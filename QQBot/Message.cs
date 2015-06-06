using System;
namespace QQBot
{
	internal class Message
	{
		private string _uid;
		private string _nk;
		private string _msg;
		private DateTime time;
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
		public string Nk
		{
			get
			{
				return this._nk;
			}
			set
			{
				this._nk = value;
			}
		}
		public string Msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}
		public DateTime Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}
		public Message(string uid, string nk, string msg)
		{
			this._uid = uid;
			this._nk = nk;
			this._msg = msg;
			this.time = DateTime.Now;
		}
	}
}
