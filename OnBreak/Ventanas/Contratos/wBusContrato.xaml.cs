using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using OnBreak.BC;
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
    /// Lógica de interacción para wBusContrato.xaml
    /// </summary>
    public partial class wBusContrato : MetroWindow
    {
        private bool _finalizado = false;
        private double nuevoValorFinal = 0;
        private int valorArriendoDefecto = 0;
        private string _numeroContratoAEvaluar = string.Empty;
        private int _valorBaseDefecto = 0;
        private string _modalidadSeleccionada = string.Empty;

        //---------- NUEVOS VALORES
        private string _nuevoNumero = string.Empty;
        private DateTime _nuevaCreacion = new DateTime();
        private DateTime _nuevoTermino = new DateTime();
        private string _nuevoRut = string.Empty;
        private string _nuevaModalidad = string.Empty;
        private int _nuevoTipoEvento = 0;
        private DateTime _nuevaFechaInicio = new DateTime();
        private DateTime _nuevaFechaTermino = new DateTime();
        private int _nuevoAsistentes = 0;
        private int _nuevoPersonal = 0;
        private bool _nuevoRealizado = false;
        private double _nuevoValorFinalCont = 0;
        private string _nuevoObservaciones = string.Empty;

        public wBusContrato()
        {
            InitializeComponent();
            tb_actNCont.SetValue(TextBoxHelper.WatermarkProperty, "Número de Contrato");
            tb_actObservaciones.SetValue(TextBoxHelper.WatermarkProperty, "Observaciones...");
            CargarContratos();
            LimpiarControles();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            VentanaContratos vc = new VentanaContratos();
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

        private async void btn_eliminarCont_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            Contrato datosContrato = (Contrato)dg_listaCont.SelectedItem;
            var settings = new MetroDialogSettings { AffirmativeButtonText = "Sí", NegativeButtonText = "No" };
            if (datosContrato != null)
            {
                if (datosContrato.FechaHoraInicio > fechahoy)
                {
                    _finalizado = false;
                }
                else
                {
                    _finalizado = true;
                }

                if (datosContrato.Realizado == false)
                {
                    var botonPulsado = await this.ShowMessageAsync("Buscar Contrato", "¿Está seguro de eliminar el contrato seleccionado?", MessageDialogStyle.AffirmativeAndNegative, settings);

                    if (botonPulsado == MessageDialogResult.Affirmative)
                    {
                        Contrato contrato = new Contrato
                        {
                            Numero = datosContrato.Numero,
                            Creacion = datosContrato.Creacion,
                            Termino = DateTime.Now,
                            RutCliente = datosContrato.RutCliente,
                            IdModalidad = datosContrato.IdModalidad,
                            IdTipoEvento = datosContrato.IdTipoEvento,
                            FechaHoraInicio = datosContrato.FechaHoraInicio,
                            FechaHoraTermino = datosContrato.FechaHoraTermino,
                            Asistentes = datosContrato.Asistentes,
                            PersonalAdicional = datosContrato.PersonalAdicional,
                            Realizado = _finalizado,
                            ValorTotalContrato = datosContrato.ValorTotalContrato,
                            Observaciones = datosContrato.Observaciones,
                        };

                        if (contrato.Update())
                        {
                            await this.ShowMessageAsync("Buscar Contrato", "Contrato ha sido finalizado.");
                            LimpiarControles();
                        }
                        else
                        {
                            await this.ShowMessageAsync("Buscar Contrato", "Error al finalizar contrato.");
                        }
                    }
                    else
                    {
                        await this.ShowMessageAsync("Buscar Contrato", "Error al finalizar contrato (literalmente cancelaste xd).");
                    }
                }
            }  
        }

        private void btn_actualizarCont_Click(object sender, RoutedEventArgs e)
        {
            fo_actualizarCont.IsOpen = true;

            Contrato datosEditarContrato = (Contrato)dg_listaCont.SelectedItem;
            
            tb_actNCont.Text = datosEditarContrato.Numero;
            tb_actObservaciones.Text = datosEditarContrato.Observaciones;
            cb_actTipoEvento.SelectedValue = datosEditarContrato.IdTipoEvento;
            cb_actModalidad.SelectedValue = datosEditarContrato.IdModalidad;
            datep_fechaIniActCont.SelectedDateTime = datosEditarContrato.FechaHoraInicio;
            datep_fechaTerActCont.SelectedDateTime = datosEditarContrato.FechaHoraTermino;
            tb_actAsistentes.Text = datosEditarContrato.Asistentes.ToString();
            tb_actPersonal.Text = datosEditarContrato.PersonalAdicional.ToString();
            tb_nuevoValorFinal.Text = datosEditarContrato.ValorTotalContrato.ToString();

            _numeroContratoAEvaluar = datosEditarContrato.Numero;
            _modalidadSeleccionada = datosEditarContrato.IdModalidad;

            _nuevoNumero = datosEditarContrato.Numero;
            _nuevaCreacion = datosEditarContrato.Creacion;
            _nuevoTermino = datosEditarContrato.Termino;
            _nuevoRut = datosEditarContrato.RutCliente;
            _nuevaModalidad = datosEditarContrato.IdModalidad;
            _nuevoTipoEvento = datosEditarContrato.IdTipoEvento;
            _nuevaFechaInicio = (DateTime)datosEditarContrato.FechaHoraInicio;
            _nuevaFechaTermino = (DateTime)datosEditarContrato.FechaHoraTermino;
            _nuevoAsistentes = datosEditarContrato.Asistentes;
            _nuevoPersonal = datosEditarContrato.PersonalAdicional;
            _nuevoRealizado = datosEditarContrato.Realizado;
            _nuevoValorFinalCont = datosEditarContrato.ValorTotalContrato;
            _nuevoObservaciones = datosEditarContrato.Observaciones;

            if (_modalidadSeleccionada == "CB001")
            {
                _valorBaseDefecto = 3;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CB002")
            {
                _valorBaseDefecto = 8;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CB003")
            {
                _valorBaseDefecto = 12;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CE001")
            {
                _valorBaseDefecto = 25;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CE002")
            {
                _valorBaseDefecto = 35;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CO001")
            {
                _valorBaseDefecto = 6;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }
            else if (_modalidadSeleccionada == "CO002")
            {
                _valorBaseDefecto = 10;
                tb_exValorBase.Text = _valorBaseDefecto.ToString();
            }

            if (DateTime.Now > datosEditarContrato.Termino)
            {
                datep_fechaIniActCont.IsEnabled = false;
                datep_fechaTerActCont.IsEnabled = false;
                cb_actTipoEvento.IsEnabled = false;
                cb_actModalidad.IsEnabled = false;
                tb_actObservaciones.IsEnabled = false;
                tb_actPersonal.IsEnabled = false;
                tb_actAsistentes.IsEnabled = false;

                btn_foActualizar.IsEnabled = false;    
            }   
        }

        private async void btn_foActualizar_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato()
            {
                Numero = _nuevoNumero,
                Creacion = _nuevaCreacion,
                Termino = _nuevoTermino,
                RutCliente = _nuevoRut,
                IdModalidad = _nuevaModalidad,
                IdTipoEvento = _nuevoTipoEvento,
                FechaHoraInicio = datep_fechaIniActCont.SelectedDateTime, //nuevo
                FechaHoraTermino = datep_fechaTerActCont.SelectedDateTime, //nuevo
                Asistentes = int.Parse(tb_actAsistentes.Text), //nuevo
                PersonalAdicional = int.Parse(tb_actPersonal.Text), //nuevo
                Realizado = _nuevoRealizado,
                ValorTotalContrato = nuevoValorFinal, //nuevo
                Observaciones = tb_actObservaciones.Text,
            };

            if (contrato.Update())
            {
                await this.ShowMessageAsync("Buscar Contrato", "Contrato ha sido actualizado.");
                LimpiarControles();
                fo_actualizarCont.IsOpen = false;
            }
            else
            {
                await this.ShowMessageAsync("Buscar Contrato", "Error al actualizar contarto.");
            }
            
        }

        private void CargarContratos()
        {
            Contrato contrato = new Contrato();
            dg_listaCont.ItemsSource = contrato.ReadAll();
        }

        private void btn_BuscarCont_Click(object sender, RoutedEventArgs e)
        {
            dg_listaCont.ItemsSource = null;
            Contrato contrato = new Contrato();
            dg_listaCont.ItemsSource = contrato.ReadAllByNumber(tb_ingreseNContrato.Text);
        }

        private void LimpiarControles()
        {
            Contrato contrato = new Contrato();
            dg_listaCont.ItemsSource = contrato.ReadAll();

            // Desactivar controles de Flyout

            cb_actModalidad.IsEnabled = false;
            cb_actTipoEvento.IsEnabled = false;
            tb_nuevoValorFinal.IsEnabled = false;
            btn_foActualizar.IsEnabled = false;
            tb_exValorBase.IsEnabled = false;
            CargarTipoEvento();
            CargarModalidad();
            CargarContratos();

        }

        #region Cargar Combobox

        private void CargarTipoEvento()
        {
            TipoEvento tipo_evento = new TipoEvento();
            cb_actTipoEvento.ItemsSource = tipo_evento.ReadAll();

            cb_actTipoEvento.DisplayMemberPath = "Descripcion";
            cb_actTipoEvento.SelectedValuePath = "IdTipoEvento";

            cb_actTipoEvento.SelectedIndex = -1;
        }

        private void CargarModalidad()
        {
            ModalidadServicio modalidad = new ModalidadServicio();
            cb_actModalidad.ItemsSource = modalidad.ReadAll();

            cb_actModalidad.DisplayMemberPath = "Nombre";
            cb_actModalidad.SelectedValuePath = "IdModalidad";

            cb_actModalidad.SelectedIndex = -1;
        }



        private void Cb_actTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        #endregion

        #region Recargo Nuevo

        public double RealizarCalculoFinal(int nAsistentes, int nPersonalAdicional, int nValorArriendo, int nValorBase)
        {
            double recargoAsistentes = 0;
            double recargoPersonal = 0;
            double recargoAmbientacion = 0;
            double recargoMusicaAmb = 0;
            double recargoLocal = 0;
            int ambientacionCocktail = 10;
            int ambientacionCenas = 10;
            bool musicacocktail = true;
            bool musicacenas = true;
            bool localOB = true;

            if (cb_actTipoEvento.SelectedValue != null)
            {
                if ((int)cb_actTipoEvento.SelectedValue == 10) // Coffee Break
                {
                    // Recargo Asistentes
                    if (nAsistentes > 0 && nAsistentes < 21)
                    {
                        recargoAsistentes = 3;
                    }
                    else if (nAsistentes > 20 && nAsistentes < 51)
                    {
                        recargoAsistentes = 5;
                    }
                    else if (nAsistentes > 50)
                    {
                        recargoAsistentes = (nAsistentes / 20) * 2;
                    }
                    else
                    {
                        recargoAsistentes = 0;
                    }
                    // Recargo Personal Adicional
                    if (nPersonalAdicional == 2)
                    {
                        recargoPersonal = 2;
                    }
                    else if (nPersonalAdicional == 3)
                    {
                        recargoPersonal = 3;
                    }
                    else if (nPersonalAdicional == 4)
                    {
                        recargoPersonal = 4;
                    }
                    else if (nPersonalAdicional > 4)
                    {
                        recargoPersonal = 3.5 + (nPersonalAdicional * 0.5);
                    }
                    else
                    {
                        recargoPersonal = 0;
                    }
                }
                else if ((int)cb_actTipoEvento.SelectedValue == 20) // Cocktail
                {
                    // Recargo Asistentes
                    if (nAsistentes > 0 && nAsistentes < 21)
                    {
                        recargoAsistentes = 4;
                    }
                    else if (nAsistentes > 20 && nAsistentes < 51)
                    {
                        recargoAsistentes = 6;
                    }
                    else if (nAsistentes > 50)
                    {
                        recargoAsistentes = (nAsistentes / 20) * 2;
                    }
                    else
                    {
                        recargoAsistentes = 0;
                    }
                    // Recargo Personal Adicional
                    if (nPersonalAdicional == 2)
                    {
                        recargoPersonal = 2;
                    }
                    else if (nPersonalAdicional == 3)
                    {
                        recargoPersonal = 3;
                    }
                    else if (nPersonalAdicional == 4)
                    {
                        recargoPersonal = 4;
                    }
                    else if (nPersonalAdicional > 4)
                    {
                        recargoPersonal = 3.5 + (nPersonalAdicional * 0.5);
                    }
                    else
                    {
                        recargoPersonal = 0;
                    }
                    // Recargo Ambientación
                    if (ambientacionCocktail != 0)
                    {
                        if ((int)ambientacionCocktail == 10)
                        {
                            recargoAmbientacion = 2;
                        }
                        else if ((int)ambientacionCocktail == 20)
                        {
                            recargoAmbientacion = 5;
                        }
                    }
                    else
                    {
                        recargoAmbientacion = 0;
                    }
                    // Recargo Ambientación Música
                    if (musicacocktail == true)
                    {
                        recargoMusicaAmb = 1;
                    }
                    else
                    {
                        recargoMusicaAmb = 0;
                    }
                }
                else if ((int)cb_actTipoEvento.SelectedValue == 30) // Cenas
                {
                    // Recargo Asistentes
                    if (nAsistentes > 0 && nAsistentes < 21)
                    {
                        recargoAsistentes = nAsistentes * 1.5;
                    }
                    else if (nAsistentes > 20 && nAsistentes < 51)
                    {
                        recargoAsistentes = nAsistentes * 1.2;
                    }
                    else if (nAsistentes > 50)
                    {
                        recargoAsistentes = (nAsistentes);
                    }
                    else
                    {
                        recargoAsistentes = 0;
                    }
                    // Personal Adicional
                    if (nPersonalAdicional == 2)
                    {
                        recargoPersonal = 3;
                    }
                    else if (nPersonalAdicional == 3)
                    {
                        recargoPersonal = 4;
                    }
                    else if (nPersonalAdicional == 4)
                    {
                        recargoPersonal = 5;
                    }
                    else if (nPersonalAdicional > 4)
                    {
                        recargoPersonal = 5 + (nPersonalAdicional * 0.5);
                    }
                    else
                    {
                        recargoPersonal = 0;
                    }
                    // Ambientación
                    if (ambientacionCenas != 0)
                    {
                        if ((int)ambientacionCenas == 10)
                        {
                            recargoAmbientacion = 3;
                        }
                        else if ((int)ambientacionCenas == 20)
                        {
                            recargoAmbientacion = 5;
                        }
                    }
                    else
                    {
                        recargoAmbientacion = 0;
                    }
                    // Música Ambiental
                    if (musicacenas == true)
                    {
                        recargoMusicaAmb = 1.5;
                    }
                    else
                    {
                        recargoMusicaAmb = 0;
                    }
                    // Local
                    if (localOB == true)
                    {
                        recargoLocal = 9;
                    }
                    else
                    {
                        recargoLocal = 0;
                    }
                }
            }
            else
            {
                return 1;
            }
            double valorfinal = nValorArriendo + nValorBase + recargoAsistentes + recargoPersonal + recargoAmbientacion + recargoMusicaAmb + recargoLocal;
            return valorfinal;
        }

        #endregion

        private void btn_reCotizar_Click(object sender, RoutedEventArgs e)
        {
            Cenas cenas = new Cenas()
            {
                Numero = _numeroContratoAEvaluar
            };
            cenas.Read();
            btn_foActualizar.IsEnabled = true;
            int asistentes = int.Parse(tb_actAsistentes.Text);
            int personal = int.Parse(tb_actPersonal.Text);
            int valorarriendo = 0;
            int valorbase = int.Parse(tb_exValorBase.Text);
            nuevoValorFinal = RealizarCalculoFinal(asistentes, personal, valorarriendo, valorbase);
            tb_nuevoValorFinal.Text = nuevoValorFinal.ToString();
        }

        private void cb_Convertidor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double valorConvertido = 0;
            if (cb_Convertidor.SelectedIndex == 0)
            {
                tb_nuevoValorFinal.Text = nuevoValorFinal.ToString();
            }
            else if (cb_Convertidor.SelectedIndex == 1)
            {
                valorConvertido = nuevoValorFinal * 32767;
                tb_nuevoValorFinal.Text = valorConvertido.ToString();
            }
            else if (cb_Convertidor.SelectedIndex == 2)
            {
                valorConvertido = nuevoValorFinal * 39;
                tb_nuevoValorFinal.Text = valorConvertido.ToString();
            }
        }
    }
}
