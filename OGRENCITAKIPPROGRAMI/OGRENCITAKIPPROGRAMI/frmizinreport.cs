using Microsoft.Reporting.WinForms;
using OGRENCITAKIPPROGRAMI.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OGRENCITAKIPPROGRAMI
{
    public partial class frmizinreport : Form
    {
        public frmizinreport()
        {
            InitializeComponent();
        }
        public string rol;
        baglantisinif con = new baglantisinif();
        DataTable1TableAdapter ds = new DataTable1TableAdapter();
        private void frmizinreport_Load(object sender, EventArgs e)
        {
            if (rol == "IZIN")
            {
                using (OleDbConnection conn = new OleDbConnection(con.baglan))
                {
                    conn.Open();

                    OleDbCommand rapor = new OleDbCommand(
                        @"SELECT ID, OGRADSOYAD , SINIFAD, IZINOGRNUMARA ,
                     IZINOGRIZINTARIH , IZINOGRIZINSAAT ,
                     IZINOGRIZINMAZERET , IZINOGRIZINALANKISI 
              FROM (TBLIZIN
              INNER JOIN TBLOGRENCILER
                ON TBLOGRENCILER.OGRID = TBLIZIN.IZINOGRADSOYAD)
              INNER JOIN TBLSINIF
                ON TBLSINIF.SINIFID = TBLIZIN.IZINOGRSINIF",
                        conn);

                    OleDbDataAdapter da = new OleDbDataAdapter(rapor);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds =
                        new ReportDataSource("DataSet1", dt); // RDLC ile birebir olmalı

                    reportViewer1.LocalReport.ReportPath =
                        Application.StartupPath + @"\Report1.rdlc";

                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.RefreshReport();



                }
            }
            else if (rol == "TELEFON")
            {
                using (OleDbConnection conn = new OleDbConnection(con.baglan))
                {
                    conn.Open();

                    OleDbCommand rapor2 = new OleDbCommand(
                        @"SELECT TELID, OGRADSOYAD,OGRNUMARATELEFON, SINIFAD,OGRTARIHTELEFON,OGRMAZERETTELEFON FROM (TBLTELEFONKAYIT INNER JOIN TBLOGRENCILER
ON TBLOGRENCILER.OGRID=TBLTELEFONKAYIT.OGRADSOYADTELEFON) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLTELEFONKAYIT.OGRSINIFTELEFON",
                        conn);

                    OleDbDataAdapter da2 = new OleDbDataAdapter(rapor2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds2 =  new ReportDataSource("DataSet1", dt2); // RDLC ile birebir olmalı
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report2.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(rds2);
                    reportViewer1.RefreshReport();



                }
            }

            else if (rol == "UNIFORMA")
            {
                using (OleDbConnection conn = new OleDbConnection(con.baglan))
                {
                    conn.Open();

                    OleDbCommand rapor3 = new OleDbCommand(
                        @"select UNIID  , OGRADSOYAD, OGRNUMARAUNI  , SINIFAD , OGRTARIHUNI , OGRMAZERETUNI  FROM (TBLUNIFORMA INNER JOIN TBLOGRENCILER ON TBLOGRENCILER.OGRID=TBLUNIFORMA.OGRADSOYADUNI) INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLUNIFORMA.OGRSINIFUNI",
                        conn);

                    OleDbDataAdapter da3 = new OleDbDataAdapter(rapor3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds3 = new ReportDataSource("DataSet1", dt3); // RDLC ile birebir olmalı
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report3.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(rds3);
                    reportViewer1.RefreshReport();



                }
            }

            else if(rol == "OGRENCI")
            {
                using (OleDbConnection conn = new OleDbConnection(con.baglan))
                {
                    conn.Open();

                    OleDbCommand rapor4 = new OleDbCommand(
                        @"SELECT  OGRID, OGRADSOYAD ,SINIFAD , OGRNUMARA,OGRBABATELEFON , OGRANNETELEFON, OGRFOTOGRAFYOL from TBLOGRENCILER INNER JOIN TBLSINIF ON TBLSINIF.SINIFID=TBLOGRENCILER.OGRSINIF",
                        conn);

                    OleDbDataAdapter da4 = new OleDbDataAdapter(rapor4);
                    DataTable dt4 = new DataTable();
                    da4.Fill(dt4);

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds4 = new ReportDataSource("DataSet1", dt4); // RDLC ile birebir olmalı
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report4.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(rds4);
                    reportViewer1.RefreshReport();



                }
            }
           
        }
    }
}
