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
        private delegate void MyDelegate(object iObj);
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
            for (int i = 0; i < 10; i++)
            {
                var iObj = new int[] { i };
                IAsyncResult res = textBox1.BeginInvoke(new MyDelegate(textBox1Change), iObj );
                textBox1.EndInvoke(res);
                for (int j = trackBar1.Value; j > 0; j--)
                {
                    var jObj = new int[] { j };
                    res = textBox2.BeginInvoke(new MyDelegate2(textBox2Change), jObj);
                    textBox2.EndInvoke(res);
                    //textBox2.Text = j.ToString();
                    //Thread.Sleep(200);
                }
            }
        }

        private void textBox1Change(object o)
        {
            if (o != null && o is int[] && ((int[])o).Length > 0)
            {
                var i = ((int[])o);
                textBox1.Text += Fibonachi(i[0]).ToString() + "; ";
            }
        }

        private void textBox2Change(object o)
        {
            if (o != null && o is int[] && ((int[])o).Length > 0)
            {
                var j = ((int[])o);
                textBox2.Text = j[0].ToString();
                Thread.Sleep(200);
            }
        }
    }
}
