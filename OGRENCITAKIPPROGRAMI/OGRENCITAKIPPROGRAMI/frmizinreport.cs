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
        baglantisinif con = new baglantisinif();
        DataTable1TableAdapter ds = new DataTable1TableAdapter();
        private void frmizinreport_Load(object sender, EventArgs e)
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
    }
}
