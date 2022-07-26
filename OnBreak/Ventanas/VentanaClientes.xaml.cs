using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace OnBreak.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaClientes.xaml
    /// </summary>
    public partial class VentanaClientes : MetroWindow
    {
        public VentanaClientes()
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

        private void t_registrarCliente_Click(object sender, RoutedEventArgs e)
        {
            Ventanas.wRegCliente wRegCli = new Ventanas.wRegCliente();
            this.Hide();
            wRegCli.Show();
        }

        private void t_buscarCliente_Click (object sender, RoutedEventArgs e)
        {
            wBusCliente wbCli = new wBusCliente();
            this.Hide();
            wbCli.Show();
        }
    }
}
