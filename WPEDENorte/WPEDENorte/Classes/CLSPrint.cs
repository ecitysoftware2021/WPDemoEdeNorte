using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;

namespace WPEDENorte.Classes
{
    public class CLSPrint
    {
        public string Contrato { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
        public string ValorIngresado { get; set; }
        public string ValorDevuelto { get; set; }
        public string Valor { get; set; }

        private SolidBrush sb;
        private Font fTitles;
        private Font fGIBTitles;
        private Font fContent;

        public CLSPrint()
        {
         sb = new SolidBrush(Color.Black);
         fTitles = new Font("Arial", 8, FontStyle.Bold);
         fGIBTitles = new Font("Arial", 12, FontStyle.Bold);
         fContent = new Font("Arial", 8, FontStyle.Regular);

        }

        #region "Métodos"
        public void ImprimirComprobante()
        {
            try
            {
                PrintController printcc = new StandardPrintController();
                PrintDocument pd = new PrintDocument();
                pd.PrintController = printcc;
                PaperSize ps = new PaperSize("Recibo Pago", 475, 470);
                pd.PrintPage += new PrintPageEventHandler(PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            try
             {
                Graphics g = e.Graphics;
                int y = 0;
                int sum = 30;
                int x = 150;

                string RutaIMG = GetConfiguration("LogoComprobante");
                g.DrawImage(Image.FromFile(RutaIMG), y += sum + 40, 0);

                g.DrawString("COMPROBANTE DE VENTA", fGIBTitles, sb, 25, y += sum);
                g.DrawString("Nit xxx.xxx.xxx.x", fContent, sb, 95, y += sum);

                g.DrawString("Contrato:", fTitles, sb, 10, y += sum);
                g.DrawString(Contrato, fContent, sb, x, y);

                g.DrawString("Fecha:", fTitles, sb, 10, y += sum);
                g.DrawString(Fecha, fContent, sb, x, y);

                g.DrawString("Hora:", fTitles, sb, 10, y += sum);
                g.DrawString(Hora, fContent, sb, x, y);

                g.DrawString("Estado:", fTitles, sb, 10, y += sum);
                g.DrawString(Estado, fContent, sb, x, y);

                g.DrawString("Valor:", fTitles, sb, 10, y += sum);
                g.DrawString(Valor, fContent, sb, x, y);

                g.DrawString("========================================", fContent, sb, 10, y += sum);

                g.DrawString("Recuerde siempre esperar la tirilla de soporte de su", fContent, sb, 10, y += sum);
                g.DrawString("pago, es el único documento que lo respalda.", fContent, sb, 10, y += 20);

                g.DrawString("E-city Software", fContent, sb, 100, y += sum);

            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        public static string GetConfiguration(string key)
        {
            try
            {
                AppSettingsReader reader = new AppSettingsReader();
                return reader.GetValue(key, typeof(String)).ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
