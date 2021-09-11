using System;
using System.Windows;
using System.Windows.Input;
using WPEDENorte.Classes;

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

        private void BtnPayFactura_TouchDown(object sender, TouchEventArgs e)
        {
            SearchWindow search = new SearchWindow();
            search.Show();
            this.Close();
        }
    }
}
