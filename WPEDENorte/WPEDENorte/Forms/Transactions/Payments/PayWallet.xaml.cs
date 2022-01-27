using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        List<Facturas> facturas = new List<Facturas>();
        private ObservableCollection<Background> fondo;
        private int num;
        public Utilities utilities;
        
       

        public Decimal totalFacturasVencidas;
        public Decimal totalFacturasActivas;
        public Decimal totalFacturas;
        private string ValorTotal;
        public Decimal totalAbono;

        public PayWallet(List<Facturas> listaTotalFacturas)
        {
            this.facturas = listaTotalFacturas;

            InitializeComponent();
            fondo = new ObservableCollection<Background>();
            this.DataContext = fondo;
            num = 1;
            ChangeBackground();
            
            this.Opacity = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            calcularTotalFacturasVencidas(facturas);
            calcularTotalFacturasActivas(facturas);
            calcularTotalFacturas(facturas);
            calcularAbonoFacturas(facturas);
            utilities = new Utilities();


        }

        private void btnMenu_TouchDown(object sender, MouseButtonEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
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

        private void btnFacturaActiva_TouchDown(object sender, TouchEventArgs e)
        {
            this.Opacity = 0.2;

            ModalAmountW modalAmountW = new ModalAmountW(totalFacturasActivas.ToString(), "Activas");
            modalAmountW.ShowDialog();
            if (modalAmountW.DialogResult == true)
            {
                this.Opacity = 1;              
            }
        }

        private void btnFacturaVencida_TouchDown(object sender, TouchEventArgs e)
        {
            this.Opacity = 0.2;

            ModalAmountW modalAmountW = new ModalAmountW(totalFacturasVencidas.ToString(), "Vencidas");
            modalAmountW.ShowDialog();
            if (modalAmountW.DialogResult == true)
            {
                this.Opacity = 1;
            }
        }

        private void btnFacturasTotal_TouchDown(object sender, TouchEventArgs e)
        {
            this.Opacity = 0.2;

            ModalAmountW modalAmountW = new ModalAmountW(totalFacturas.ToString(), "Total");
            modalAmountW.ShowDialog();
            if (modalAmountW.DialogResult == true)
            {
                this.Opacity = 1;
            }
        }

        private void btnValorAbono_TouchDown(object sender,TouchEventArgs e)
        {
            this.Opacity = 0.2;
            ModalAmountW modalAmountW = new ModalAmountW(totalAbono.ToString(), "Activas");
           modalAmountW.ShowDialog();
            if(modalAmountW.DialogResult==true)
            {
                this.Opacity = 1;
            }
        }

        private void calcularTotalFacturasVencidas(List<Facturas> listaTotalFacturas)
        {
            totalFacturasVencidas = 0;

            foreach (Facturas Factura in listaTotalFacturas)
            {
                if (EsFechaMayorQueHoy(Factura.Pague_Antes_De))
                {
                    totalFacturasVencidas += Convert.ToDecimal(Factura.Total_Pagar);
                }
            }
            
            txt_valorVencido.Text = totalFacturasVencidas.ToString();
        }

        

        private void calcularTotalFacturasActivas(List<Facturas> listaTotalFacturas)
        {
            totalFacturasActivas = 0;

            foreach (Facturas Factura in listaTotalFacturas)
            {
                if (!EsFechaMayorQueHoy(Factura.Pague_Antes_De))
                {
                    totalFacturasActivas += Convert.ToDecimal(Factura.Total_Pagar);
                }
            }

           txt_facturaMes.Text = totalFacturasActivas.ToString();

        }

        private void calcularAbonoFacturas(List<Facturas> listaTotalFacturas)
        {
            totalAbono = 0;
           

            foreach (Facturas Factura in listaTotalFacturas)
            {
                
                    totalAbono += Convert.ToDecimal(Factura.Total_Pagar);
                Utilities.valortotal = Convert.ToDecimal(totalAbono);
            }

            txt_Abonopago.Text = totalAbono.ToString();
            
        } 

        private void calcularTotalFacturas(List<Facturas> listaTotalFacturas)
        {
            totalFacturas = 0;

            foreach (Facturas Factura in listaTotalFacturas)
            {
                totalFacturas += Convert.ToDecimal(Factura.Total_Pagar);
                Utilities.valortotal = Convert.ToDecimal(totalFacturas);
            }

            txt_facturasTotal.Text = totalFacturas.ToString();
           
           
        }

        private void SumaCuentas()
        {
            
            calcularTotalFacturasActivas(facturas);
            calcularTotalFacturasVencidas(facturas);
            ;

        }


        private Boolean EsFechaMayorQueHoy(string FechaString)
        {
            string dateFormat = "dd/MM/yyyy";

            var Fecha = DateTime.ParseExact(FechaString, dateFormat, CultureInfo.InvariantCulture);

            if (Fecha < DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
