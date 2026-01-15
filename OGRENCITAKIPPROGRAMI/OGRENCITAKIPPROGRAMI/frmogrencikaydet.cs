using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OGRENCITAKIPPROGRAMI
{
    public partial class frmogrencikaydet : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public frmogrencikaydet()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible == true)
            {
                button2.Visible = false;
                this.WindowState = FormWindowState.Maximized;
                button5.Visible = true;
            }
        }

        baglantisinif con= new baglantisinif();
      
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Visible == true)
            {
                button5.Visible = false;
                this.WindowState = FormWindowState.Normal;
                button2.Visible = true;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        void listele()
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand komutlistele = new OleDbCommand("SELECT  OGRID AS 'ID', OGRADSOYAD AS 'ADI SOYADI',SINIFAD AS 'SINIFI', OGRSINIF, OGRNUMARA AS 'NUMARASI',OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON' from TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF", conn);

            OleDbDataAdapter da = new OleDbDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[3].Visible = false;
            conn.Close();
        }
        void sinifgoruntule()
        {
            OleDbConnection conn= new OleDbConnection(con.baglan);
            conn.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select SINIFID, SINIFAD from TBLSINIF", conn);
            cmbsinif.DisplayMember = "SINIFAD";
            cmbsinif.ValueMember = "SINIFID";
            da.Fill(dt);
            cmbsinif.DataSource=dt;
            conn.Close() ;
        }
        private void frmogrencikaydet_Load(object sender, EventArgs e)
        {
            listele();
            sinifgoruntule();

        }

        private void btnara_Click(object sender, EventArgs e)
        {
            listele();
        }
        Boolean var;
        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand komutogrencioku = new OleDbCommand("select OGRNUMARA FROM TBLOGRENCILER WHERE OGRNUMARA=@K1", conn);
            komutogrencioku.Parameters.AddWithValue("@K1", msknumara.Text);
            OleDbDataReader komutogrenciokurd = komutogrencioku.ExecuteReader();
           
                if(komutogrenciokurd.Read() == false)
                     {
                    var = false;
                      }
                else
                {
                    var=true;
                }

                
            
            conn.Close();
            DialogResult result1=MessageBox.Show("ÖĞRENCİ KAYDEDİLECEK ONAYLIYOR MUSUNUZ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            conn.Open();
            OleDbCommand komutkaydet = new OleDbCommand("insert into TBLOGRENCILER (OGRADSOYAD, OGRNUMARA, OGRSINIF, OGRBABATELEFON, OGRANNETELEFON) VALUES (@P1, @P2, @P3,@P4,@P5)", conn);
            komutkaydet.Parameters.AddWithValue("@P1", rchadsoyad.Text);
            komutkaydet.Parameters.AddWithValue("@P2", msknumara.Text);
            komutkaydet.Parameters.AddWithValue("@P3",cmbsinif.SelectedValue);
            komutkaydet.Parameters.AddWithValue("@P4", mskbabatelefon.Text);
            komutkaydet.Parameters.AddWithValue("@P5", mskannetelefon.Text);
            if (var == false &&result1==DialogResult.Yes)
            {
                if (rchadsoyad.Text != "" && msknumara.Text != "" && mskannetelefon.Text != "" && mskbabatelefon.Text != "")
                {
                    komutkaydet.ExecuteNonQuery();
                    MessageBox.Show(rchadsoyad.Text + " adlı öğrenci kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }
                else
                {
                    MessageBox.Show("Lütfen bilgileri eksiksiz giriniz.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show(msknumara.Text+" okul numaralı öğrenci zaten kayıtlı.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

                conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
         
            conn.Open();
            OleDbCommand komutsil = new OleDbCommand("delete from TBLOGRENCILER WHERE OGRID=@P1", conn);
            komutsil.Parameters.AddWithValue("@P1", mskıd.Text);
            if(mskıd.Text!="")
            {
                DialogResult result2 = MessageBox.Show(rchadsoyad.Text + " adlı öğrenci silinecek. Onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2==DialogResult.Yes)
                {
                    komutsil.ExecuteNonQuery();
                    MessageBox.Show(rchadsoyad.Text + "  adlı öğrenci silindi.");
                    listele();
                }
                
            }
            else
            {
                MessageBox.Show("Lütfen tablodan hücre seçiniz.");
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mskıd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                rchadsoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbsinif.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                msknumara.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                mskbabatelefon.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                mskannetelefon.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                

            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen tabloda erişmek istediğiniz hücrelere tıklayınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
          
            conn.Open();
             OleDbCommand komutguncelle=new OleDbCommand("update TBLOGRENCILER SET OGRADSOYAD=@P1, OGRNUMARA=@P2, OGRSINIF=@P3, OGRBABATELEFON=@P4, OGRANNETELEFON=@P5 WHERE OGRID=@P0",conn);
           
            komutguncelle.Parameters.AddWithValue("@P1", rchadsoyad.Text);
            komutguncelle.Parameters.AddWithValue("@P2", msknumara.Text);
            komutguncelle.Parameters.AddWithValue("@P3", cmbsinif.SelectedValue);
            komutguncelle.Parameters.AddWithValue("@P4", mskbabatelefon.Text);
            komutguncelle.Parameters.AddWithValue("@P5", mskannetelefon.Text);
            komutguncelle.Parameters.AddWithValue("@P0", mskıd.Text);
            if (mskıd.Text != "")
            {
                DialogResult result3 = MessageBox.Show(rchadsoyad.Text + "  adlı öğrenci bilgileri güncellenecek. Onaylıyor musunuz?", "Bilgi",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result3== DialogResult.Yes)
                {
                    komutguncelle.ExecuteNonQuery();
                    MessageBox.Show(mskıd.Text + " ıd numaralı öğrenci bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }
            }

            else
            {
                MessageBox.Show(" Lütfen tablodan güncellemek istediğiniz hücreyi seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();

        }

        private void rchadsoyad_MouseClick(object sender, MouseEventArgs e)
        {
            rchadsoyad.SelectionStart = rchadsoyad.Text.Length;
        }

        private void msknumara_MouseClick(object sender, MouseEventArgs e)
        {
            msknumara.SelectionStart= msknumara.Text.Length;
        }

        private void mskannetelefon_MouseClick(object sender, MouseEventArgs e)
        {
            mskannetelefon.SelectionStart = mskannetelefon.Text.Length;
        }

        private void mskbabatelefon_MouseClick(object sender, MouseEventArgs e)
        {
            mskbabatelefon.SelectionStart = mskbabatelefon.Text.Length;
        }

        private void btnogrenciara_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand ogrenciara = new OleDbCommand("SELECT  OGRID AS 'ID', OGRADSOYAD AS 'ADI SOYADI',SINIFAD AS 'SINIFI', SINIFID AS 'SINIFNO', OGRNUMARA AS 'NUMARASI',OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON' from TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRADSOYAD like '" + rchadsoyad.Text + "%' ", conn);
            OleDbDataAdapter da = new OleDbDataAdapter(ogrenciara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnraporal_Click(object sender, EventArgs e)
        {
            frmizinreport frmizinreport = new frmizinreport();
            frmizinreport.rol = "OGRENCI";
            frmizinreport.Show();
        }
    }
}
