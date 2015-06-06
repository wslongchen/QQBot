using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KuaidiByCSharp;
using System.Threading;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Xml;
using Microsoft.Win32;

namespace QQBot
{
    public partial class QQBot : Form
    {
        private Thread th;
        private int y = 0;
        private delegate void InitialForms();
        private delegate void QQListOnlineHelpers(string uid, string online);
        private TCPClass tcp;
        private List<Message> msg = new List<Message>();
        private  XmlDocument xml = new XmlDocument();
        public QQBot()
        {
            InitializeComponent();
            ////MessageBox.Show(OtherHelper.Television("湖南"));
            //xml.Load("qq.xml");
            //QQUser.QQID = "1007310431";
            //QQUser.Password = MD5Helper.ToMD5("LONGCHEN");
            //this.th = new Thread(new ThreadStart(this.Initial));
            //this.th.IsBackground = true;
            //this.th.Start();
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            t.AutoReset = true;
            ////设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;
            ////是否执行System.Timers.Timer.Elapsed事件； 
            //OtherHelper.SetAutoRun("QQBot", Application.StartupPath + "//QQBot.exe");
            //SendMail();
            
        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            //this.GetQQListIsOnline("0");
            SendMail();
        }
        private void Initial()
        {
            this.tcp = new TCPClass();
            int num = this.tcp.Connection();
            if (num == 0)
            {
                MessageBox.Show("连接服务器失败！请检查网络！");
                base.Invoke(new QQBot.InitialForms(this.CloseForms), new object[0]);
            }
            else
            {
                this.tcp.messageHelper += new TCPClass.MessageHelper(this.tcp_messageHelper);
                this.Login();
            }
        }
        public void Execute()
        {
           
        }
        public void QQqun(string qq)
        {
            string data = string.Format("VER=1.4&CON=1&CMD=GROUP_CMD&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&LV=2&UN={0}\n",qq, QQUser.QQID, this.GetSjs3Byte());
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            this.tcp.Send(bytes);
        }
        private void tcp_messageHelper(byte[] msg)
        {
            base.Invoke(new TCPClass.MessageHelper(this.ShowMessage), new object[]
			{
				msg
			});
        }
        public void ShowMessage(string qq)
        {
            string data = string.Format("VER=1.4&CON=1&CMD=GetInfo&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&LV=2&UN={0}\n", qq, QQUser.QQID, this.GetSjs3Byte());
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            this.tcp.Send(bytes);
        }
        
            private void Login()
        {
            string data = string.Format("VER=1.4&CON=1&CMD=Login&SEQ={0}&UIN={1}&PS={2}&M5=1&LG=0&LC=812822641C978097&GD=TW00QOJ9KUVD753S&CKE=\n", this.GetSjs3Byte(), QQUser.QQID, QQUser.Password);
            this.tcp.Send(data);
        }
        private void CloseForms()
        {
            this.tcp.Close();
            this.tcp = null;
            base.Close();
        }
        public void SendMail()
        {
            try
            {
                OtherHelper.SendMail("1049058427@qq.com", "提醒", "longchen@mrpann.com", "坑爹的小号登陆了", "时间" + DateTime.Now, "1049058427@qq.com", "longchen520", "smtp.qq.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string GetSjs3Byte()
        {
            Random random = new Random();
            return random.Next(200, 999).ToString();
        }
        private void SendQQMsg(string qqid, string msg)
        {
            string data = string.Format("VER=1.4&CON=1&CMD=CLTMSG&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={2}&MG={3}\n", new object[]
			{
				this.GetSjs3Byte(),
				QQUser.QQID,
				qqid,
				msg
			});
            this.tcp.Send(data);
        }
        private void ShowMessage(byte[] msg)
        {
            string @string = Encoding.UTF8.GetString(msg, 0, msg.Length);
            if (@string.IndexOf("VER") != -1)
            {
                string[] array = @string.Split(new char[]
				{
					'&'
				});
                array[array.Length - 1] = array[array.Length - 1].Split(new char[]
                {
                    '\r'
                })[0];
                string text = array[1];

                switch (text)
                {
                    case "CMD=Login":
                        text = array[5];
                        if (text != null)
                        {
                            if (!(text == "RS=0"))
                            {
                                if (text == "RS=1")
                                {
                                    MessageBox.Show("密码错误！");
                                    this.CloseForms();
                                }
                            }
                            else
                            {
                                //SendMail();
                                SendQQMsg("1049058427", string.Format("我登陆了，煞笔。"));
                                //string tem = OtherHelper.Television("湖南");
                                //SendQQMsg("1049058427",tem);
                            }
                        }
                        break;
                    case "CMD=VERIFYCODE":
                        {
                            string hex = array[7].Split(new char[]
					{
						'='
					})[1];
                            byte[] buffer = Tools.HexStringToBytes(hex);
                            Bitmap img = new Bitmap(new MemoryStream(buffer));
                            Validate validate = new Validate(this.tcp, img);
                            validate.Show();
                            break;
                        }
                    case "CMD=QUERY_STAT2":
                        if (array[4] == "RES=20")
                        {
                            this.Login();
                        }
                        else
                        {
                            string a = array[5].Split(new char[]
						{
							'='
						})[1];
                            string[] array2 = array[8].Split(new char[]
						{
							','
						});
                            if (a != "1")
                            {
                                this.GetQQListIsOnline(array2[array2.Length - 2]);
                            }
                            //this.QQListOnlineHelper(array[8], array[7]);
                        }
                        break;
                    case "CMD=update_stat":
                        this.UpdateStat(array[6].Split(new char[]
					{
						'='
					})[1], array[5].Split(new char[]
					{
						'='
					})[1]);
                        break;
                    case "CMD=Server_Msg":
                        {

                            string text3 = array[7].Split(new char[]
					{
						'='
					})[1];
                            string text4 = array[8].Split(new char[]
					{
						'='
					})[1].Split(new char[]
					{
						'\n'
					})[0];
                            //bool tt = true;
                            //if (text4.Contains("湖南台节目"))
                            //{
                            //    string[] result = OtherHelper.Television("湖南卫视");
                            //    foreach (string t in result)
                            //    {
                            //        SendQQMsg(text3, t);
                            //    }

                            //}
                            //else
                            //{
                            //    SendQQMsg(text3, Replay(text4));
                            //}//SendQQMsg(text3, "");
                            SendQQMsg(text3, Replay(text4,text3));
                            break;
                        }
                    default :
                        break;
                }
            }
        }
        //对收到的信息作出处理
        private string Replay(string message,string id="")
        {
            string reply="";

            if (message == "")
            {
                int[] RandNum = new int[10];
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    RandNum[i] = r.Next(11);
                }
                foreach (int ii in RandNum)
                {
                    switch (ii)
                    {
                        case 1: reply = "你怎么不说话呀？";
                            break;
                        case 2: reply = "你怎么什么都不说呀？";
                            break;
                        case 3: reply = "再不说，我就生气咯~~";
                            break;
                        case 4: reply = "你是不是点错“发送”按钮了？";
                            break;
                        case 5: reply = "下次要想好说什么再发送哦";
                            break;
                        case 6: reply = "......";
                            break;
                        case 7: reply = "你在说什么？";
                            break;
                        case 8: reply = "我蛋都碎了~~~";
                            break;
                        case 9: reply = "你这句是无字天书吗？";
                            break;
                        default: reply = "这是什么呀？";
                            break;
                    }
                }
            }
            else
            {
                //回答当天日期
                if ((message.Contains("今天")) & (message.Contains("星期几")) || (message.Contains("今天")) & (message.Contains("几号")))
                {
                    string date = string.Format("{0:今天是yyyy年M月d日,dddd}", DateTime.Now);
                    reply = date;
                }
                //加法运算
                else if ((message.Contains("+")) & (message.Contains("等于多少")) || (message.Contains("+")) & (message.Contains("=?")) || (message.Contains("加")) & (message.Contains("=？")))
                {
                    string[] Add = message.Split('+', '=', '等');
                    double add1 = double.Parse(Add[0]);
                    double add2 = double.Parse(Add[1]);
                    string add = Add[0] + " + " + Add[1] + " = " + (add1 + add2);
                    reply = add;
                }
                else if ((message.Contains("www")) || (message.Contains("WWW")))
                {
                    reply = "你是想输入网址吗？";
                }
                else if (message.Contains("md5") || message.Contains("MD5"))
                {
                    string str = message.Replace("md5", "").Replace("MD5", "").Trim();
                    if (str.Length > 0)
                    {
                        reply = "加密后：" + MD5Helper.ToMD5(str);
                    }
                    else
                    {
                        reply = "小安安很聪明，怎么会不知道MD5...";
                    }
                }
                else if (id == "1049058427" && message.Contains("我的段位"))
                {
                    reply = OtherHelper.LOL("Mr潘安", "诺克萨斯");
                }
                else if (message.Contains("LOL") || message.Contains("英雄联盟"))
                {
                    string[] lol = message.Split(' ');
                    if (lol.Length == 4)
                    {
                        string a = OtherHelper.LOL(lol[2], lol[1]);
                        if (a.Length > 0)
                        {
                            reply = a;
                        }
                        else
                        {
                            reply = "小安安没去过这个鬼地方哎...";
                        }
                    }
                    else
                        reply = "小安安在上分，别烦我！";
                }
                else if (message.Contains("天气"))
                {
                    string a = message.Replace("天气", "");
                    if (a.Length > 0)
                    {
                        reply = OtherHelper.ShowWeather(a);
                    }
                    else
                    {
                        reply = "小安安没去过这个鬼地方哎...";
                    }
                }
                else if ((message.Contains("小安安 搞基")))
                {
                    string temp = message.Replace("小安安 搞基", "");
                    string[] c = temp.Split(' ');
                    if (c.Length == 4)
                    {
                        string teach = c[1];
                        string study = c[2];
                        reply = OtherHelper.Study(teach, study);
                        xml.Load("qq.xml");
                    }
                    else
                    {
                        reply = "你叫我干嘛，真要搞基啊？还是...(格式：小安安 搞基 潘安 潘安是帅哥！)";
                    }
                }
                else if (message.Contains("小安安说"))
                {
                    string str = message.Replace("小安安说", "").Trim();
                    if (str.Length > 0)
                    {
                        reply = str;
                    }
                    else
                    {
                        reply = "要小安安说啥？(格式：小安安说我爱你)";
                    }
                }
                else if (message.Contains("龙晨") || message.Contains("潘安"))
                {
                    int[] RandNum = new int[10];
                    Random r = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        RandNum[i] = r.Next(5);
                    }
                    foreach (int mess in RandNum)
                    {
                        switch (mess)
                        {
                            case 1: reply = "小安安的主人很厉害的哦...";
                                break;
                            case 2: reply = "卧槽，你连小安安主人都认识！";
                                break;
                            case 3: reply = "在一个晚上，我的主人问我，今天怎么不开心！";
                                break;
                            case 4: reply = "主人太懒，很久没管我了,~~~~(>_<)~~~~ ";
                                break;
                            default: reply = "找我主人吖？(⊙o⊙)…";
                                break;
                        }
                    }
                }
                else if (xml.SelectSingleNode("study").SelectSingleNode(message) != null)
                {
                    reply = xml.SelectSingleNode("study").SelectSingleNode(message).InnerText;
                }
                else if (message.Contains("快递"))
                {
                    string num = message.Substring(message.IndexOf("快递"), message.Length - message.IndexOf("快递")).Remove(0, 2);
                    string name = message.Substring(0, message.IndexOf("快递"));
                    string result = OtherHelper.kuaidi(StrToPinyin.GetChineseFullSpell(name), num);
                    if (result.Length > 0)
                    {
                        reply = result + "(小安安只会给你发最近的一条物流消息哦...么么哒)";
                    }
                    else
                    {
                        reply = "好奇怪，小安安找不到这个快递...(例如:申通快递14384389438)";
                    }
                }
                else if (message.Contains("湖南台节目"))
                {
                    string[] result = OtherHelper.Television("湖南卫视");
                    //foreach (string i in result)
                    //{
                    //    SendQQMsg(id, i);
                    //}
                    int lenth = result.Length;
                    for (int i = 0; i < lenth / 5; i++)
                    {
                        SendQQMsg(id, result[i]);
                    }
                }
                else if (message.Contains("翻译"))
                {
                    string temp = message.Replace("翻译", "").Trim();
                    string result = OtherHelper.Translator(temp);
                    //foreach (string i in result)
                    //{
                    //    SendQQMsg(id, i);
                    //}
                    if (result.Length > 0)
                    {
                        reply = result;
                    }
                    else
                    {
                        reply = "好奇怪，小安安找不到这个单词哎...(例如:翻译你好)";
                    }
                }
                else if (message.Contains("身份证"))
                {
                    string num = message.Replace("身份证", "").Trim();
                    string result = OtherHelper.IDInfo(num);
                    if (result.Length > 0)
                    {
                        reply = result + "(小安安是不是好厉害...么么哒)";
                    }
                    else
                    {
                        reply = "好奇怪，小安安找不到这个死基佬...(例如:身份证14384389438)";
                    }
                }
                else
                {
                    reply = OtherHelper.DialogReturn();
                }
            }
            return reply;
        }
        private void GetQQListIsOnline(string un)
        {
            string s = string.Format("VER=1.4&CON=1&CMD=Query_Stat2&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&CM=2&UN={2}\n", this.GetSjs3Byte(), QQUser.QQID, un);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            this.tcp.Send(bytes);
        }
        private void GetQQList(string un)
        {
            string s = string.Format("VER=1.4&CON=1&CMD=SimpleInfo2&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={2}&TO=0\n", this.GetSjs3Byte(), QQUser.QQID, un);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            this.tcp.Send(bytes);
        }
        private void QQListHelper(string uid, string nk)
        {
            string[] array = uid.Split(new char[]
			{
				'='
			})[1].Split(new char[]
			{
				','
			});
            string[] array2 = nk.Split(new char[]
			{
				'='
			})[1].Split(new char[]
			{
				','
			});
            for (int i = 0; i < array.Length - 1; i++)
            {
                QQLists.Qqlists.Add(new QQList(array[i], array2[i]));
            }
        }
        private void UpdateStat(string uid, string online)
        {
            
        }
    }
}
