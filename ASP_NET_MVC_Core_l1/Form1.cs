using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_NET_MVC_Core_l1
{
    public partial class Form1 : Form
    {
        //private delegate void MyDelegate(object iObj);
        private delegate void MyDelegate2(object iObj);
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            trackBar1.Value = 10;
            textBox2.Text = trackBar1.Value.ToString();
        }

        private int Fibonachi(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }
            return Fibonachi(n - 1) + Fibonachi(n - 2);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            trackBar1.Enabled = false;
            trackBar1.Refresh();
            textBox1.Text = "";
            textBox1.Refresh();
            for (int i = 0; i < 10; i++)
            {
                object iObj = i;
                int ii = ((int)iObj);
                Invoke((MethodInvoker) delegate
                {
                    textBox1.Text += Fibonachi(ii).ToString() + "; ";
                    textBox1.Refresh();
                });
                //Thread.Sleep(200);
                //textBox1.EndInvoke(res);
                for (int j = trackBar1.Value; j > 0; j--)
                {
                    object jObj = j;
                    IAsyncResult res = textBox2.BeginInvoke(new MyDelegate2(textBox2Change), jObj);
                    textBox2.EndInvoke(res);
                    Thread.Sleep(200);
                }
            }
            buttonStart.Enabled = true;
            trackBar1.Enabled = true;
        }

        //private void textBox1Change(object o)
        //{
        //    if (o != null && o is int)
        //    {
        //        int i = ((int)o);
        //        textBox1.Text += Fibonachi(i).ToString() + "; ";
        //        textBox1.Refresh();
        //    }
        //}

        private void textBox2Change(object o)
        {
            if (o != null && o is int)
            {
                int j = ((int)o);
                textBox2.Text = j.ToString();
                textBox2.Refresh();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
        }
    }
}
