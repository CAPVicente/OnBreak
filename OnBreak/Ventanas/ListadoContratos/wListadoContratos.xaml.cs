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
using MahApps.Metro.Controls.Dialogs;

namespace OnBreak.Ventanas.ListadoContratos
{
    /// <summary>
    /// Lógica de interacción para wListadoContratos.xaml
    /// </summary>
    public partial class wListadoContratos : MetroWindow
    {
        private string _realizado = string.Empty;
        public wListadoContratos()
        {
            InitializeComponent();
            tb_busquedaRut.IsEnabled = false;
            tb_nroContrato.IsEnabled = false;
            cb_Relleno.IsEnabled = false;
            cb_ModalidadServicio.Visibility = Visibility.Hidden;
            cb_TipoEvento.Visibility = Visibility.Hidden;
            CargarModalidad();
            CargarTipoEvento();
            tb_ConvModalidad.Text = string.Empty;
            tb_ConvTipoServicio.Text = string.Empty;
            tb_ConvRealizado.Text = string.Empty;
            tb_ConvModalidad.IsEnabled = false;
            tb_ConvTipoServicio.IsEnabled = false;
            tb_ConvRealizado.IsEnabled = false;
            LimpiarCamposReadOnly();
        }

        #region Botones Barra de Título
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
        #endregion

        #region Métodos
        private void CargarModalidad()
        {
            ModalidadServicio modalidad = new ModalidadServicio();
            cb_ModalidadServicio.ItemsSource = modalidad.ReadAll();

            cb_ModalidadServicio.DisplayMemberPath = "Nombre";
            cb_ModalidadServicio.SelectedValuePath = "IdModalidad";

            cb_ModalidadServicio.SelectedIndex = -1;
        }

        private void CargarTipoEvento()
        {
            TipoEvento tipo_evento = new TipoEvento();
            cb_TipoEvento.ItemsSource = tipo_evento.ReadAll();

            cb_TipoEvento.DisplayMemberPath = "Descripcion";
            cb_TipoEvento.SelectedValuePath = "IdTipoEvento";

            cb_TipoEvento.SelectedIndex = -1;
        }
        #endregion

        private void cb_filtrarPor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LimpiarCamposReadOnly();
            if(cb_filtrarPor.SelectedIndex == 1)
            {
                tb_nroContrato.IsEnabled = true;

                tb_busquedaRut.IsEnabled = false;
                cb_Relleno.IsEnabled = false;

                cb_Relleno.Visibility = Visibility.Visible;
                cb_ModalidadServicio.Visibility = Visibility.Hidden;
                cb_TipoEvento.Visibility = Visibility.Hidden;
            }
            else if(cb_filtrarPor.SelectedIndex == 2)
            {
                tb_nroContrato.IsEnabled = false;

                tb_busquedaRut.IsEnabled = true;
                cb_Relleno.IsEnabled = false;

                cb_Relleno.Visibility = Visibility.Visible;
                cb_ModalidadServicio.Visibility = Visibility.Hidden;
                cb_TipoEvento.Visibility = Visibility.Hidden;
            }
            else if(cb_filtrarPor.SelectedIndex == 3)
            {
                tb_nroContrato.IsEnabled = false;

                tb_busquedaRut.IsEnabled = false;

                cb_Relleno.Visibility = Visibility.Hidden;
                cb_ModalidadServicio.Visibility = Visibility.Hidden;
                cb_TipoEvento.Visibility = Visibility.Visible;
            }
            else if(cb_filtrarPor.SelectedIndex == 4)
            {
                tb_nroContrato.IsEnabled = true;

                tb_busquedaRut.IsEnabled = false;

                cb_Relleno.Visibility = Visibility.Hidden;
                cb_ModalidadServicio.Visibility = Visibility.Visible;
                cb_TipoEvento.Visibility = Visibility.Hidden;
            }
        }
        private async void btn_listarClientes_Click(object sender, RoutedEventArgs e)
        {
            dg_listaCont.ItemsSource = null;
            Contrato contrato = new Contrato();

            if (cb_filtrarPor.SelectedIndex == 1)
            {
                // Filtrar por número de contrato
                dg_listaCont.ItemsSource = contrato.ReadAll();
            }
            if (cb_filtrarPor.SelectedIndex == 2)
            {
                // Filtrar por Rut del Cliente
                dg_listaCont.ItemsSource = contrato.ReadAllByRutContrato(tb_busquedaRut.Text);

                
            }
            if (cb_filtrarPor.SelectedIndex == 3)
            {
                // Filtrar por Tipo de Evento
                if(cb_TipoEvento.SelectedIndex != -1)
                {
                    dg_listaCont.ItemsSource = contrato.ReadAllByTipoEvento((int)cb_TipoEvento.SelectedValue);                
                }
                else
                {
                    await this.ShowMessageAsync("Listado Contratos", "Debe seleccionar un Tipo de Evento.");
                }          
            }
            if (cb_filtrarPor.SelectedIndex == 4)
            {
                // Filtrar por Modalidad de Servicio
                if (cb_ModalidadServicio.SelectedIndex != -1)
                {
                    dg_listaCont.ItemsSource = contrato.ReadAllByModalidadServicio((string)cb_ModalidadServicio.SelectedValue);                  
                }
                else
                {
                    await this.ShowMessageAsync("Listado Contratos", "Debe seleccionar una Modalidad de Servicio");
                }
            }
        }

        private void seleccionarRut_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            Contrato datosContrato = (Contrato)dg_listaCont.SelectedItem;
            if(datosContrato != null)
            {
                dg_listaCont.ItemsSource = contrato.ReadAllByRutContrato(datosContrato.RutCliente);
                tb_busquedaRut.Text = datosContrato.RutCliente;
            }           
        }

        private void dg_listaCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModalidadServicio modalidad = new ModalidadServicio();
            TipoEvento tipoevento = new TipoEvento();
            Contrato datosContratoConv = (Contrato)dg_listaCont.SelectedItem;

            // Modalidad de Servicio

            if (datosContratoConv != null)
            {
                if (datosContratoConv.IdModalidad == "CB001")
                {
                    tb_ConvModalidad.Text = "Light Break";
                }
                else if (datosContratoConv.IdModalidad == "CB002")
                {
                    tb_ConvModalidad.Text = "Journal Break";
                }
                else if (datosContratoConv.IdModalidad == "CB003")
                {
                    tb_ConvModalidad.Text = "Day Break";
                }
                else if (datosContratoConv.IdModalidad == "CE001")
                {
                    tb_ConvModalidad.Text = "Ejecutiva";
                }
                else if (datosContratoConv.IdModalidad == "CE002")
                {
                    tb_ConvModalidad.Text = "Celebración";
                }
                else if (datosContratoConv.IdModalidad == "CO001")
                {
                    tb_ConvModalidad.Text = "Quick Cocktail";
                }
                else if (datosContratoConv.IdModalidad == "CO002")
                {
                    tb_ConvModalidad.Text = "Ambient Cocktail";
                }
            }

            // Tipo de Evento
            if (datosContratoConv != null)
            {
                if (datosContratoConv.IdTipoEvento == 10)
                {
                    tb_ConvTipoServicio.Text = "Coffee Break";
                }
                else if (datosContratoConv.IdTipoEvento == 20)
                {
                    tb_ConvTipoServicio.Text = "Cocktail";
                }
                else if (datosContratoConv.IdTipoEvento == 30)
                {
                    tb_ConvTipoServicio.Text = "Cenas";
                } 
            }

            // Vigencia
            if (datosContratoConv != null)
            {
                if (datosContratoConv.Realizado == true)
                {
                    tb_ConvRealizado.Text = "Realizado";
                }
                else
                {
                    tb_ConvRealizado.Text = "No Realizado";
                } 
            }
        }

        private void LimpiarCamposReadOnly()
        {
            if (tb_ConvModalidad != null && tb_ConvTipoServicio != null && tb_ConvRealizado != null && tb_busquedaRut != null)
            {
                tb_ConvModalidad.Text = string.Empty;
                tb_ConvTipoServicio.Text = string.Empty;
                tb_ConvRealizado.Text = string.Empty;
                tb_busquedaRut.Text = string.Empty;
            }
        }
    }
}
