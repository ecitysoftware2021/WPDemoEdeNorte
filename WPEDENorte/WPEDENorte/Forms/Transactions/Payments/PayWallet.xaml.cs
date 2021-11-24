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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPEDENorte.Classes;
using WPEDENorte.Windows.Alerts;

namespace WPEDENorte.Forms.Transactions.Payments
{
    /// <summary>
    /// Interaction logic for PayWallet.xaml
    /// </summary>
    public partial class PayWallet : Window
    {
       
        Facturas facturas = new Facturas();
        private ObservableCollection<Background> fondo;
        private int num;

        public PayWallet(Facturas datos)
        {
            facturas = datos;
            InitializeComponent();
            fondo = new ObservableCollection<Background>();
            this.DataContext = fondo;
            num = 1;
            ChangeBackground();

            try
            {
                datos.Valor = String.Format("RD {0:C0}", Convert.ToDecimal(datos.Valor));

                this.GridData.DataContext = datos;
            }
            catch (Exception ex)
            {
            }
        }

        private void Border_TouchDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txt_valor_TouchDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txt_valorConsignacion_TouchDown(object sender, MouseButtonEventArgs e)
        {
            ModalAmountW modalAmountW = new ModalAmountW(facturas);
            modalAmountW.Show();
        }

        private void btnContinue_TouchDown(object sender, MouseButtonEventArgs e)
        {
            //decimal valor = Convert.ToDecimal(txtVlrpago.Text.Trim().Replace("RD $ ", "").Replace(",", "").Replace(".", ""));
            //PayWindow pay = new PayWindow(valor);

            //pay.Show();
            //this.Close();
        }

        private void btnMenu_TouchDown(object sender, MouseButtonEventArgs e)
        {

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

        private void btnFacturaMes_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void btnValorVencido_TouchDown(object sender, TouchEventArgs e)
        {

        }
    }
}
