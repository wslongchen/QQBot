using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace QQBot
{
	public class Validate : Form
	{
		private TCPClass tcp;
		private PictureBox pictureBox1;
		private TextBox textBox1;
		private MainMenu mainMenu1;
		private IContainer components;
		private MenuItem menuItem1;
		private MenuItem menuItem2;
		private Image img;
		public TCPClass Tcp
		{
			get
			{
				return this.tcp;
			}
			set
			{
				this.tcp = value;
			}
		}
		public Image Img
		{
			get
			{
				return this.img;
			}
			set
			{
				this.img = value;
			}
		}
		public Validate(TCPClass tcp, Image img)
		{
			this.InitializeComponent();
			this.tcp = tcp;
			this.img = img;
			this.pictureBox1.Image = this.img;
		}
		private void menuItem2_Click(object sender, EventArgs e)
		{
		}
		private void menuItem1_Click(object sender, EventArgs e)
		{
		}
		public string GetSjs3Byte()
		{
			Random random = new Random();
			return random.Next(200, 999).ToString();
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			this.pictureBox1 = new PictureBox();
			this.textBox1 = new TextBox();
			this.mainMenu1 = new MainMenu(this.components);
			this.menuItem1 = new MenuItem();
			this.menuItem2 = new MenuItem();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Location = new Point(84, 83);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(100, 46);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.textBox1.Location = new Point(84, 163);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(100, 21);
			this.textBox1.TabIndex = 2;
			this.mainMenu1.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItem1,
				this.menuItem2
			});
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "确定";
			this.menuItem1.Click += new EventHandler(this.menuItem1_Click_1);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "取消";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click_1);
			base.ClientSize = new Size(284, 262);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.pictureBox1);
			base.Menu = this.mainMenu1;
			base.Name = "Validate";
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		private void menuItem1_Click_1(object sender, EventArgs e)
		{
			if (!(this.textBox1.Text.Trim() == ""))
			{
				string s = string.Format("VER=1.4&CON=1&CMD=VERIFYCODE&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&SC=2&VC={0}\n", this.textBox1.Text.Trim(), QQUser.QQID, this.GetSjs3Byte());
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				this.tcp.Send(bytes);
				base.Close();
			}
		}
		private void menuItem2_Click_1(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
