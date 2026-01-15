using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace OGRENCITAKIPPROGRAMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btntelefon_Click(object sender, EventArgs e)
        {
            frmtelefon frm=new frmtelefon();
            frm.form = "telefon";
            frm.Show();
            
        }

        private void btnizin_Click(object sender, EventArgs e)
        {
            frmizin frm=new frmizin();
            frm.Show();

        }

        private void btnuniform_Click(object sender, EventArgs e)
        {
            frmuniforma frm = new frmuniforma();
            frm.Show();
        }

        private void lnkogrkaydet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmogrencikaydet frm = new frmogrencikaydet();
            frm.Show();
        }

        private void lnkcikis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Bu program CENGİZ YILMAZ tarafından 2026 yılında yapılmıştır. Bilgi için muallimiturki@gmail.com adresine ileti gönderebilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
