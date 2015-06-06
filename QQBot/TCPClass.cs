using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace QQBot
{
	public class TCPClass
	{
		public delegate void MessageHelper(byte[] msg);
		public delegate void IsNotConnection();
		private TcpClient tcp;
		private NetworkStream networkStream;
		private BinaryReader br;
		private BinaryWriter bw;
		private Thread th;
		private bool IsExit = false;
		public event TCPClass.MessageHelper messageHelper;
        public bool IsExit1
		{
			get
			{
				return this.IsExit;
			}
			set
			{
				this.IsExit = value;
			}
		}
		public int Connection()
		{
			this.IsExit = true;
			int result;
			try
			{
                this.tcp = new TcpClient("211.136.236.88", 14000);
			}
			catch
			{
				result = 0;
				return result;
			}
			this.networkStream = this.tcp.GetStream();
			this.br = new BinaryReader(this.networkStream, Encoding.UTF8);
			this.bw = new BinaryWriter(this.networkStream, Encoding.UTF8);
			this.th = new Thread(new ThreadStart(this.GetData));
			this.th.IsBackground = true;
			this.th.Start();
			result = 1;
			return result;
		}
		private void GetData()
		{
			while (this.IsExit)
			{
				byte[] array = new byte[100000];
				try
				{
					int num = this.br.Read(array, 0, array.Length);
					this.messageHelper(array);
				}
				catch (Exception var_2_31)
				{
				}
			}
		}
		public void Send(string data)
		{
			try
			{
				this.bw.Write(data);
				this.bw.Flush();
			}
			catch
			{
			}
		}
		public void Send(byte[] data)
		{
			try
			{
				this.bw.Write(data);
				this.bw.Flush();
			}
			catch
			{
			}
		}
		public void Close()
		{
			this.br.Close();
			this.bw.Close();
			this.tcp.Close();
			this.tcp = null;
			this.IsExit = false;
		}
		public void IsConnection()
		{
			this.tcp.Client.Close();
			this.tcp.Close();
			this.tcp = null;
		}


       
	}
}
