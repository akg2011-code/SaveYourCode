using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_Your_Code
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }
        //frm_add
        private void roundButton1_Click(object sender, EventArgs e)
        {
            PL.frm_add frm = new PL.frm_add();
            frm.ShowDialog();
        }

        //tas3'er 2lwondeos
        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        //close all main form
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //frm_view
        private void roundButton2_Click(object sender, EventArgs e)
        {
            PL.frm_view frm = new PL.frm_view();
            frm.ShowDialog();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {

        }
        //frm_about
        private void roundButton3_Click(object sender, EventArgs e)
        {
            PL.frm_about frm = new PL.frm_about();
            frm.ShowDialog(); 
        }

        //Move Form From His Body
        private void frm_main_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,
                WM_NCLBUTTONDOWN,
                HTCAPTION, 0);
        }


        
        [DllImport("user32.dll")]
        static extern int SendMessage(
    IntPtr hWnd,
    int Msg,
    int wParam,
    int lParam);
        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();
        const int WM_NCLBUTTONDOWN = 0xA1; // 161
        const int HTCAPTION = 2;
    }
}
