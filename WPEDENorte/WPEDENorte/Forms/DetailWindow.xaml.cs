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

                this.Border.DataContext = datos;
            }
            catch (Exception ex)
            {
            }
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

        private bool state = false;
        private void btnDetalle_TouchDown(object sender, TouchEventArgs e)
        {
            state = !state;

            if (state)
            {
                GRV1.Visibility = Visibility.Visible;
                GRV2.Visibility = Visibility.Visible;
                GRV3.Visibility = Visibility.Visible;
                GRV4.Visibility = Visibility.Visible;
                GRV5.Visibility = Visibility.Visible;
                GRV6.Visibility = Visibility.Visible;
                GRV7.Visibility = Visibility.Visible;
                GRV8.Visibility = Visibility.Visible;
            }
            else
            {
                GRV1.Visibility = Visibility.Hidden;
                GRV2.Visibility = Visibility.Hidden;
                GRV3.Visibility = Visibility.Hidden;
                GRV4.Visibility = Visibility.Hidden;
                GRV5.Visibility = Visibility.Hidden;
                GRV6.Visibility = Visibility.Hidden;
                GRV7.Visibility = Visibility.Hidden;
                GRV8.Visibility = Visibility.Hidden;
            }
        }

        private void BtnBack_TouchDown(object sender, TouchEventArgs e)
        {
            SearchWindow search = new SearchWindow();
            search.Show();
            this.Close();
        }

        private void BtnExit_TouchDown(object sender, TouchEventArgs e)
        {
            Utilities.GoToInicial();
        }

        private void BtnPagar_TouchDown(object sender, TouchEventArgs e)
        {
            //decimal valor = Convert.ToDecimal(txtValor.Text.Trim().Replace("RD $ ", "").Replace(",", "").Replace(".", ""));
            decimal valor = 2000;

            PayWindow pay = new PayWindow(valor);
            pay.Show();
            this.Close();
        }
    }

    public class Background
    {
       public string background { get; set; }
    }
}
