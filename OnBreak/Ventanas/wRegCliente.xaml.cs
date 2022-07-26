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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using OnBreak.BC;

namespace OnBreak.Ventanas
{
    /// <summary>
    /// Lógica de interacción para wRegCliente.xaml
    /// </summary>
    public partial class wRegCliente : MetroWindow
    {

        private bool _camposValidados = false;
        public wRegCliente()
        {
            InitializeComponent();
            LimpiarControles();
           /* CargarActividadEmpresa();
            CargarTipoEmpresa();*/
            tb_rutCliente.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            tb_direccion.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            tb_email.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            tb_Nombre.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            tb_razonSocial.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            tb_telefono.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            VentanaClientes vcli = new VentanaClientes();
            this.Hide();
            vcli.Show();
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
            cb_tipoEmpresa.ItemsSource = tipo_empresa.ReadAll();

            cb_tipoEmpresa.DisplayMemberPath = "Descripcion";
            cb_tipoEmpresa.SelectedValuePath = "IdTipoEmpresa";

            cb_tipoEmpresa.SelectedIndex = 0;
        }

        private void CargarActividadEmpresa()
        {
            ActividadEmpresa actividad_empresa = new ActividadEmpresa();
            cb_actEmpresa.ItemsSource = actividad_empresa.ReadAll();

            cb_actEmpresa.DisplayMemberPath = "Descripcion";
            cb_actEmpresa.SelectedValuePath = "IdActividadEmpresa";

            cb_actEmpresa.SelectedIndex = 0;
        }

        #region Validaciones de Campo

        #endregion
        
        private async void btn_Registrar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente()
            {
                RutCliente = tb_rutCliente.Text,
                RazonSocial = tb_razonSocial.Text,
                NombreContacto = tb_Nombre.Text,
                MailContacto = tb_email.Text,
                Direccion = tb_direccion.Text,
                Telefono = tb_telefono.Text,
                IdActividadEmpresa = (int)cb_actEmpresa.SelectedValue,
                IdTipoEmpresa = (int)cb_tipoEmpresa.SelectedValue
            };

            // Validación de Campos rellenos

            if (string.IsNullOrEmpty(tb_rutCliente.Text) || string.IsNullOrWhiteSpace(tb_rutCliente.Text))
            {
                _camposValidados = false;
                await this.ShowMessageAsync("Error", "Campo RUT Cliente debe estar rellenado.");
            }
            else
            {
                if (string.IsNullOrEmpty(tb_razonSocial.Text) || string.IsNullOrWhiteSpace(tb_razonSocial.Text))
                {
                    _camposValidados = false;
                    await this.ShowMessageAsync("Error", "Campo  Razón Social debe estar rellenado.");
                }
                else
                {
                    if (string.IsNullOrEmpty(tb_Nombre.Text) || string.IsNullOrWhiteSpace(tb_Nombre.Text))
                    {
                        _camposValidados = false;
                        await this.ShowMessageAsync("Error", "Campo Nombre debe estar rellenado.");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(tb_email.Text) || string.IsNullOrWhiteSpace(tb_email.Text))
                        {
                            _camposValidados = false;
                            await this.ShowMessageAsync("Error", "Campo Email debe estar rellenado.");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(tb_direccion.Text) || string.IsNullOrWhiteSpace(tb_direccion.Text))
                            {
                                _camposValidados = false;
                                await this.ShowMessageAsync("Error", "Campo Dirección debe estar rellenado.");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(tb_telefono.Text) || string.IsNullOrWhiteSpace(tb_telefono.Text))
                                {
                                    _camposValidados = false;
                                    await this.ShowMessageAsync("Error", "Campo Teléfono debe estar rellenado.");
                                }
                                else
                                {
                                    _camposValidados = true;
                                }
                            }
                        }
                    }
                }
            }
            if (_camposValidados == true)
            {
                if (cliente.Create())
                {
                    await this.ShowMessageAsync("Registrar Cliente", "Cliente registrado exitosamente");
                    LimpiarControles();
                }
                else
                {
                    await this.ShowMessageAsync("Registrar Cliente", "Error al registrar cliente.");
                } 
            }
            else
            {
                await this.ShowMessageAsync("Registrar Cliente","Faltan campos por rellenar.");
            }

        }

        private void LimpiarControles()
        {
            /* Limpia los controles de texto */
            tb_rutCliente.Text = string.Empty;
            tb_razonSocial.Text = string.Empty;
            tb_Nombre.Text = string.Empty;
            tb_email.Text = string.Empty;
            tb_direccion.Text = string.Empty;
            tb_telefono.Text = string.Empty;

            CargarActividadEmpresa();
            CargarTipoEmpresa();

            
        }

    }
}
