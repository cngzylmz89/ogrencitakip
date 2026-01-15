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

namespace OGRENCITAKIPPROGRAMI
{
    public partial class frmuniforma : Form
    {


        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public frmuniforma()
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible == true)
            {
                button2.Visible=false;
                this.WindowState=FormWindowState.Maximized;
                button5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(button5.Visible == true)
            {
                button5.Visible=false;
                this.WindowState = FormWindowState.Normal;
                button2.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        baglantisinif con=new baglantisinif();
        void listele()
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter("select UNIID  AS 'SIRA NO', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARAUNI  AS 'NUMASARI', SINIFAD AS  'SINIFI', OGRTARIHUNI AS 'TARİHİ', OGRMAZERETUNI AS 'MAZERETİ' FROM (TBLUNIFORMA\r\nINNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI)\r\nINNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI ", conn);
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

        private void frmuniforma_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void rchadsoyad_TextChanged(object sender, EventArgs e)
        {
            rchadsoyad.Text.ToUpper();
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select UNIID  AS 'SIRA NO', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARAUNI  AS 'NUMASARI', SINIFAD AS  'SINIFI', OGRTARIHUNI AS 'TARİHİ', OGRMAZERETUNI AS 'MAZERETİ' FROM (TBLUNIFORMA INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI  WHERE OGRADSOYAD LIKE '" + rchadsoyad.Text + "%' ", conn);
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
            OleDbDataAdapter da = new OleDbDataAdapter("select UNIID  AS 'SIRA NO', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARAUNI  AS 'NUMASARI', SINIFAD AS  'SINIFI', OGRTARIHUNI AS 'TARİHİ', OGRMAZERETUNI AS 'MAZERETİ', OGRSINIFUNI FROM (TBLUNIFORMA INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI  WHERE OGRSINIFUNI LIKE '" + cmbsinif.SelectedValue + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            dataGridView2.Columns["OGRSINIFUNI"].Visible= false;
            conn.Close();

            conn.Open();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select OGRID AS 'SIRA NUMARASI', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARA AS 'NUMARASI', SINIFAD AS 'SINIFI',  OGRBABATELEFON AS 'BABA TELEFON', OGRANNETELEFON AS 'ANNE TELEFON', OGRSINIF FROM TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF WHERE OGRSINIF LIKE '" + cmbsinif.SelectedValue + "%' ", conn);
            DataTable dt3 = new DataTable();
            da2.Fill(dt3);
            dataGridView1.DataSource = dt3;
            dataGridView1.Columns["OGRSINIF"].Visible= false;
            conn.Close();
        }

        private void rchnumara_TextChanged(object sender, EventArgs e)
        {
            
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select UNIID  AS 'SIRA NO', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARAUNI  AS 'NUMASARI', SINIFAD AS  'SINIFI', OGRTARIHUNI AS 'TARİHİ', OGRMAZERETUNI AS 'MAZERETİ' FROM (TBLUNIFORMA INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI  WHERE OGRNUMARAUNI LIKE '" + rchnumara.Text + "%' ", conn);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select UNIID  AS 'SIRA NO', OGRADSOYAD AS 'ADI SOYADI', OGRNUMARAUNI  AS 'NUMASARI', SINIFAD AS  'SINIFI', OGRTARIHUNI AS 'TARİHİ', OGRMAZERETUNI AS 'MAZERETİ' FROM (TBLUNIFORMA INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI  WHERE OGRTARIHUNI LIKE '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "%' ", conn);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lbladisoyadi.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblnumarasi.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                lblsinifi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
        }
        public int ogrenciid;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lbladisoyadi.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblnumarasi.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                lblsinifi.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
        }

        void bilgigetir()
        {

            lbluniformasizsayisi.Text = "";
            lblensonuniformasiz.Text = "";
            lblbabatelefno.Text = "";
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
            OleDbCommand komutunisayi=new OleDbCommand("select count(*) from TBLUNIFORMA WHERE OGRNUMARAUNI=@k1", conn);
            komutunisayi.Parameters.AddWithValue("@k1", numara);
            OleDbDataReader komutunisayird = komutunisayi.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutunisayird.Read())
                {
                    lbluniformasizsayisi.Text = komutunisayird[0].ToString() ;
                }
            }
            
            conn.Close();

            conn.Open();
            OleDbCommand komutunienson = new OleDbCommand("select TOP 1 OGRTARIHUNI FROM TBLUNIFORMA WHERE OGRNUMARAUNI=@P1 ORDER BY UNIID DESC", conn);
            komutunienson.Parameters.AddWithValue("@P1", numara);
            OleDbDataReader komutuniensonrd = komutunienson.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutuniensonrd.Read())
                {
                    lblensonuniformasiz.Text = komutuniensonrd[0].ToString();
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

            conn.Open();
            OleDbCommand komutoku3 = new OleDbCommand("select OGRBABATELEFON FROM TBLOGRENCILER WHERE OGRNUMARA=@N1", conn);
            komutoku3.Parameters.AddWithValue("@N1", numara);
            OleDbDataReader komutoku3rd = komutoku3.ExecuteReader();
            if (lblnumarasi.Text != "")
            {
                while (komutoku3rd.Read())
                {
                    lblbabatelefno.Text = komutoku3rd[0].ToString();
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
                    lblannetelefon.Text = komutoku4rd[0].ToString();
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

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(con.baglan);
            conn.Open();
            OleDbCommand komutunikaydet = new OleDbCommand("insert into TBLUNIFORMA (OGRADSOYADUNI,OGRNUMARAUNI, OGRSINIFUNI, OGRTARIHUNI,OGRMAZERETUNI) VALUES (@P1, @P2,@P3,@P4,@P5)", conn);
            komutunikaydet.Parameters.AddWithValue("@P1", ogrenciid);
            komutunikaydet.Parameters.AddWithValue("@P2", numara);
            komutunikaydet.Parameters.AddWithValue("@P3", ogrsinif);
            komutunikaydet.Parameters.AddWithValue("@P4", DateTime.Now.ToString("dd.MM.yyyy"));
            komutunikaydet.Parameters.AddWithValue("@P5", rchmazeret.Text);
            if (lblnumarasi.Text != "")
            {
                komutunikaydet.ExecuteNonQuery();
                MessageBox.Show("Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

            else
            {
                MessageBox.Show("Lütfen tablodan öğrenci seçiniz.");
            }



            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmizinreport frmizinreport = new frmizinreport();
            frmizinreport.rol = "UNIFORMA";
            frmizinreport.Show();
        }
    }
}
