using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;
using WPEDENorte.Classes;
using WPEDENorte.Services;

namespace WPEDENorte.Forms
{
    /// <summary>
    /// Interaction logic for FrmConfigurate.xaml
    /// </summary>
    public partial class ConfigurateWindow : Window
    {
        private Api api;
        private bool state;

        public ConfigurateWindow()
        {
            InitializeComponent();
            api = new Api();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                GetToken();
            });
        }

        /// <summary>
        /// Método encargado de obtener el token necesario para que el corresponsal pueda operar, seguido de esto se consulta el estado inicial del corresponsal
        /// para saber si se pueden realizar transacciones
        /// </summary>
        private async void GetToken()
        {
            try
            {
                Utilities util = new Utilities(1);
                state = await api.SecurityToken();
                if (state)
                {
                    var response = await api.GetResponse(new Uptake.RequestApi(), "InitPaypad");
                    if (response.CodeError == 200)
                    {
                        DataPaypad data = JsonConvert.DeserializeObject<DataPaypad>(response.Data.ToString());

                        if (data.State)
                        {
                            if (data.StateAceptance && data.StateDispenser)
                            {
                                Utilities.control.callbackError = error =>
                                {
                                    GetToken();
                                };
                                Utilities.control.callbackToken = isSucces =>
                                {
                                    Dispatcher.BeginInvoke((Action)delegate
                                    {
                                        InitialWindow inicio = new InitialWindow();
                                        inicio.Show();
                                        Close();
                                    });
                                };
                                Utilities.control.Start();
                            }
                            else
                            {
                                ShowModalError("No están disponibles los billeteros");
                            }
                        }
                        else
                        {
                            ShowModalError("No se pudo verificar el estado de los periféricos");
                        }
                    }
                    else
                    {
                        ShowModalError("No se pudo iniciar el cajero");
                    }
                }
                else
                {
                    ShowModalError("No se pudo verificar las credenciales.");
                }
            }
            catch (Exception ex)
            {
                ShowModalError(ex.Message, ex.StackTrace);
            }
        }

        private void ShowModalError(string description, string message = "")
        {
            Dispatcher.BeginInvoke((Action)delegate
            {
                ModalWindow modal = new ModalWindow(string.Concat("Lo sentimos,", Environment.NewLine, "el cajero no se encuentra disponible.\nError: ", description));
                modal.ShowDialog();
                if (modal.DialogResult.HasValue)
                {
                    Utilities.RestartApp();
                }
            });
        }
        }
}
