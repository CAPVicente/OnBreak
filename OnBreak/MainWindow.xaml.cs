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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;

namespace OnBreak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Ventanas.VentanaClientes ventanaCli = new Ventanas.VentanaClientes();
        Ventanas.wRegCliente ventanaRegCli = new Ventanas.wRegCliente();
        Ventanas.wBusCliente ventanaBusCli = new Ventanas.wBusCliente();
        Ventanas.ListadoClientes.wListadoClientes wListadoClientes = new Ventanas.ListadoClientes.wListadoClientes();
        Ventanas.Contratos.VentanaContratos ventanaContratos = new Ventanas.Contratos.VentanaContratos();
        Ventanas.Contratos.wBusContrato ventanaBusCont = new Ventanas.Contratos.wBusContrato();
        Ventanas.Contratos.wRegContrato ventanaRegCont = new Ventanas.Contratos.wRegContrato();
        Ventanas.ListadoContratos.wListadoContratos wListadoContratos = new Ventanas.ListadoContratos.wListadoContratos();
        public MainWindow()
        {
            InitializeComponent();
            ts_displaymode.IsEnabled = false;
        }

        public void ts_displaymode_Toggled(object sender, RoutedEventArgs e)
        {
            SolidColorBrush rojoPredominante = new SolidColorBrush(Color.FromRgb(120, 13, 20));
            SolidColorBrush defaultbgcolor = new SolidColorBrush(Color.FromRgb(253, 246, 237));
            SolidColorBrush bgcolor = new SolidColorBrush(Color.FromRgb(45, 0, 0));
            SolidColorBrush TitleBarColor = new SolidColorBrush(Color.FromRgb(62, 3, 3));
            SolidColorBrush TitleBarColorDefault = new SolidColorBrush(Color.FromRgb(245, 231, 212));

            SolidColorBrush defaultTiles = new SolidColorBrush(Color.FromRgb(247, 230, 196));
            SolidColorBrush darkmodeTiles = new SolidColorBrush(Color.FromRgb(162, 45, 52));

            SolidColorBrush darkGrayTrue = new SolidColorBrush(Color.FromRgb(31, 31, 31));

            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    bg_grid.Background = bgcolor;
                    ventanaCli.bg_grid.Background = bgcolor;
                    logo_Empresa.Visibility = Visibility.Hidden;
                    logo_EmpresaWhite.Visibility = Visibility.Visible;
                    TitleBar.Background = TitleBarColor;
                    Titulo.Foreground = System.Windows.Media.Brushes.White;
                    Minimize.Background = TitleBarColor;
                    Close.Background = TitleBarColor;
                    minimize_Icon.Foreground = System.Windows.Media.Brushes.White;
                    closeIcon.Foreground = System.Windows.Media.Brushes.White;

                    t_clientes.Background = darkmodeTiles;
                    t_listadoClientes.Background = darkmodeTiles;
                    tile_Contratos.Background = darkmodeTiles;
                    tile_listadoContratos.Background = darkmodeTiles;

                    t_clientesText.Foreground = System.Windows.Media.Brushes.White;
                    t_listadoClientesText.Foreground = System.Windows.Media.Brushes.White;
                    tile_ContratosText.Foreground = System.Windows.Media.Brushes.White; 
                    t_listadoContratosText.Foreground = System.Windows.Media.Brushes.White;

                    btn_Settings.Background = darkmodeTiles;
                    btn_Settings.BorderBrush = System.Windows.Media.Brushes.White;
                    settingsIcon.Foreground = System.Windows.Media.Brushes.White;

                    fo_settings.Background = darkGrayTrue;
                }
                else
                {
                    bg_grid.Background = defaultbgcolor;
                    logo_Empresa.Visibility = Visibility.Visible;
                    logo_EmpresaWhite.Visibility = Visibility.Hidden;
                    TitleBar.Background = TitleBarColorDefault;
                    Titulo.Foreground = rojoPredominante;
                    Minimize.Background = TitleBarColorDefault;
                    Close.Background = TitleBarColorDefault;
                    minimize_Icon.Foreground = rojoPredominante;
                    closeIcon.Foreground = rojoPredominante;

                    t_clientes.Background = defaultTiles;
                    t_listadoClientes.Background = defaultTiles;
                    tile_Contratos.Background = defaultTiles;
                    tile_listadoContratos.Background = defaultTiles;

                    t_clientesText.Foreground = rojoPredominante;
                    t_listadoClientesText.Foreground = rojoPredominante;
                    tile_ContratosText.Foreground = rojoPredominante;
                    t_listadoContratosText.Foreground = rojoPredominante;

                    btn_Settings.Background = defaultTiles;
                    btn_Settings.BorderBrush = rojoPredominante;
                    settingsIcon.Foreground = rojoPredominante;

                    fo_settings.Background = rojoPredominante;
                }
            }
        }

        private void t_clientes_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ventanaCli.Show();   
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void t_listadoClientes_Click(object sender, RoutedEventArgs e)
        {
            Ventanas.ListadoClientes.wListadoClientes lisCli = new Ventanas.ListadoClientes.wListadoClientes();
            this.Hide();
            lisCli.Show();
        }

        private void tile_Contratos_Click(object sender, RoutedEventArgs e)
        {
            Ventanas.Contratos.VentanaContratos vContratos = new Ventanas.Contratos.VentanaContratos();
            this.Hide();
            vContratos.Show();
        }

        private void tile_listadoContratos_Click(object sender, RoutedEventArgs e)
        {
            Ventanas.ListadoContratos.wListadoContratos listaContratos = new Ventanas.ListadoContratos.wListadoContratos();
            this.Hide();
            listaContratos.Show();
        }

        private void btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            fo_settings.IsOpen = true;
        }

        private void Btn_accederCapaWeb_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:59478/Index.aspx"); 
        }

        private void cb_Resoluciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedOption = cb_Resoluciones.SelectedIndex;
            switch (selectedOption)
            {
                case 1:
                    Application.Current.MainWindow.Width = 1024;
                    Application.Current.MainWindow.Height = 576;
                    

                    break;
            }
        }
    }
}
