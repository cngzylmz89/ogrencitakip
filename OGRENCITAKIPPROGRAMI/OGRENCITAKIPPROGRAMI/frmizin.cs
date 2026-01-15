using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace OGRENCITAKIPPROGRAMI
{
    public partial class frmizin : Form
    {

        
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public frmizin()
        {
            InitializeComponent();
        }
        baglantisinif con=new baglantisinif();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible == true)
            {
                this.WindowState = FormWindowState.Maximized;
                button2.Visible = false;
                button5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Visible == true)
            {
                this.WindowState = FormWindowState.Normal;
                button2.Visible = true;
                button5.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //DATAGRİDDE LİSTELEME YAPAR
        void listele()
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI', IZINOGRIZINTARIH AS 'İZİN TARİHİ',IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERETİ', IZINOGRIZINALANKISI AS 'İZİN ALAN KİŞİ' FROM( (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER  JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF) ", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;


            conn.Close();

            conn.Open();
            OleDbDataAdapter daasilliste = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF", conn);
            DataTable dt2 = new DataTable();
            daasilliste.Fill(dt2);
            dataGridView3.DataSource = dt2;
            dataGridView3.Columns["OGRSINIF"].Visible = false;
            conn.Close();

            conn.Open();
            OleDbDataAdapter dasinif = new OleDbDataAdapter("select SINIFID, SINIFAD FROM TBLSINIF", conn);
            DataTable dt3 = new DataTable();
            dasinif.Fill(dt3);
            cmbsinif.DataSource = dt3;
            cmbsinif.DisplayMember = "SINIFAD";
            cmbsinif.ValueMember = "SINIFID";
            conn.Close();
        }
        
        private void rchadsoyad_TextChanged(object sender, EventArgs e)
        {
            rchadsoyad.Text.ToUpper();
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRADSOYAD LIKE '" + rchadsoyad.Text + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView3.DataSource = dt2;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI',OGRADSOYAD AS 'AD SOYAD', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI', IZINOGRIZINTARIH AS 'TARİH',IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERET', IZINOGRIZINALANKISI AS 'ALAN KİŞİ' FROM (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF WHERE OGRADSOYAD LIKE '" + rchadsoyad.Text + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }
        private void frmizin_Load(object sender, EventArgs e)
        {

            listele();

           
        }

        private void cmbsinif_SelectedValueChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();

            if (cmbsinif.Text == "TÜMÜ")
            {
                OleDbDataAdapter da4 = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF ", conn);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                dataGridView3.DataSource = dt4;
            }

            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE SINIFAD LIKE '" + cmbsinif.Text + "%' ", conn);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                dataGridView3.DataSource = dt2;
            }
                
            conn.Close();

            conn.Open();
            if (cmbsinif.Text == "TÜMÜ")
            {
                OleDbDataAdapter da3 = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI',OGRADSOYAD AS 'AD SOYAD', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI', IZINOGRIZINTARIH AS 'TARİH',IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERET', IZINOGRIZINALANKISI AS 'ALAN KİŞİ' FROM (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF", conn);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                dataGridView1.DataSource = dt3;
            }
            else
            {
                OleDbDataAdapter da2 = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI',OGRADSOYAD AS 'AD SOYAD', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI',  IZINOGRIZINTARIH AS 'TARİH',  IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERET', IZINOGRIZINALANKISI AS 'ALAN KİŞİ' FROM (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF WHERE SINIFAD LIKE '" + cmbsinif.Text + "%' ", conn);
                DataTable dt3 = new DataTable();
                da2.Fill(dt3);
                dataGridView1.DataSource = dt3;
            }
                
           
            conn.Close();


        }

        private void rchnumara_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRNUMARA LIKE '" + rchnumara.Text + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView3.DataSource = dt2;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI',OGRADSOYAD AS 'AD SOYAD', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI', IZINOGRIZINTARIH AS 'TARİH', IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERET', IZINOGRIZINALANKISI AS 'ALAN KİŞİ' FROM (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF WHERE IZINOGRNUMARA LIKE '" + rchnumara.Text + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lbladisoyadi.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblsinifi.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                lblnumarasi.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lbladisoyadi.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblsinifi.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                lblnumarasi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                bilgigetir();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
        }
        public int ogrenciıd;
        void bilgigetir()
        {
           
            lblizinsayisi.Text = "";
            lblensonizintarihi.Text = "";
            lblbabatelefon.Text = "";
            lblannetelefon.Text = "";
            OleDbConnection conn = new OleDbConnection(con.baglan);

            conn.Open();
            OleDbCommand komutokupic = new OleDbCommand("select  OGRFOTOGRAFYOL from TBLOGRENCILER WHERE OGRNUMARA=@P1", conn);
            komutokupic.Parameters.AddWithValue("@P1", numara);
            OleDbDataReader komutokupicrd = komutokupic.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutokupicrd.Read())
                {
                    pckbxogrenci.ImageLocation = komutokupicrd[0].ToString();
                }
            }
            conn.Close();

            

            conn.Open();
            OleDbCommand komutoku1 = new OleDbCommand("select  count(*) from TBLIZIN WHERE IZINOGRNUMARA=@P1", conn);
            komutoku1.Parameters.AddWithValue("@P1", numara);
            OleDbDataReader komutoku1rd = komutoku1.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku1rd.Read())
                {
                    lblizinsayisi.Text = komutoku1rd[0].ToString();
                }
            }
            conn.Close();

            conn.Open();
            OleDbCommand komutoku2 = new OleDbCommand("select  TOP 1 IZINOGRIZINTARIH from TBLIZIN WHERE IZINOGRNUMARA=@P2", conn);
            komutoku2.Parameters.AddWithValue("@P2", numara);
            OleDbDataReader komutoku2rd = komutoku2.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku2rd.Read())
                {
                   lblensonizintarihi.Text = komutoku2rd[0].ToString();
                }
            }
            conn.Close();

            conn.Open ();
            OleDbCommand komutoku3 = new OleDbCommand("select OGRBABATELEFON FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku3.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku3rd = komutoku3.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku3rd.Read())
                {
                    lblbabatelefon.Text=komutoku3rd[0].ToString();
                }
            }
            conn.Close ();
            conn.Open();
            OleDbCommand komutoku4 = new OleDbCommand("select OGRANNETELEFON FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku4.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku4rd = komutoku4.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku4rd.Read())
                {
                    lblannetelefon.Text = komutoku4rd[0].ToString();
                }
            }
            conn.Close();

            conn.Open();
            OleDbCommand komutoku5 = new OleDbCommand("select OGRID, OGRSINIF FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku5.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku5rd = komutoku5.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku5rd.Read())
                {
                    ogrenciıd =int.Parse( komutoku5rd[0].ToString());
                    ogrsinif=int.Parse(komutoku5rd [1].ToString());
                }
            }
            conn.Close();
        }
        public int numara;
        public int ogrsinif;
        private void lblnumarasi_TextChanged(object sender, EventArgs e)
        {
            numara=int.Parse(lblnumarasi.Text);
            bilgigetir();
        }

        private void btnizinkaydet_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand komutizinkaydet = new OleDbCommand("insert into TBLIZIN (IZINOGRADSOYAD, IZINOGRSINIF, IZINOGRNUMARA, IZINOGRIZINTARIH, IZINOGRIZINSAAT, IZINOGRIZINMAZERET, IZINOGRIZINALANKISI) VALUES (@P1, @P2, @P3, @S1,@P4, @P5, @P6)", conn);
            komutizinkaydet.Parameters.AddWithValue("@P1", ogrenciıd);
            komutizinkaydet.Parameters.AddWithValue("@P2",ogrsinif);
            komutizinkaydet.Parameters.AddWithValue("@P3", lblnumarasi.Text);
            komutizinkaydet.Parameters.AddWithValue("@S1", DateTime.Now.ToString("dd.MM.yyyy"));
            komutizinkaydet.Parameters.AddWithValue("@P4", DateTime.Now.ToString("HH:mm"));
            komutizinkaydet.Parameters.AddWithValue("@P5", rchmazeret.Text);
            komutizinkaydet.Parameters.AddWithValue("@P6", rchogrencialankisi.Text);
            if (lblnumarasi.Text != "")
            {
                komutizinkaydet.ExecuteNonQuery();
                MessageBox.Show("İZİN KAYDEDİLDİ");
                listele();
                
            }
            else
            {
                MessageBox.Show("Lütfen tablodan öğrenci seçiniz.");
            }

                conn.Close();
        }
        string tarih;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tarih=dateTimePicker1.Value.ToString("dd.MM.yyyy");
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select ID AS 'SIRA NUMARASI',OGRADSOYAD AS 'AD SOYAD', SINIFAD AS 'SINIFI', IZINOGRNUMARA AS 'NUMARASI', IZINOGRIZINTARIH AS 'TARİH', IZINOGRIZINSAAT AS 'SAAT', IZINOGRIZINMAZERET AS 'MAZERET', IZINOGRIZINALANKISI AS 'ALAN KİŞİ' FROM (TBLIZIN INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLIZIN.IZINOGRADSOYAD) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLIZIN.IZINOGRSINIF WHERE IZINOGRIZINTARIH LIKE '" + tarih + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmizinreport frmizinreport = new frmizinreport();
            frmizinreport.rol = "IZIN";
            frmizinreport.Show();
        }
    }
}
