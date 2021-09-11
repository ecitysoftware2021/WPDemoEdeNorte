using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPEDENorte.Classes;

namespace WPEDENorte.Forms
{
    /// <summary>
    /// Lógica de interacción para SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        #region "References"
        private Utilities utilities;
        private string num;
        #endregion

        #region "Constructor"
        public SearchWindow()
        {
            InitializeComponent();
            utilities = new Utilities();
        }
        #endregion

        #region "MétodoValidarCampos"
        /// <summary>
        /// Método que me valida que ingrese un número
        /// </summary>
        /// <returns></returns>
        /// 
        private bool ValidarCampo()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtNumero.Text))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region "ButtonSearch"
       
        /// <summary>
        /// Método que me redirecciona a detalles si la consulta es exitosa
        /// </summary>
        /// <param name="tipConsult"></param>
        /// <param name="reference"></param>
        private void Consult()
        {
            try
            {
                Facturas types = new Facturas 
                {
                    Factura = "1414561",
                    Valor = "2000",
                    Contrato = "54211",
                    Fecha_Emision = "2021/07/28",
                    Ref_Pago = "4524126",
                    Pague_Antes_De = "2021/08/30",
                    Direccion_Suministro = "Calle 31 # 45-11"

                };

                if (types == null)
                {
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        gif.Visibility = Visibility.Hidden;
                        btnConsultar.Visibility = Visibility.Visible;
                        GrvPpl.Opacity = 0.2;
                        ModalWindow modal = new ModalWindow("No se encontrarón resultados de la factura",false);
                        modal.ShowDialog();
                        GrvPpl.Opacity = 1;
                    });
                    GC.Collect();
                }
                else
                {
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        Utilities.Contrato = num;
                        DetailWindow detail = new DetailWindow(types);
                        detail.Show();
                        this.Close();
                    });
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

     

        private void BtnBack_TouchDown(object sender, TouchEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();
        }


        private void BtnExit_TouchDown(object sender, TouchEventArgs e)
        {
            Utilities.GoToInicial();
        }

        private void btnConsultar_TouchDown(object sender, TouchEventArgs e)
        {
            try
            {
                if (!ValidarCampo())
                {
                    GrvPpl.Opacity = 0.2;
                    ModalWindow modal = new ModalWindow("Debe de ingresar una factura", false);
                    modal.ShowDialog();
                    GrvPpl.Opacity = 1;
                }
                else
                {
                    gif.Visibility = Visibility.Visible;
                    btnConsultar.Visibility = Visibility.Hidden;

                    num = TxtNumero.Text;

                    Task.Run(() =>
                    {
                        Consult();
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ImgDeleteAll_TouchDown(object sender, TouchEventArgs e)
        {
            try
            {
                TxtNumero.Text = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }

        private void Img0_TouchDown(object sender, TouchEventArgs e)
        {
            try
            {
                Image image = (Image)sender;
                string Tag = image.Tag.ToString();
                TxtNumero.Text += Tag;
            }
            catch (Exception ex)
            {
            }
        }

        private void ImgDelete_TouchDown(object sender, TouchEventArgs e)
        {
            try
            {
                string val = TxtNumero.Text;

                if (val.Length > 0)
                {
                    TxtNumero.Text = val.Remove(val.Length - 1);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
