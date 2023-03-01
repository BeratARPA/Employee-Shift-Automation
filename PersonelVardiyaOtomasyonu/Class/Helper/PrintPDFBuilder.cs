using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu.Class.Helper
{
    public static class PrintPDFBuilder
    {
        public static void Print(DataTable shifts, DateTime startDate, DateTime endDate)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
            saveFileDialog.FileName = "document.pdf";

            //Yukarıda FileDialog nesnesi oluşturup alt satırda dialog penceresini açıp gerekli bilgileri alıyoruz.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL);

                //DataTabledaki sütün sayısı kadar sütüna sahip bir tablo nesnesi oluşturuyoruz
                PdfPTable pdfPTable = new PdfPTable(shifts.Columns.Count);

                //Hücre nesnesi oluşturarak istediğimiz veriyi yazıyor ve özelliklerini veriyoruz
                PdfPCell HeaderCell = new PdfPCell(new Phrase("MANISA CELAL BAYAR ÜNIVERSITESI KIRKAGAÇ MESLEK YÜKSEKOKULU", font));
                HeaderCell.Colspan = shifts.Columns.Count;
                HeaderCell.HorizontalAlignment = 1;
                HeaderCell.Padding = 10;
                pdfPTable.AddCell(HeaderCell);

                //Hücre nesnesi oluşturarak istediğimiz veriyi yazıyor ve özelliklerini veriyoruz
                PdfPCell InformationCell = new PdfPCell(new Phrase(startDate.ToShortDateString() + " - " + endDate.ToShortDateString() + " NÖBET ÇIZELGESI", font));
                InformationCell.Colspan = shifts.Columns.Count;
                InformationCell.HorizontalAlignment = 1;
                InformationCell.Padding = 10;
                pdfPTable.AddCell(InformationCell);

                //Verileri DataTable şeklinde alıp itextsharp ile pdfpcelle aktarıyoruz ve özelliklerini verip ekrana basıyoruz.
                foreach (DataColumn dataColumn in shifts.Columns)
                {
                    PdfPCell pdfPCell = new PdfPCell(new Phrase(dataColumn.ColumnName, font));
                    pdfPCell.HorizontalAlignment = 1;
                    pdfPCell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#CED4DA"));
                    pdfPTable.AddCell(pdfPCell);
                }

                //Satır ve sütünları tek tek gezerek DataTabledaki bütün verileri oluşturduğumuz table nesnesine AddCell metodu ile ekliyoruz.
                for (int i = 0; i < shifts.Rows.Count; i++)
                {
                    for (int j = 0; j < shifts.Columns.Count; j++)
                    {
                        pdfPTable.AddCell(new Phrase(shifts.Rows[i][j].ToString(), font));
                    }
                }

                //Açtığımız belgeleri ve streamleri kapatıp kayıt işlemino sonlandırıyoruz.
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 0, 0, 10, 10);
                    PdfWriter.GetInstance(document, fileStream);
                    document.Open();
                    document.Add(pdfPTable);
                    document.Close();
                    fileStream.Close();
                }
            }
        }
    }
}
