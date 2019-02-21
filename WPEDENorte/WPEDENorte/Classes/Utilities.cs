using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using WPEDENorte.Classes;
using WPEDENorte.Forms;
using WPEDENorte.LocalBD;

namespace WPEDENorte.Classes
{
    public class Utilities
    {
        public Utilities()
        {

        }

        public static string GetConfiguration(string key)
        {
            try
            {
                AppSettingsReader reader = new AppSettingsReader();
                return reader.GetValue(key, typeof(String)).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Método que me redirecciona a la ventana de inicio
        /// </summary>
        public static void GoToInicial()
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    InitialWindow main = new InitialWindow();
                    main.Show();
                    CloseWindows(main.Title);
                }));
                GC.Collect();
            }
            catch (Exception ex)
            {
                RestartApp();
            }
        }

        public static void CloseWindows(string Title)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.IsLoaded && w.Title != Title)
                {
                    w.Close();
                }
            }
        }

        /// <summary>
        /// Método usado para regresar a la pantalla principal
        /// </summary>
        public static void RestartApp()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                Process pc = new Process();
                Process pn = new Process();
                ProcessStartInfo si = new ProcessStartInfo();
                si.FileName = Path.Combine(Directory.GetCurrentDirectory(), "WPEDENorte.exe");
                pn.StartInfo = si;
                pn.Start();
                pc = Process.GetCurrentProcess();
                pc.Kill();
            }));
            GC.Collect();
        }

        public void ImprimirComprobante()
        {
            try
            {
                //CLSPrint objPrint = new CLSPrint();

                //objPrint.FECHA_FACTURA = DateTime.Now.ToString("yyyy-MM-dd");
                //objPrint.HORA_FACTURA = DateTime.Now.ToString("hh:mm:ss");
                //objPrint.VALOR = String.Format("{0:C0}", 2000);

                //objPrint.ImprimirComprobante();
            }
            catch (Exception ex)
            {
            }
        }

        public Facturas GetTypesConsult(string reference)
        {
            try
            {
                Facturas facturas = new Facturas();

                using (var conexion = new EDENorteBDEntities())
                {
                    var types = conexion.Tbl_Facturas.ToList();
                    if (types.Count() > 0)
                    {
                        foreach (var type in types)
                        {
                            if (reference == type.Factura)
                            {
                                facturas.Direccion_Suministro = type.Direccion_Suministro;
                                facturas.Factura = type.Factura;
                                facturas.Fecha_Emision = type.Fecha_Emision;
                                facturas.Pague_Antes_De = type.Pague_Antes_De;
                                facturas.Ref_Pago = type.Ref_Pago;
                                facturas.RNC_Cliente = type.RNC_Cliente;
                                facturas.Titular_De_Pago = type.Titular_De_Pago;
                                facturas.Valida_Hasta = type.Valida_Hasta;
                                facturas.Valor = Convert.ToDecimal(type.Total_Pagar);
                                facturas.Contrato = type.Contrato;

                                return facturas;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
