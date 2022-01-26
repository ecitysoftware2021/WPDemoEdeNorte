using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPEDENorte.Classes;
using WPEDENorte.LocalBD;

namespace WPEDENorte.Forms
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        #region "Constructor"
        public MenuWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region "HeaderButton"
        /// <summary>
        /// Botón que me redirecciona a la ventana inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHome_PreviewStylusDown(object sender, StylusDownEventArgs e)
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

        #region "Buttons"

        #endregion

        private void BtnPayFactura_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                SearchWindow search = new SearchWindow();
                search.Show();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void Image_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            this.Opacity = 0.3;
            ModalWindow modal = new ModalWindow("En este momento no se encuentra disponible este servicio", false);
            modal.ShowDialog();
            this.Opacity = 1;
        }

     

        private void BtnPayFactura_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SearchWindow search = new SearchWindow();
                search.Show();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void  BtnHome_TouchDown(object sender, TouchEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
