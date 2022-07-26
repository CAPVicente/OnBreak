using MahApps.Metro.Controls;
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

namespace OnBreak.Ventanas.Contratos
{
    /// <summary>
    /// Lógica de interacción para VentanaContratos.xaml
    /// </summary>
    public partial class VentanaContratos : MetroWindow
    {
        public VentanaContratos()
        {
            InitializeComponent();
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Hide();
            mw.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void t_nuevoContrato_Click(object sender, RoutedEventArgs e)
        {
            wRegContrato regContrato = new wRegContrato();
            this.Hide();
            regContrato.Show();
        }

        private void t_buscarContrato_Click(object sender, RoutedEventArgs e)
        {
            wBusContrato busContrato = new wBusContrato();
            this.Hide();
            busContrato.Show();
        }
    }
}
