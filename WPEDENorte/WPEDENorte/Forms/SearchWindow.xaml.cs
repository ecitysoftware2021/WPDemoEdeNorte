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

        #region "HeadersButtons"
        /// <summary>
        /// Botón que me redirecciona a la ventana anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Botón que me redirecciona a la ventana inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                Utilities.GoToInicial();
            }
            catch (Exception ex)
            {
            }
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
        private void BtnConsultar_StylusDown_1(object sender, StylusDownEventArgs e)
        {
            try
            {
                if (!ValidarCampo())
                {
                    GrvPpl.Opacity = 0.2;
                    ModalWindow modal = new ModalWindow("Debe de ingresar una factura");
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

        /// <summary>
        /// Método que me redirecciona a detalles si la consulta es exitosa
        /// </summary>
        /// <param name="tipConsult"></param>
        /// <param name="reference"></param>
        private void Consult()
        {
            try
            {
                var types = utilities.GetTypesConsult(num);

                if (types == null)
                {
                    GrvPpl.Opacity = 0.2;
                    ModalWindow modal = new ModalWindow("No se encontrarón resultados de la factura");
                    modal.ShowDialog();
                    GrvPpl.Opacity = 1;
                }
                else
                {
                    DetailWindow detail = new DetailWindow(types);
                    detail.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region "Teclado"
        /// <summary>
        /// Botón que me elimina todo el texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgDeleteAll_StylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                TxtNumero.Text = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Botón coge el tag y me lo pinta el valor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keyboard_StylusDown(object sender, StylusDownEventArgs e)
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

        /// <summary>
        /// Botón que me elimina el ultimo regristro ingresado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgDelete_StylusDown(object sender, StylusDownEventArgs e)
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
        #endregion
    }
}
