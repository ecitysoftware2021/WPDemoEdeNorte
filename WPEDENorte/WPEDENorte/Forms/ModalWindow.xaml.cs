using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPEDENorte.Forms
{
    /// <summary>
    /// Lógica de interacción para ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        private string mensaje;
        private bool giff;

        #region "Constructor
        public ModalWindow(string mensaje, bool gif)
        {
            InitializeComponent();
            this.mensaje = mensaje;
            this.giff = gif;
        }
        #endregion

    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(mensaje) && giff == true)
            {
                LblMessage.Text = "Se está procesando su solicitud, por favor espere un momento";
                Gif.Visibility = Visibility.Visible;
                btnAceptar.Visibility = Visibility.Hidden;
            }
            else
            {
                LblMessage.Text = mensaje;
            }
        }

        private void btnAceptar_TouchDown(object sender, TouchEventArgs e)
        {
            DialogResult = true;
        }
    }
}
