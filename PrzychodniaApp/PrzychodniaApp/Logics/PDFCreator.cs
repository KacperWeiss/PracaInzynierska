using PrzychodniaApp.UserControlers.DataRepresantations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.IO.Util;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace PrzychodniaApp.Logics
{
    public static class PDFCreator
    {
        public static void MakePrescription(PrescriptionPDFRequiredData prescriptionData)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FileInfo file = new FileInfo(path + "/Recepta_PrzychodniaApp.pdf");
            file.Directory.Create();

            PdfWriter writer = new PdfWriter(path + "/Recepta_PrzychodniaApp.pdf");
            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf, new PageSize(280, 594));
            document.SetMargins(2, 2, 2, 2);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Table table1 = new Table(1).UseAllAvailableWidth();
            table1.SetFontSize(8);
            table1.AddCell(new Cell().Add(new Paragraph("Recepta\n\n\n\n\n\n\nSwiadczeniodawca")));

            Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 3, 1 })).UseAllAvailableWidth();
            table2.SetFontSize(10);
            table2.AddCell(new Cell().Add(new Paragraph("Pacjent\n" + prescriptionData.PacientName + "\n\n\n\n\nPESEL  " + prescriptionData.PacientPESEL)));
            table2.AddCell(new Cell().Add(new Paragraph("Oddzial NFZ\n\n\n\nUprawnienia\nDodatkowe\n X")));
            table2.AddCell(new Cell().Add(new Paragraph("Rp")));
            table2.AddCell(new Cell().Add(new Paragraph("Odplatnosc")));
            table2.AddCell(new Cell().Add(new Paragraph(prescriptionData.Medicines)));
            table2.AddCell(new Cell().Add(new Paragraph(prescriptionData.MedicinesPayment)));

            Table table3 = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
            table3.SetFontSize(10);
            table3.AddCell(new Cell().Add(new Paragraph("Data wystawienia\n" + DateTime.Now.ToShortDateString() + "\nData realizacji ,,do dnia''\n" + DateTime.Now.ToShortDateString())));
            table3.AddCell(new Cell().Add(new Paragraph("Dane i podpis lekarza\n" + prescriptionData.MedicalWorkerName + "\n\n")));

            document.Add(table1);
            document.Add(table2);
            document.Add(table3);
            document.Close();
        }

        public static void MakeRefferal(RefferalPDFRequiredData refferalData)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FileInfo file = new FileInfo(path + "/Skierowanie_PrzychodniaApp.pdf");
            file.Directory.Create();

            PdfWriter writer = new PdfWriter(path + "/Skierowanie_PrzychodniaApp.pdf");
            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf, PageSize.A4);
            document.SetMargins(2, 2, 2, 2);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            document.Add(new Paragraph("SKIEROWANIE DO PORADNI SPECJALISTYCZNEJ\n").SetFontSize(11)).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            document.Add(new Paragraph("................................................\n").SetFontSize(9)).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            document.Add(new Paragraph("(nazwa poradni)\n\n\n\n").SetFontSize(7)).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            document.Add(new Paragraph("Prosze o: porade specjalistyczna,   objecie leczeniem specjalistycznym*\n" +
                "Panią (Pana) ..........." + refferalData.PacientName +"..................... , lat " + refferalData.PacientAge + "\n" +
                "Adres: .....................................................................\n" +
                "PESEL: " + refferalData.PacientPESEL + ", telefon: ...............................\n" +
                "Rozpoznanie: " + refferalData.Sickness + "\n" +
                "Kod(ICD10) ...........................................................................").SetFontSize(10)).SetHorizontalAlignment(HorizontalAlignment.LEFT);


            document.Add(new Paragraph("Podpis\n...............").SetFontSize(9).SetVerticalAlignment(VerticalAlignment.BOTTOM)).SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            document.Close();
        }
    }
}
