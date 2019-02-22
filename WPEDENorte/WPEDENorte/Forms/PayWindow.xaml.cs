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
    /// Lógica de interacción para PayWindow.xaml
    /// </summary>
    public partial class PayWindow : Window
    {
        #region References

        private PaymentViewModel PaymentViewModel;

        private ModalWindow frmLoading;

        private Utilities utilities;

        private int count;

        private bool stateUpdate;

        private bool payState;

        private string descrip;

        private decimal value;

        #endregion

        #region L oadMethods
        public PayWindow(decimal value)
        {
            InitializeComponent();

            try
            {
                this.value = value;
                Utilities.PayVal = Utilities.RoundValue(value); 
                OrganizeValues();
                frmLoading = new ModalWindow("", true);
                utilities = new Utilities();
                count = 0;
                stateUpdate = true;
                Utilities.control.StartValues();
            }
            catch (Exception ex)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
              ActivateWallet();
              //SavePay();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Events
        private void btnCancelar_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            try
            {
                //PaymentGrid.Opacity = 0.3;
                //ModalValidation comfirmation = new ModalValidation("¿ESTÁ SEGURO DE CANCELAR?");
                //comfirmation.ShowDialog();
                //if (comfirmation.DialogResult.Value && comfirmation.DialogResult.HasValue)
                //{
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        this.Opacity = 0.6;
                        Utilities.Loading(frmLoading, true, this);
                    });
                    Utilities.control.StopAceptance();
                    if (PaymentViewModel.ValorIngresado > 0)
                    {
                        Utilities.DispenserVal = PaymentViewModel.ValorIngresado;
                        Utilities.control.callbackTotalOut = totalOut =>
                        {
                            Cancelled();
                        };

                        Utilities.control.callbackOut = quiantityOut =>
                        {
                            Cancelled();
                        };

                        Utilities.control.StartDispenser(Utilities.DispenserVal);
                    }
                    else
                    {
                        Utilities.GoToInicial();
                    }
                //}
                //PaymentGrid.Opacity = 1;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Método encargado de activar el billetero aceptance, seguido de esto crea un callback esperando a que este le indique que puede finalizar la transacción
        /// </summary>
        private void ActivateWallet()
        {
            try
            {
                payState = false;
                Utilities.control.callbackValueIn = enterValue =>
                {
                    if (enterValue > 0)
                    {
                        PaymentViewModel.ValorIngresado += enterValue;
                    }
                };

                Utilities.control.callbackTotalIn = enterTotal =>
                {

                    Dispatcher.BeginInvoke((Action)delegate { this.Opacity = 0.6; Utilities.Loading(frmLoading, true, this); });
                    Utilities.EnterTotal = enterTotal;
                    if (enterTotal > 0 && PaymentViewModel.ValorSobrante > 0)
                    {
                        ReturnMoney(PaymentViewModel.ValorSobrante, true);
                    }
                    else
                    {
                        SavePay();
                    }
                };

                Utilities.control.StartAceptance(PaymentViewModel.PayValue);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Método que se encarga de devolver el dinero ya sea por que se canceló la transacción o por que hay valor sobrante
        /// </summary>
        /// <param name="returnValue">valor a devolver</param>
        private void ReturnMoney(decimal returnValue, bool state)
        {
            try
            {
                Utilities.control.callbackTotalOut = totalOut =>
                {
                    if (state)
                    {
                        SavePay();
                    }
                    else
                    {
                        Cancelled();
                    }
                };

                Utilities.control.callbackOut = quiantityOut =>
                {
                    if (state)
                    {
                        SavePay();
                    }
                    else
                    {
                        Cancelled();
                    }
                };

                Utilities.control.StartDispenser(returnValue);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Método encargado de dar el estado inicial de todas las imagenes/botones de la vista
        /// </summary>
        private void VisibilityImage()
        {
            try
            {
                PaymentViewModel.ImgCancel = Visibility.Visible;
                PaymentViewModel.ImgIngreseBillete = Visibility.Visible;
                PaymentViewModel.ImgEspereCambio = Visibility.Hidden;
                PaymentViewModel.ImgLeyendoBillete = Visibility.Hidden;
                PaymentViewModel.ImgRecibo = Visibility.Hidden;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Método encargado de organizar todos los valores de la transacción en la vista
        /// </summary>
        private void OrganizeValues()
        {
            try
            {
                //lblValorPagar.Content = string.Format("{0:C0}", Utilities.PayVal);
                string a = String.Format("RDE {0:C0}", Utilities.PayVal);
                lblValorPagar.Text = a.Replace("$", "");

                PaymentViewModel = new PaymentViewModel
                {
                    PayValue = Utilities.PayVal,
                    ValorFaltante = Utilities.PayVal,
                    ValorSobrante = 0,
                    ValorIngresado = 0
                };

                VisibilityImage();
                this.DataContext = PaymentViewModel;
            }
            catch (Exception ex)
            {
            }
        }

        private void SavePay()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    PaySuccessfulWindow paySuccessful = new PaySuccessfulWindow();
                    paySuccessful.ShowDialog();
                });
                GC.Collect();
            }
            catch (Exception ex)
            {
            }
        }

        private void Cancelled()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    PaymentGrid.Opacity = 0.3;
                    Utilities.Loading(frmLoading, false, this);
                    ModalWindow modal = new ModalWindow("Usuario su pago fue cancelado.",false);
                    modal.ShowDialog();
                });
                GC.Collect();

                Utilities.GoToInicial();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
