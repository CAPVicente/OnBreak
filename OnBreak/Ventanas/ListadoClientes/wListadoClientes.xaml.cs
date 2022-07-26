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
using System.Data;

namespace OnBreak.Ventanas.ListadoClientes
{
    /// <summary>
    /// Lógica de interacción para wListadoClientes.xaml
    /// </summary>
    public partial class wListadoClientes : MetroWindow
    {
        public Cliente ClienteSeleccionado
        {
            get
            {
                if (dg_listaCli.HasItems)
                {
                    if (dg_listaCli.SelectedItem != null)
                    {
                        return (Cliente)dg_listaCli.SelectedItem;

                    }
                }
                return null;
            }
        }
        public wListadoClientes()
        {
            InitializeComponent();
            generica.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            CargarActividadEmpresa();
            CargarTipoEmpresa();
            tb_mostrarTipoEmpresa.IsEnabled = false;
            tb_mostrarActividadEmpresa.IsEnabled = false;
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

        private void CargarTipoEmpresa()
        {
            TipoEmpresa tipo_empresa = new TipoEmpresa();
            cb_tipoEmpresaBusqueda.ItemsSource = tipo_empresa.ReadAll();

            cb_tipoEmpresaBusqueda.DisplayMemberPath = "Descripcion";
            cb_tipoEmpresaBusqueda.SelectedValuePath = "IdTipoEmpresa";

            cb_tipoEmpresaBusqueda.SelectedIndex = 0;
        }

        private void CargarActividadEmpresa()
        {
            ActividadEmpresa actividad_empresa = new ActividadEmpresa();
            cb_actividadEmpresaBusqueda.ItemsSource = actividad_empresa.ReadAll();

            cb_actividadEmpresaBusqueda.DisplayMemberPath = "Descripcion";
            cb_actividadEmpresaBusqueda.SelectedValuePath = "IdActividadEmpresa";

            cb_actividadEmpresaBusqueda.SelectedIndex = 0;
        }

        private void Tb_filtrarPor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LimpiarCamposSoloLectura();
            if (tb_filtrarPor.SelectedIndex == 2)
            {
                cb_tipoEmpresaBusqueda.Visibility = Visibility.Visible;
                cb_actividadEmpresaBusqueda.Visibility = Visibility.Hidden;
            }

            if (tb_filtrarPor.SelectedIndex == 3)
            {
                cb_actividadEmpresaBusqueda.Visibility = Visibility.Visible;
                cb_tipoEmpresaBusqueda.Visibility = Visibility.Hidden;
            }
        }

        private void btn_listarClientes_Click(object sender, RoutedEventArgs e)
        {
            dg_listaCli.ItemsSource = null;
            Cliente cliente = new Cliente();
            

            if (tb_filtrarPor.SelectedIndex == 1)
            {
                // Filtrar por rut
                
                dg_listaCli.ItemsSource = cliente.ReadAll();
            }
            if (tb_filtrarPor.SelectedIndex == 2)
            {
                // Filtrar por Tipo Empresa
                
                dg_listaCli.ItemsSource = cliente.ReadOnlyByTipoEmpresa((int)cb_tipoEmpresaBusqueda.SelectedValue);
            }
            if (tb_filtrarPor.SelectedIndex == 3)
            {
                // Filtrar por Actividad Empresa
                
                dg_listaCli.ItemsSource = cliente.ReadOnlyByActividadEmpresa((int)cb_actividadEmpresaBusqueda.SelectedValue);
            }
        }

        private void dg_listaCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cliente datosCliente = (Cliente)dg_listaCli.SelectedItem;

            if (datosCliente != null)
            {
                if (datosCliente.IdActividadEmpresa == 1)
                {
                    tb_mostrarActividadEmpresa.Text = "Agropecuaria";
                }
                else if (datosCliente.IdActividadEmpresa == 2)
                {
                    tb_mostrarActividadEmpresa.Text = "Minería";
                }
                else if (datosCliente.IdActividadEmpresa == 3)
                {
                    tb_mostrarActividadEmpresa.Text = "Manufactura";
                }
                else if (datosCliente.IdActividadEmpresa == 4)
                {
                    tb_mostrarActividadEmpresa.Text = "Comercio";
                }
                else if (datosCliente.IdActividadEmpresa == 5)
                {
                    tb_mostrarActividadEmpresa.Text = "Hotelería";
                }
                else if (datosCliente.IdActividadEmpresa == 6)
                {
                    tb_mostrarActividadEmpresa.Text = "Alimentos";
                }
                else if (datosCliente.IdActividadEmpresa == 7)
                {
                    tb_mostrarActividadEmpresa.Text = "Transporte";
                }
                else if (datosCliente.IdActividadEmpresa == 8)
                {
                    tb_mostrarActividadEmpresa.Text = "Servicios";
                }
            }
            if (datosCliente != null)
            {
                if (datosCliente.IdTipoEmpresa == 10)
                {
                    tb_mostrarTipoEmpresa.Text = "SPA";
                }
                else if (datosCliente.IdTipoEmpresa == 20)
                {
                    tb_mostrarTipoEmpresa.Text = "EIRL";
                }
                else if (datosCliente.IdTipoEmpresa == 30)
                {
                    tb_mostrarTipoEmpresa.Text = "Limitada";
                }
                else if (datosCliente.IdTipoEmpresa == 40)
                {
                    tb_mostrarTipoEmpresa.Text = "Sociedad Anónima";
                }
            }
        }

        public void LimpiarCamposSoloLectura()
        {
            if (tb_mostrarActividadEmpresa != null && tb_mostrarTipoEmpresa != null)
            {
                tb_mostrarTipoEmpresa.Text = string.Empty;
                tb_mostrarActividadEmpresa.Text = string.Empty;
            }
        }
    }
}
