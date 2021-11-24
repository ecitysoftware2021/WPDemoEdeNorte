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
using WPEDENorte.Forms;

namespace WPEDENorte.Windows.Alerts
{
    /// <summary>
    /// Interaction logic for ModalAmountW.xaml
    /// </summary>
    public partial class ModalAmountW : Window
    {
        private ObservableCollection<Background> fondo;
        private int num;
        public ModalAmountW(Facturas facturas)
        {

            InitializeComponent();
            fondo = new ObservableCollection<Background>();
            this.DataContext = fondo;
            num = 1;
            ChangeBackground();

            try
            {
            //    txtval.Text = facturas.Valor;
                facturas.Valor = String.Format("RD {0:C0}", Convert.ToDecimal(facturas.Valor));
                this.GridData.DataContext = facturas;
            }
            catch (Exception ex)
            {
            }
        }

        private void BtnCancel_TouchDown(object sender, MouseButtonEventArgs e)
        {
            ModalAmountW modalAmountW = new ModalAmountW(null);
            modalAmountW.Close();
        }

        private void TxtVal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAceptar_TouchDown_1(object sender, TouchEventArgs e)
        {
            decimal valor = Convert.ToDecimal(txtval.Text.Trim().Replace("RD $ ", "").Replace(",", "").Replace(".", ""));
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
    }
}
