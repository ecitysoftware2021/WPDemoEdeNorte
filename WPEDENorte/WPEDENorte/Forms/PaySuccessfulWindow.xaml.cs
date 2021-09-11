using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para PaySuccessfulWindow.xaml
    /// </summary>
    public partial class PaySuccessfulWindow : Window
    {
        private Utilities utilities;

        public PaySuccessfulWindow()
        {
            InitializeComponent();
            utilities = new Utilities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            utilities.ImprimirComprobante();
            Task.Run(() =>
            {
                Closes();
            });
        }

        private void Closes()
        {
            try
            {
                Thread.Sleep(6000);

                Dispatcher.Invoke(() =>
                {
                    MenuWindow paySuccessful = new MenuWindow();
                    paySuccessful.Show();
                    this.Close();
                });
                GC.Collect();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
