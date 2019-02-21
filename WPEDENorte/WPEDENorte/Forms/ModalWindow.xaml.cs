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
        #region "Constructor
        public ModalWindow(string mensaje)
        {
            InitializeComponent();
            LblMessage.Text = mensaje;
        }
        #endregion

        #region "Button"
        private void Image_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
