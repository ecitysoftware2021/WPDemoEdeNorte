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
using WPEDENorte.Classes;

namespace WPEDENorte.Forms
{
    /// <summary>
    /// Lógica de interacción para InitialWindow.xaml
    /// </summary>
    public partial class InitialWindow : Window
    {
        #region "Referencias"
        private List<string> images;
        private ImageSleader imageSleader;
        #endregion

        #region "Constructor"
        public InitialWindow()
        {
            InitializeComponent();

            images = LoadImageFolder();

            imageSleader = new ImageSleader(images);

            this.DataContext = imageSleader.imageModel;

            imageSleader.time = 2;

            imageSleader.isRotate = true;

            init();

            //Utilities.Operation = 2;
            //utilities.ImprimirComprobante();

        }
        #endregion

        #region "Eventos"
        /// <summary>
        /// Evento que me redirecciona a la ventana del menú
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                MenuWindow Menu = new MenuWindow();
                Menu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region "Métodos"
        /// <summary>
        /// Método que me inicia la rotación de las imagenes
        /// </summary>
        private void init()
        {
            try
            {
                imageSleader.star();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Método que me carga una lista de imagenes
        /// </summary>
        /// <returns></returns>
        private List<string> LoadImageFolder()
        {
            try
            {
                return new List<string>
                {
                @"Images/Backgrounds/Publicidad-02.jpg",
                @"Images/Backgrounds/Publicidad-03.jpg"
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}
