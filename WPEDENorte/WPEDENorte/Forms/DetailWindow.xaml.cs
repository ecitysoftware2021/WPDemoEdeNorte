using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private ObservableCollection<Background> fondo;
        private int num;

        public DetailWindow(Facturas datos)
        {
            InitializeComponent();
            fondo = new ObservableCollection<Background>();
            this.DataContext = fondo;
            num = 1;
            ChangeBackground();

            try
            {
                datos.Valor = String.Format("RD {0:C0}", Convert.ToDecimal(datos.Valor));

                this.DataContext = datos;
            }
            catch (Exception ex)
            {
            }
        }

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
                SearchWindow search = new SearchWindow();
                search.Show();
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

        private void BtnPagar_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            decimal valor = Convert.ToDecimal(txtValor.Text.Trim().Replace("RD $ ","").Replace(",", "").Replace(".", ""));

            PayWindow pay = new PayWindow(valor);
            pay.Show();
            this.Close();
        }

        private void ChangeBackground()
        {
            try
            {
                if (num == 1)
                {
                    fondo.Add(new Background
                    {
                        background = @"Images/Background/f-para-modales.jpg"
                    });
                    if (fondo.Count > 1)
                    {
                        fondo.RemoveAt(0);
                    }
                }
                else if (num == 2)
                {

                    fondo.Add(new Background
                    {
                        background = @"Images/Background/f-generico.png"
                    });
                    fondo.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void BtnX_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            num = 2;
            ChangeBackground();
            BtnX.Visibility = Visibility.Hidden;
            BtnPagar.IsEnabled = true;
            BtnExit.IsEnabled = true;
            BtnBack.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnPagar.IsEnabled = false;
            BtnExit.IsEnabled = false;
            BtnBack.IsEnabled = false;
        }
    }

    public class Background
    {
       public string background { get; set; }
    }
}
