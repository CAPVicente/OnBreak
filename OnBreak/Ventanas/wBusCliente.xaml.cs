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
using OnBreak.BC;

namespace OnBreak.Ventanas
{
    /// <summary>
    /// Lógica de interacción para wBusCliente.xaml
    /// </summary>
    public partial class wBusCliente : MetroWindow
    {
        public wBusCliente()
        {
            InitializeComponent();
            tb_actRunCli.SetValue(TextBoxHelper.WatermarkProperty, "Rut Cliente");
            tb_actRazSoc.SetValue(TextBoxHelper.WatermarkProperty, "Razón Social");
            tb_actNomCli.SetValue(TextBoxHelper.WatermarkProperty, "Nombre");
            tb_actEmail.SetValue(TextBoxHelper.WatermarkProperty, "Email");
            tb_actDirCli.SetValue(TextBoxHelper.WatermarkProperty, "Dirección");
            tb_actTelCli.SetValue(TextBoxHelper.WatermarkProperty, "Teléfono");

            CargarClientes();
            LimpiarControles();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            VentanaClientes vc = new VentanaClientes();
            this.Hide();
            vc.Show();
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
            cb_actTipoEmp.ItemsSource = tipo_empresa.ReadAll();

            cb_actTipoEmp.DisplayMemberPath = "Descripcion";
            cb_actTipoEmp.SelectedValuePath = "IdTipoEmpresa";

            cb_actTipoEmp.SelectedIndex = 0;
        }

        private void CargarActividadEmpresa()
        {
            ActividadEmpresa actividad_empresa = new ActividadEmpresa();
            cb_actTvEmp.ItemsSource = actividad_empresa.ReadAll();

            cb_actTvEmp.DisplayMemberPath = "Descripcion";
            cb_actTvEmp.SelectedValuePath = "IdActividadEmpresa";

            cb_actTvEmp.SelectedIndex = 0;
        }

        private void btn_actualizarCli_Click(object sender, RoutedEventArgs e)
        {
            Cliente datosCliente = (Cliente)dg_listaEmp.SelectedItem;
            tb_actRunCli.Text = datosCliente.RutCliente;
            tb_actRazSoc.Text = datosCliente.RazonSocial;
            tb_actNomCli.Text = datosCliente.NombreContacto;
            tb_actEmail.Text = datosCliente.MailContacto;
            tb_actDirCli.Text = datosCliente.Direccion;
            tb_actTelCli.Text = datosCliente.Telefono;
            cb_actTvEmp.SelectedValue = datosCliente.IdActividadEmpresa;
            cb_actTipoEmp.SelectedValue = datosCliente.IdTipoEmpresa;
            fo_actualizarCli.IsOpen = true;
        }

        private async void btn_foActualizar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente()
            {
                RutCliente = tb_actRunCli.Text,
                RazonSocial = tb_actRazSoc.Text,
                NombreContacto = tb_actNomCli.Text,
                MailContacto = tb_actEmail.Text,
                Direccion = tb_actDirCli.Text,
                Telefono = tb_actTelCli.Text,
                IdActividadEmpresa = (int)cb_actTvEmp.SelectedValue,
                IdTipoEmpresa = (int)cb_actTipoEmp.SelectedValue
            };

            if (cliente.Update())
            {
                fo_actualizarCli.IsOpen = false;
                await this.ShowMessageAsync("Actualizar Cliente", "Cliente ha sido actualizado.");
                LimpiarControles();
            }
            else
            {
                await this.ShowMessageAsync("Actualizar Cliente", "Error al actualizar cliente.");
            }
            
        }

        private bool TieneContrato(string rutCliente)
        {
            Contrato contrato = new Contrato();
            Cliente cliente = new Cliente();
            Cliente datosClienteTieneContrato = (Cliente)dg_listaEmp.SelectedItem;

            if (contrato.ReadAllByRutContrato(rutCliente) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void btn_eliminarCli_Click(object sender, RoutedEventArgs e)
        {
            Cliente datosClienteBorrar = (Cliente)dg_listaEmp.SelectedItem;
            Cliente cliente = new Cliente()
            {
                RutCliente = datosClienteBorrar.RutCliente
            };

            string rutCli = datosClienteBorrar.RutCliente;

            if (TieneContrato(rutCli) == true)
            {
                if (cliente.Delete())
                {
                    await this.ShowMessageAsync("Buscar Cliente", "Cliente ha sido eliminado.");
                    LimpiarControles();
                }
                else
                {
                    await this.ShowMessageAsync("Buscar Cliente", "El cliente tiene un contrato asociado, este no ha podido ser eliminado.");
                } 
            }
            else
            {
                await this.ShowMessageAsync("Buscar Cliente", "El cliente tiene un contrato asociado, este no ha podido ser eliminado");
            }
            
        }

        private void CargarClientes()
        {
            Cliente cliente = new Cliente();
            dg_listaEmp.ItemsSource = cliente.ReadAll();
        }

        private void btn_BuscarCli_Click(object sender, RoutedEventArgs e)
        {
            dg_listaEmp.ItemsSource = null;
            Cliente cli = new Cliente();
            dg_listaEmp.ItemsSource = cli.ReadAllByRut(tb_ingreseRun.Text);
        }

        private void LimpiarControles()
        {
            tb_ingreseRun.Text = string.Empty;
            tb_actRunCli.Text = string.Empty;
            tb_actRazSoc.Text = string.Empty; 
            tb_actNomCli.Text = string.Empty;
            tb_actEmail.Text = string.Empty;
            tb_actDirCli.Text = string.Empty;
            tb_actTelCli.Text = string.Empty;

            CargarTipoEmpresa();
            CargarActividadEmpresa();

            Cliente cliente = new Cliente();
            dg_listaEmp.ItemsSource = cliente.ReadAll();

        }
    }
}
