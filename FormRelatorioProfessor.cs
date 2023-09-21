using MySql.Data.MySqlClient;
using ReaLTaiizor.Forms;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto4
{
    public partial class FormRelatorioProfessor : MaterialForm
    {
        string cs = @"server=localhost;uid=root;pwd=;database=academico";
        public FormRelatorioProfessor()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormRelatorioAluno_Load(object sender, EventArgs e)
        {

        }
        private void CarregaImpressoras()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboImpressora.Items.Add(printer);
            }
        }

        private void MontaRelatorio()
        {
            var con = new MySqlConnection(cs);
            con.Open();
            var sql = "SELECT * FROM professor WHERE 1 = 1";

            if (cboTitulacao.Text != "")
            {
                sql += " and titulacao = @titulacao";
            }

            if (txtAreaFormacao.Text != "")
            {
                sql += " and area_formacao = @area_formacao";
            }
            var sqlAd = new MySqlDataAdapter();
            sqlAd.SelectCommand = new MySqlCommand(sql, con);

            if (cboTitulacao.Text != "")
            {
                sqlAd.SelectCommand.Parameters.AddWithValue("@titulacao", cboTitulacao.Text);
            }

            if (txtAreaFormacao.Text != "")
            {
                sqlAd.SelectCommand.Parameters.AddWithValue("@area_formacao", txtAreaFormacao.Text);
            }
            var dt = new DataTable();
            sqlAd.Fill(dt);
            con.Close();

            PdfDocument doc = new PdfDocument();
            PdfSection sec = doc.Sections.Add();
            sec.PageSettings.Width = PdfPageSize.A4.Width;
            PdfPageBase page = sec.Pages.Add();
            int y = 20;
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);

            page.Canvas.DrawString("RelatorioProfessor.pdf", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);

            PdfTable table = new PdfTable();
            table.Style.CellPadding = 2;
            table.Style.BorderPen = new PdfPen(brush1, 0.75f);
            table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            table.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
            table.Style.HeaderRowCount = 1;
            table.Style.HeaderStyle.BackgroundBrush = PdfBrushes.CadetBlue;
            table.Style.ShowHeader = true;
            table.DataSource = dt;
            foreach (PdfColumn col in table.Columns)
            {
                col.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            }
            table.Draw(page, new Point(0, y + 30));


            doc.SaveToFile("RelatorioProfessor.pdf");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MontaRelatorio();

            string pdfFilePath = @"C:\Users\aluno\source\repos\duows\projeto5\bin\Debug\net6.0-windows\RelatorioProfessor.pdf";
            string imp = cboImpressora.Text;
            if (String.IsNullOrEmpty(imp))
            {
                return;
            }

            PdfDocument doc = new PdfDocument();

            doc.LoadFromFile(pdfFilePath);
            doc.PrintSettings.PrinterName = imp;
            doc.Print();

        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {

            MontaRelatorio();
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"RelatorioProfessor.pdf")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private void FormRelatorioProfessor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Principal.isOpenRelatorioProfessor = false;
        }
    }
}
