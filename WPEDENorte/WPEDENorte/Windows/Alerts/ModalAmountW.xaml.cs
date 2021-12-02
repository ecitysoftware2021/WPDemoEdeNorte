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
        private List<Facturas> facturas;
        private Decimal totalAPagar;
        private string tipoFacturasAPagar;

        public ModalAmountW(string totalAPagar, string tipoFacturasAPagar)
        {
            InitializeComponent();
            fondo = new ObservableCollection<Background>();
            this.DataContext = fondo;
            num = 1;
            this.tipoFacturasAPagar = tipoFacturasAPagar;
            this.totalAPagar = Convert.ToDecimal(totalAPagar);

            ChangeBackground();

            try
            {
                LimitarEscrituraSegunTipoFactura(totalAPagar, tipoFacturasAPagar);

                txtval.Text = totalAPagar;

                this.GridData.DataContext = facturas;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAceptar_TouchDown_1(object sender, TouchEventArgs e)
        {
            decimal valor = Convert.ToDecimal(txtval.Text);

            if (valor > totalAPagar)
            {
                txtPagoParcialValido.Visibility = Visibility.Visible;
            }
            else
            {
                PayWindow pay = new PayWindow(valor);

                pay.Show();
                this.Close();
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

        private void BtnCancel_TouchDown_1(object sender, TouchEventArgs e)
        {
            DialogResult = true;
        }

        private void LimitarEscrituraSegunTipoFactura(string totalAPagar, string tipoFacturasAPagar)
        {
            string TxtPagoEditable = "Ingrese el valor que desea consignar";
            string TxtPagoNoEditable = "Valor a pagar";

            if(tipoFacturasAPagar == "Activas")
            {
                txtval.IsReadOnly = false;
                txtMsInformacion.Text = TxtPagoEditable;
            }
            else {
                txtval.IsReadOnly = true;
                txtMsInformacion.Text = TxtPagoNoEditable;
            }
        }

        private void txtval_TouchDown(object sender, TouchEventArgs e)
        {
            if(tipoFacturasAPagar == "Activas")
            {
                Utilities.OpenKeyboard(true, sender, this, 800, 1000);
            }
        }
    }
}
