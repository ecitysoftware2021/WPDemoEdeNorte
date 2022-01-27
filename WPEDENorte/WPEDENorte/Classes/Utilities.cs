using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        #region "References"
        public static string Contrato { get; set; }
        public static string TOKEN { get; set; }
        public static int Session { get; set; }
        public static int CorrespondentId = 2;
        public static decimal PayVal { get; set; }
        public static decimal DispenserVal { get; set; }
        public static decimal EnterTotal { get; set; }

        public static decimal valortotal { get; set; }
        public static ControlPeripherals control;
        #endregion

        public Utilities(int i)
        {
            try
            {
                control = new ControlPeripherals();
                control.StopAceptance();
            }
            catch (Exception ex)
            {
            }
        }

        public Utilities()
        {

        }

        public static void SaveLogDispenser(LogDispenser log)
        {
            try
            {
                LogService logService = new LogService
                {
                    NamePath = "C:\\LogDispenser",
                    FileName = string.Concat("Log", DateTime.Now.ToString("yyyyMMdd"), ".json")
                };

                logService.CreateLogs(log);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Se usa para ocultar o mostrar la modal de carga
        /// </summary>
        /// <param name="window">objeto de la clase FrmLoading  </param>
        /// <param name="state">para saber si se oculta o se muestra true:muestra, false: oculta</param>
        public static void Loading(Window window, bool state, Window w)
        {
            try
            {
                if (state)
                {
                    window.Show();
                    w.IsEnabled = false;
                }
                else
                {
                    window.Hide();
                    w.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static decimal RoundValue(decimal valor)
        {
            decimal RoundTo = 100;
            decimal Amount = valor;
            decimal ExcessAmount = Amount % RoundTo;
            decimal a = 0;
            if (ExcessAmount < (RoundTo / 2))
            {
                Amount -= ExcessAmount;
                Amount = Amount + RoundTo;
                a = Amount - RoundTo;
            }
            else
            {
                Amount += (RoundTo - ExcessAmount);
                a = Amount - RoundTo;
            }

            valor = a;

            return valor;
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
                CLSPrint objPrint = new CLSPrint();

                objPrint.Contrato = Contrato;
                objPrint.Estado = "Aprobado";
                objPrint.Fecha = DateTime.Now.ToString("yyyy-MM-dd");
                objPrint.Hora = DateTime.Now.ToString("hh:mm:ss");
                objPrint.Valor = String.Format("{0:C0}", PayVal);
                objPrint.Valortotal = String.Format("{0:C0}", valortotal);
                
                //objPrint.ValorIngresado = String.Format("{0:C0}", 2000);
                //objPrint.ValorDevuelto = String.Format("{0:C0}", 2000);

                objPrint.ImprimirComprobante();
            }
            catch (Exception ex)
            {
            }
        }

        public List<Facturas> GetTypesConsult(string reference)
        {
            try
            {
                using (var conexion = new EDENorteBDEntities())
                {
                    List<Facturas> facturasCliente = new List<Facturas>();


                    //8142014144
                    string consultaFacturasCliente = "SELECT * FROM Tbl_Facturas WHERE Ref_Pago = '" + reference + "'";

                    var types = conexion.Tbl_Facturas.SqlQuery(consultaFacturasCliente).ToList();
                    //var types = conexion.Tbl_Facturas.ToList();

                    if (types.Count() > 0)
                    {
                        foreach (var type in types)
                        {
                            if (reference == type.Ref_Pago)
                            {
                                Facturas factura = new Facturas();

                                factura.Direccion_Suministro = type.Direccion_Suministro;
                                factura.Factura = type.Factura;
                                factura.Fecha_Emision = type.Fecha_Emision;
                                factura.Pague_Antes_De = type.Pague_Antes_De;
                                factura.Ref_Pago = type.Ref_Pago;
                                factura.RNC_Cliente = type.RNC_Cliente;
                                factura.Titular_De_Pago = type.Titular_De_Pago;
                                factura.Valida_Hasta = type.Valida_Hasta;
                                factura.Total_Pagar = type.Total_Pagar;
                                factura.Contrato = type.Ref_Pago;

                                facturasCliente.Add(factura);
                                //return facturas;
                            }
                        }

                        return facturasCliente;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void OpenKeyboard(bool keyBoard_Numeric, object sender, object thisView, int x = 0, int y = 0)
        {
            try
            {
                WPKeyboard.Keyboard.InitKeyboard(new WPKeyboard.Keyboard.DataKey
                {
                    control = sender,
                    userControl = thisView is System.Windows.Controls.UserControl ? thisView as System.Windows.Controls.UserControl : null,
                    eType = (keyBoard_Numeric == true) ? WPKeyboard.Keyboard.EType.Numeric : WPKeyboard.Keyboard.EType.Standar,
                    window = thisView is Window ? thisView as Window : null,
                    X = x,
                    Y = y,
                });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
