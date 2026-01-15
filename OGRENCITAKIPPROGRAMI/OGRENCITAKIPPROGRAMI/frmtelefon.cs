using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace OGRENCITAKIPPROGRAMI
{

    public partial class frmtelefon : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public frmtelefon()
        {
            InitializeComponent();

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
;        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible==true)
            {
                this.WindowState = FormWindowState.Maximized;
                button2.Visible = false;
                button5.Visible = true;
            }
        }
        public string form;
        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            if (button5.Visible==true)
            {
                this.WindowState = FormWindowState.Normal;
                button2.Visible = true;
                button5.Visible = false;
            }
        }

        private void frmtelefon_Load(object sender, EventArgs e)
        {
            if (form == "telefon")
            {
                splitContainer2.Visible = true;
            }

            listele();
        }

        private void lblannenumara_Click(object sender, EventArgs e)
        {
            lblaranantelefon.Text = "";
            lblaranantelefon.Text=lblannenumara.Text;
        }

        private void lblbabanumara_Click(object sender, EventArgs e)
        {
            lblaranantelefon.Text = "";
            lblaranantelefon.Text=lblbabanumara.Text;
        }
        baglantisinif con = new baglantisinif();
        void listele()
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT TELID AS 'ID', OGRADSOYAD  AS 'AD SOYAD', OGRNUMARATELEFON AS 'NUMARASI', SINIFAD AS 'SINIFI', OGRTARIHTELEFON AS 'TARİH', OGRMAZERETTELEFON AS 'MAZERETİ' FROM (TBLTELEFONKAYIT  INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON)  INNER JOIN TBLSINIF ON TBLTELEFONKAYIT.OGRSINIFTELEFON=TBLSINIF.SINIFID ", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;


            conn.Close();

            conn.Open();
            OleDbDataAdapter daasilliste = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF", conn);
            DataTable dt2 = new DataTable();
            daasilliste.Fill(dt2);
            dataGridView1.DataSource = dt2;
            dataGridView1.Columns["OGRSINIF"].Visible = false;
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TELID AS 'ID', OGRADSOYAD  AS 'AD SOYAD', OGRNUMARATELEFON AS 'NUMARASI', SINIFAD AS 'SINIFI', OGRTARIHTELEFON AS 'TARİH', OGRSAATTELEFON AS 'SAAT', OGRMAZERETTELEFON AS 'MAZERETİ' FROM (TBLTELEFONKAYIT  INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON)  INNER JOIN TBLSINIF ON TBLTELEFONKAYIT.OGRSINIFTELEFON=TBLSINIF.SINIFID WHERE OGRADSOYAD LIKE '" + rchadsoyad.Text + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRADSOYAD LIKE '" + rchadsoyad.Text + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }

        private void cmbsinif_SelectedValueChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TELID AS 'ID', OGRADSOYAD  AS 'AD SOYAD', OGRNUMARATELEFON AS 'NUMARASI', SINIFAD AS 'SINIFI', OGRTARIHTELEFON AS 'TARİH',OGRSAATTELEFON AS 'SAAT', OGRMAZERETTELEFON AS 'MAZERETİ' FROM (TBLTELEFONKAYIT  INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON)  INNER JOIN TBLSINIF ON TBLTELEFONKAYIT.OGRSINIFTELEFON=TBLSINIF.SINIFID WHERE OGRSINIF LIKE '" + cmbsinif.SelectedValue+ "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRSINIF LIKE '" + cmbsinif.SelectedValue + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TELID AS 'ID', OGRADSOYAD  AS 'AD SOYAD', OGRNUMARATELEFON AS 'NUMARASI', SINIFAD AS 'SINIFI', OGRTARIHTELEFON AS 'TARİH',OGRSAATTELEFON AS 'SAAT', OGRMAZERETTELEFON AS 'MAZERETİ' FROM (TBLTELEFONKAYIT  INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON)  INNER JOIN TBLSINIF ON TBLTELEFONKAYIT.OGRSINIFTELEFON=TBLSINIF.SINIFID WHERE OGRNUMARATELEFON LIKE '" + rchnumara.Text + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRNUMARA LIKE '" + rchnumara.Text + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            conn.Close();
        }

        private void btnizinkaydet_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lbladisoyadi.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblsinifi.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                lblnumarasi.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
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
                lblsinifi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                lblnumarasi.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
        }

        public int ogrenciid;
        void bilgigetir()
        {

            lblannenumara.Text = "";
            lblbabanumara.Text = "";
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
            OleDbCommand komutoku3 = new OleDbCommand("select OGRBABATELEFON FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku3.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku3rd = komutoku3.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku3rd.Read())
                {
                    lblbabanumara.Text = komutoku3rd[0].ToString();
                }
            }
            conn.Close();
            conn.Open();
            OleDbCommand komutoku4 = new OleDbCommand("select OGRANNETELEFON FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku4.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku4rd = komutoku4.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku4rd.Read())
                {
                    lblannenumara.Text = komutoku4rd[0].ToString();
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
                    ogrenciid = int.Parse(komutoku5rd[0].ToString());
                    ogrsinif = int.Parse(komutoku5rd[1].ToString());
                }
            }
            conn.Close();
        }
        public int numara;
        public int ogrsinif;

        private void lblnumarasi_TextChanged(object sender, EventArgs e)
        {
            numara = int.Parse(lblnumarasi.Text);
            bilgigetir();
        }

        private void btntelefonaramakaydet_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand komuttelefonkaydet = new OleDbCommand("insert into TBLTELEFONKAYIT  (OGRADSOYADTELEFON,OGRNUMARATELEFON,OGRSINIFTELEFON, OGRTARIHTELEFON, OGRSAATTELEFON, OGRMAZERETTELEFON) VALUES (@P1, @P2, @P3, @P4,@S1, @P5, )", conn);
            komuttelefonkaydet.Parameters.AddWithValue("@P1", ogrenciid);
            komuttelefonkaydet.Parameters.AddWithValue("@P2", lblnumarasi.Text);
            komuttelefonkaydet.Parameters.AddWithValue("@P3",ogrsinif);
            komuttelefonkaydet.Parameters.AddWithValue("@P4", DateTime.Now.ToString("dd.MM.yyyy"));
            komuttelefonkaydet.Parameters.AddWithValue("@S1", DateTime.Now.ToString("HH.mm"));
            komuttelefonkaydet.Parameters.AddWithValue("@P5", rchmazeret.Text);

            if (lblnumarasi.Text != "")
            {
                komuttelefonkaydet.ExecuteNonQuery();
                MessageBox.Show("Telefon araması kaydedildi.");
                listele();

            }
            else
            {
                MessageBox.Show("Lütfen tablodan öğrenci seciniz");
            }
                conn.Close();
        }
        
        private void btnara_Click(object sender, EventArgs e)
        {
            frmizinreport frmizinreport = new frmizinreport();
            frmizinreport.rol = "TELEFON";
            frmizinreport.Show();
        }

        private void dtptarih_ValueChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT TELID AS 'ID', OGRADSOYAD  AS 'AD SOYAD', OGRNUMARATELEFON AS 'NUMARASI', SINIFAD AS 'SINIFI', OGRTARIHTELEFON AS 'TARİH',OGRSAATTELEFON AS 'SAAT', OGRMAZERETTELEFON AS 'MAZERETİ' FROM (TBLTELEFONKAYIT  INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON)  INNER JOIN TBLSINIF ON TBLTELEFONKAYIT.OGRSINIFTELEFON=TBLSINIF.SINIFID WHERE OGRTARIHTELEFON LIKE '" + dtptarih.Value + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();

           ;
        }
    }
}
