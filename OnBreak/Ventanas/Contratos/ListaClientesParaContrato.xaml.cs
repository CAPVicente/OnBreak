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
using OnBreak.BC;

namespace OnBreak.Ventanas.Contratos
{
    /// <summary>
    /// Lógica de interacción para ListaClientesParaContrato.xaml
    /// </summary>
    public partial class ListaClientesParaContrato : MetroWindow
    {
        private readonly wRegContrato _regContrato;
        public ListaClientesParaContrato(wRegContrato regContrato)
        {
            _regContrato = regContrato;
            InitializeComponent();
            BarraDeBusqueda.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            CargarActividadEmpresa();
            CargarTipoEmpresa();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CargarTipoEmpresa()
        {
            TipoEmpresa tipo_empresa = new TipoEmpresa();
            cb_tipoEmpresaBusquedaParaContrato.ItemsSource = tipo_empresa.ReadAll();

            cb_tipoEmpresaBusquedaParaContrato.DisplayMemberPath = "Descripcion";
            cb_tipoEmpresaBusquedaParaContrato.SelectedValuePath = "IdTipoEmpresa";

            cb_tipoEmpresaBusquedaParaContrato.SelectedIndex = 0;
        }

        private void CargarActividadEmpresa()
        {
            ActividadEmpresa actividad_empresa = new ActividadEmpresa();
            cb_actividadEmpresaBusquedaParaContrato.ItemsSource = actividad_empresa.ReadAll();

            cb_actividadEmpresaBusquedaParaContrato.DisplayMemberPath = "Descripcion";
            cb_actividadEmpresaBusquedaParaContrato.SelectedValuePath = "IdActividadEmpresa";

            cb_actividadEmpresaBusquedaParaContrato.SelectedIndex = 0;
        }

        private void tb_filtrarPorClienteContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tb_filtrarPorClienteContrato.SelectedIndex == 2)
            {
                cb_tipoEmpresaBusquedaParaContrato.Visibility = Visibility.Visible;
                cb_actividadEmpresaBusquedaParaContrato.Visibility = Visibility.Hidden;
            }

            if (tb_filtrarPorClienteContrato.SelectedIndex == 3)
            {
                cb_actividadEmpresaBusquedaParaContrato.Visibility = Visibility.Visible;
                cb_tipoEmpresaBusquedaParaContrato.Visibility = Visibility.Hidden;
            }
        }

        private void btn_listarClientes_Click(object sender, RoutedEventArgs e)
        {
            dg_listaCliParaContrato.ItemsSource = null;
            Cliente cliente = new Cliente();

            if (tb_filtrarPorClienteContrato.SelectedIndex == 1)
            {
                // Filtrar por rut

                dg_listaCliParaContrato.ItemsSource = cliente.ReadAll();
            }
            if (tb_filtrarPorClienteContrato.SelectedIndex == 2)
            {
                // Filtrar por Tipo Empresa

                dg_listaCliParaContrato.ItemsSource = cliente.ReadOnlyByTipoEmpresa((int)cb_tipoEmpresaBusquedaParaContrato.SelectedValue);
            }
            if (tb_filtrarPorClienteContrato.SelectedIndex == 3)
            {
                // Filtrar por Actividad Empresa

                dg_listaCliParaContrato.ItemsSource = cliente.ReadOnlyByActividadEmpresa((int)cb_actividadEmpresaBusquedaParaContrato.SelectedValue);
            }
        }

        private void btn_Seleccionar_Click(object sender, RoutedEventArgs e)
        {
            Cliente clienteDatos = (Cliente)dg_listaCliParaContrato.SelectedItem;

            _regContrato.SetRutToTextBox(clienteDatos.RutCliente);

            this.Close();
            
        }
    }
}
