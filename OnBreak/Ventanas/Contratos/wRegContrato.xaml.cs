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

namespace OnBreak.Ventanas.Contratos
{
    /// <summary>
    /// Lógica de interacción para wRegContrato.xaml
    /// </summary>
    public partial class wRegContrato : MetroWindow
    {
        #region Declaracion de Variables privadas

        private DateTime? fechainicioEvento = DateTime.Now;
        private bool comidaVeg = false;
        private bool ambientacionCocktail = false;
        private int v_idAmbientacion = 10;
        private bool musicaAmbienteCocktail = false;
        private bool musicaClienteCocktail = true;
        private bool musicaAmbienteCenas = false;
        private int v_valorArriendo = 0;
        private bool b_localOnbreak = true;
        private bool b_otrolocalOnBreak = false;

        private int ambientacionCenas = 0;
        private int _valorBase = 0;

        private double _valorFinal = 0;

        private bool _validacionCampos = false;
        #endregion

        #region Declaracion de Variables Publicas

        public string rutClienteListado = string.Empty;

        #endregion

        public wRegContrato()
        {
            InitializeComponent();

            tb_Asistentes.SetValue(TextBoxHelper.WatermarkProperty, "Asistentes");
            tb_Personal.SetValue(TextBoxHelper.WatermarkProperty, "Personal");
            tb_valorBase.SetValue(TextBoxHelper.WatermarkProperty, "Valor Base");
            tb_valorFinal.SetValue(TextBoxHelper.WatermarkProperty, "Valor Final");
            btn_Aceptar.IsEnabled = false;
            

            LimpiarControles();
        }


        #region Botones Barra de Titulo
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            VentanaContratos vcli = new VentanaContratos();
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

        private void Btn_CotizarCont_Click(object sender, RoutedEventArgs e)
        {
            fo_cotizarContrato.IsOpen = true;
        }
        #endregion

        #region Cargar ComboBox
        /*
        private void CargarModalidad()
        {
            ModalidadServicio modalidad = new ModalidadServicio();
            cb_Modalidad.ItemsSource = modalidad.ReadAll();

            cb_Modalidad.DisplayMemberPath = "Nombre";
            cb_Modalidad.SelectedValuePath = "IdModalidad";

            cb_Modalidad.SelectedIndex = -1;
        } */
        
        private void CargarTipoEvento()
        {
            TipoEvento tipo_evento = new TipoEvento();
            cb_TipoEvento.ItemsSource = tipo_evento.ReadAll();

            cb_TipoEvento.DisplayMemberPath = "Descripcion";
            cb_TipoEvento.SelectedValuePath = "IdTipoEvento";

            cb_TipoEvento.SelectedIndex = -1;
        }

        private void CargarTipoAmbientacion()
        {
            TipoAmbientacion tipoAmbienta = new TipoAmbientacion();
            cb_AmbientacionCocktail.ItemsSource = tipoAmbienta.ReadAll();
            cb_AmbientacionCocktailCenas.ItemsSource = tipoAmbienta.ReadAll();

            cb_AmbientacionCocktail.DisplayMemberPath = "Descripcion";
            cb_AmbientacionCocktail.SelectedValuePath = "IdTipoAmbientacion";

            cb_AmbientacionCocktailCenas.DisplayMemberPath = "Descripcion";
            cb_AmbientacionCocktailCenas.SelectedValuePath = "IdTipoAmbientacion";

            cb_AmbientacionCocktail.SelectedIndex = -1;
            cb_AmbientacionCocktailCenas.SelectedIndex = -1;
        }
        #endregion

        #region Botones Flyout
        private void Btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            fo_cotizarContrato.IsOpen = false;
        }

        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            fo_cotizarContrato.IsOpen = false;
            btn_RegistrarCont.IsEnabled = true;
        }
        #endregion

        #region Acciones de WPF
        private void ch_Comida_Checked(object sender, RoutedEventArgs e)
        {
            comidaVeg = true;
        }

        private void ch_AmbientacionCocktail_Checked(object sender, RoutedEventArgs e)
        {
            lb_TipoAmbientacion.Visibility = Visibility.Visible;
            cb_AmbientacionCocktail.Visibility = Visibility.Visible;
            ambientacionCocktail = true;
            v_idAmbientacion = 20;
        }

        private void Cb_AmbientacionCocktailCenas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_AmbientacionCocktailCenas.SelectedValue != null)
            {
                string y = cb_AmbientacionCocktailCenas.SelectedValue.ToString();
                if (int.TryParse(y, out int idAmb))
                {
                    if (idAmb == 10)
                    {
                        ambientacionCenas = 10;
                    }
                    else if (idAmb == 20)
                    {
                        ambientacionCenas = 20;
                    }
                }
            }
        }

        private void cb_TipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModalidadServicio modalidad = new ModalidadServicio();

            if (cb_TipoEvento.SelectedValue != null)
            {
                string x = cb_TipoEvento.SelectedValue.ToString();
                if (int.TryParse(x, out int opcion))
                {
                    if (opcion == 10)
                    {
                        ch_AmbientacionCocktail.Visibility = Visibility.Hidden;
                        ch_MusicaCocktail.Visibility = Visibility.Hidden;
                        lb_TipoAmbientacion.Visibility = Visibility.Hidden;
                        cb_AmbientacionCocktail.Visibility = Visibility.Hidden;

                        ch_MusicaCocktailCenas.Visibility = Visibility.Hidden;
                        lb_TipoAmbientacionCenas.Visibility = Visibility.Hidden;
                        cb_AmbientacionCocktailCenas.Visibility = Visibility.Hidden;
                        rb_LocalOnBreak.Visibility = Visibility.Hidden;
                        rb_LocalConvenirOnBreak.Visibility = Visibility.Hidden;
                        rb_LocalConvenirCliente.Visibility = Visibility.Hidden;

                        ch_Comida.Visibility = Visibility.Visible;

                        tb_ValorArriendoLocal.Visibility = Visibility.Hidden;
                        lb_ValorArriendoLocalCenas.Visibility = Visibility.Hidden;

                        // Aquí se cargan solo las modalidades de Coffee Break
                        cb_Modalidad.ItemsSource = modalidad.ReadOnlyByTipoEvento(opcion);

                        cb_Modalidad.DisplayMemberPath = "Nombre";
                        cb_Modalidad.SelectedValuePath = "IdModalidad";

                        cb_Modalidad.SelectedIndex = -1;
                    }
                    else if (opcion == 20)
                    {
                        ch_Comida.Visibility = Visibility.Hidden;

                        ch_MusicaCocktailCenas.Visibility = Visibility.Hidden;
                        lb_TipoAmbientacionCenas.Visibility = Visibility.Hidden;
                        cb_AmbientacionCocktailCenas.Visibility = Visibility.Hidden;
                        rb_LocalOnBreak.Visibility = Visibility.Hidden;
                        rb_LocalConvenirOnBreak.Visibility = Visibility.Hidden;
                        rb_LocalConvenirCliente.Visibility = Visibility.Hidden;

                        ch_AmbientacionCocktail.Visibility = Visibility.Visible;
                        ch_MusicaCocktail.Visibility = Visibility.Visible;
                        lb_TipoAmbientacion.Visibility = Visibility.Visible;
                        cb_AmbientacionCocktail.Visibility = Visibility.Visible;

                        tb_ValorArriendoLocal.Visibility = Visibility.Hidden;
                        lb_ValorArriendoLocalCenas.Visibility = Visibility.Hidden;

                        // Aquí se cargan solo las modalidades de Cocktail
                        cb_Modalidad.ItemsSource = modalidad.ReadOnlyByTipoEvento(opcion);

                        cb_Modalidad.DisplayMemberPath = "Nombre";
                        cb_Modalidad.SelectedValuePath = "IdModalidad";

                        cb_Modalidad.SelectedIndex = -1;
                    }
                    else if (opcion == 30)
                    {
                        ch_Comida.Visibility = Visibility.Hidden;

                        ch_AmbientacionCocktail.Visibility = Visibility.Hidden;
                        ch_MusicaCocktail.Visibility = Visibility.Hidden;
                        lb_TipoAmbientacion.Visibility = Visibility.Hidden;
                        cb_AmbientacionCocktail.Visibility = Visibility.Hidden;

                        ch_MusicaCocktailCenas.Visibility = Visibility.Visible;
                        lb_TipoAmbientacionCenas.Visibility = Visibility.Visible;
                        cb_AmbientacionCocktailCenas.Visibility = Visibility.Visible;
                        rb_LocalOnBreak.Visibility = Visibility.Visible;
                        rb_LocalConvenirOnBreak.Visibility = Visibility.Visible;
                        rb_LocalConvenirCliente.Visibility = Visibility.Visible;

                        tb_ValorArriendoLocal.Visibility = Visibility.Visible;
                        lb_ValorArriendoLocalCenas.Visibility = Visibility.Visible;

                        // Aquí se cargan solo las modalidades de Cena
                        cb_Modalidad.ItemsSource = modalidad.ReadOnlyByTipoEvento(opcion);

                        cb_Modalidad.DisplayMemberPath = "Nombre";
                        cb_Modalidad.SelectedValuePath = "IdModalidad";

                        cb_Modalidad.SelectedIndex = -1;
                    }
                }

            }
        }

        private void Ch_MusicaCocktailCenas_Checked(object sender, RoutedEventArgs e)
        {
            musicaAmbienteCenas = true;
        }

        private void cb_Modalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModalidadServicio modalidad = new ModalidadServicio();

            if (cb_Modalidad.SelectedValue != null)
            {
                string z = (string)cb_Modalidad.SelectedValue;
                _valorBase = (int)(modalidad.RetornaValorBase(z));
                tb_valorBase.Text = _valorBase.ToString();
                tb_ValorBaseNoFlyout.Text = _valorBase.ToString();

                /*
                if (z == "CB001")
                {
                    _valorBase = (int)(modalidad.RetornaValorBase((string)cb_Modalidad.SelectedValue));
                    //_valorBase = 3;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CB002")
                {
                    _valorBase = 8;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CB003")
                {
                    _valorBase = 12;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CE001")
                {
                    _valorBase = 25;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CE002")
                {
                    _valorBase = 35;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CO001")
                {
                    _valorBase = 6;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                else if (z == "CO002")
                {
                    _valorBase = 10;
                    tb_valorBase.Text = _valorBase.ToString();
                    tb_ValorBaseNoFlyout.Text = _valorBase.ToString();
                }
                */
            }
        }

        private void ch_MusicaCocktail_Checked(object sender, RoutedEventArgs e)
        {
            musicaAmbienteCocktail = true;
            musicaClienteCocktail = false;
        }

        private void rb_LocalConvenirOnBreak_Checked(object sender, RoutedEventArgs e)
        {
            tb_ValorArriendoLocal.IsEnabled = true;
        }

        private void btn_BuscarCli_Click(object sender, RoutedEventArgs e)
        {
            ListaClientesParaContrato listaClientesSeleccionar = new ListaClientesParaContrato(this);
            listaClientesSeleccionar.Show();
        }
        #endregion

        #region Calculos
        private void Arriendo()
        {
            if (rb_LocalConvenirOnBreak.IsChecked == true)
            {
                tb_ValorArriendoLocal.IsEnabled = true;
                b_localOnbreak = false;
                b_otrolocalOnBreak = true;
                v_valorArriendo = Int32.Parse(tb_ValorArriendoLocal.Text);
            }
            else if (rb_LocalConvenirCliente.IsChecked == true)
            {
                v_valorArriendo = 0;
                b_localOnbreak = false;
                b_otrolocalOnBreak = true;
                tb_ValorArriendoLocal.IsEnabled = false;
            }
            else if (rb_LocalOnBreak.IsChecked == true)
            {
                v_valorArriendo = 0;
                b_localOnbreak = true;
                b_otrolocalOnBreak = false;
                tb_ValorArriendoLocal.IsEnabled = false;
            }
        }

        public double RealizarCalculoFinal(int nAsistentes, int nPersonalAdicional, int nValorArriendo, int nValorBase)
        {
            double recargoAsistentes = 0;
            double recargoPersonal = 0;
            double recargoAmbientacion = 0;
            double recargoMusicaAmb = 0;
            double recargoLocal = 0;
            if (cb_TipoEvento.SelectedValue != null)
            {
                if((int)cb_TipoEvento.SelectedValue == 10) // Coffee Break
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
                        recargoAsistentes= (nAsistentes/20)*2;
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
                    else if(nPersonalAdicional == 3)
                    {
                        recargoPersonal = 3;
                    }
                    else if(nPersonalAdicional == 4)
                    {
                        recargoPersonal = 4;
                    }
                    else if(nPersonalAdicional > 4)
                    {
                        recargoPersonal = 3.5 + (nPersonalAdicional * 0.5);
                    }
                    else
                    {
                        recargoPersonal = 0;
                    }
                }
                else if((int)cb_TipoEvento.SelectedValue == 20) // Cocktail
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
                    if (cb_AmbientacionCocktail.SelectedValue != null)
                    {
                        if ((int)cb_AmbientacionCocktail.SelectedValue == 10)
                        {
                            recargoAmbientacion = 2;
                        }
                        else if ((int)cb_AmbientacionCocktail.SelectedValue == 20)
                        {
                            recargoAmbientacion = 5;
                        }
                    }
                    else
                    {
                        recargoAmbientacion = 0;
                    }
                    // Recargo Ambientación Música
                    if (ch_MusicaCocktail.IsChecked == true)
                    {
                        recargoMusicaAmb = 1;
                    }
                    else
                    {
                        recargoMusicaAmb = 0;
                    }
                }
                else if ((int)cb_TipoEvento.SelectedValue == 30) // Cenas
                {
                    // Recargo Asistentes
                    if (nAsistentes > 0 && nAsistentes < 21)
                    {
                        recargoAsistentes = nAsistentes*1.5;
                    }
                    else if (nAsistentes > 20 && nAsistentes < 51)
                    {
                        recargoAsistentes = nAsistentes*1.2;
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
                    if (cb_AmbientacionCocktailCenas.SelectedValue != null)
                    {
                        if ((int)cb_AmbientacionCocktailCenas.SelectedValue == 10)
                        {
                            recargoAmbientacion = 3;
                        }
                        else if ((int)cb_AmbientacionCocktailCenas.SelectedValue == 20)
                        {
                            recargoAmbientacion = 5;
                        }
                    }
                    else
                    {
                        recargoAmbientacion = 0;
                    }
                    // Música Ambiental
                    if (ch_MusicaCocktailCenas.IsChecked == true)
                    {
                        recargoMusicaAmb = 1.5;
                    }
                    else
                    {
                        recargoMusicaAmb = 0;
                    }
                    // Local
                    if (b_localOnbreak == true)
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

        #region Traer Rut

        public void SetRutToTextBox(string rut)
        {
            tb_rutCliente.Text = rut;
        }

        #endregion

        #region Validación Fecha de Creación de Contrato

        private bool FechaInicioValidacion(DateTime? _fechaInicio, DateTime? _fechaTermino, DateTime? _creacion)
        {
            if (_fechaInicio == null)
            {
                return false;
                
            }
            if (_fechaTermino < _fechaInicio)
            {
                return false;
                
            }
            if (_fechaInicio < _creacion)
            {
                return false;
                
            }
            return true;
        }

        #endregion

        #region Creacion de Contrato
        private async void btn_RegistrarCont_Click(object sender, RoutedEventArgs e)
        {
            if (FechaInicioValidacion(dt_FechaHoraInicio.SelectedDateTime, dt_FechaHoraTermino.SelectedDateTime, DateTime.Now) == false)
            {
                _validacionCampos = false;
                await this.ShowMessageAsync("Error - Crear Contrato", $"La fecha de inicio de evento ingresada no es válida {Environment.NewLine} Por favor, vuelva a intentarlo");
            }
            else
            {
                fechainicioEvento = dt_FechaHoraInicio.SelectedDateTime;
                if (string.IsNullOrEmpty(tb_rutCliente.Text) || string.IsNullOrWhiteSpace(tb_rutCliente.Text))
                {
                    _validacionCampos = false;
                    await this.ShowMessageAsync("Error - Crear Contrato", $"El campo RUT Cliente no ha sido rellenado. {Environment.NewLine} Por favor, vuelva a intentarlo");
                }
                else
                {
                    if(string.IsNullOrEmpty(tb_Observaciones.Text) || string.IsNullOrWhiteSpace(tb_Observaciones.Text))
                    {
                        _validacionCampos = false;
                        await this.ShowMessageAsync("Error - Crear Contrato", $"El campo Observaciones no ha sido rellenado. {Environment.NewLine} Por favor, vuelva a intentarlo");
                    }
                    else
                    {
                        if(string.IsNullOrEmpty(tb_Asistentes.Text) || string.IsNullOrWhiteSpace(tb_Asistentes.Text))
                        {
                            _validacionCampos = false;
                            await this.ShowMessageAsync("Error - Crear Contrato", $"El campo Asistentes no ha sido rellenado. {Environment.NewLine} Por favor, vuelva a intentarlo");
                        }
                        else
                        {
                            if(string.IsNullOrEmpty(tb_Personal.Text) || string.IsNullOrWhiteSpace(tb_Personal.Text))
                            {
                                _validacionCampos = false;
                                await this.ShowMessageAsync("Error - Crear Contrato", $"El campo Personal Adicional no ha sido rellenado. {Environment.NewLine} Por favor, vuelva a intentarlo");
                            }
                            else
                            {
                                _validacionCampos = true;
                            }
                        }
                    }
                }
            }
            Contrato contrato = new Contrato()
            {
                Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                Creacion = DateTime.Now,
                Termino = new DateTime(2050, 1, 1),
                RutCliente = tb_rutCliente.Text,
                IdModalidad = (string)cb_Modalidad.SelectedValue,
                IdTipoEvento = (int)cb_TipoEvento.SelectedValue,
                FechaHoraInicio = fechainicioEvento,
                FechaHoraTermino = dt_FechaHoraTermino.SelectedDateTime,
                Asistentes = int.Parse(tb_Asistentes.Text),
                PersonalAdicional = int.Parse(tb_Personal.Text),
                Realizado = false,
                ValorTotalContrato = _valorFinal,
                Observaciones = tb_Observaciones.Text,
            };

            CoffeeBreak coffeebreak = new CoffeeBreak()
            {
                Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                Vegetariana = comidaVeg
            };

            Cocktail cocktail = new Cocktail()
            {
                Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                Ambientacion = ambientacionCocktail,
                IdTipoAmbientacion = v_idAmbientacion,
                MusicaAmbiental = musicaAmbienteCocktail,
                MusicaCliente = musicaClienteCocktail,
            };

            Arriendo();

            Cenas cenas = new Cenas()
            {
                Numero = DateTime.Now.ToString("yyyyMMddHHmm"),
                IdTipoAmbientacion = ambientacionCenas,
                MusicaAmbiental = musicaAmbienteCenas,
                LocalOnBreak = b_localOnbreak,
                OtroLocalOnBreak = b_otrolocalOnBreak,
                ValorArriendo = (double)v_valorArriendo
            };

            if (_validacionCampos == true)
            {
                if ((int)cb_TipoEvento.SelectedValue == 10)
                {
                    if (contrato.Create() && coffeebreak.Create())
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Contrato registrado exitosamente");
                        LimpiarControles();
                        LimpiarCampoRut();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Error al registrar contrato Coffee Break.");
                    }
                }
                else if ((int)cb_TipoEvento.SelectedValue == 20)
                {
                    if (contrato.Create() && cocktail.Create())
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Contrato registrado exitosamente");
                        LimpiarControles();
                        LimpiarCampoRut();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Error al registrar contrato Cocktail.");
                    }
                }
                else if ((int)cb_TipoEvento.SelectedValue == 30)
                {
                    if (contrato.Create() && cenas.Create())
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Contrato registrado exitosamente");
                        LimpiarControles();
                        LimpiarCampoRut();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Crear Contrato", "Error al registrar contrato Cenas.");
                    }
                }
            }
            else
            {
                await this.ShowMessageAsync("Crear Contrato", "Faltan campos por rellenar.");
            }
        }
        #endregion

        #region Otros Metodos
        private void LimpiarControles()
        {
            /* Limpia los controles de texto */
            tb_NumeroContrato.Text = string.Empty;
            
            tb_Asistentes.Text = string.Empty;
            tb_Personal.Text = string.Empty;
            //CargarModalidad();
            CargarTipoEvento();
            CargarTipoAmbientacion();
            cb_Modalidad.SelectedIndex = -1;

            tb_Observaciones.Text = string.Empty;

            ch_Comida.Visibility = Visibility.Hidden;



            ch_AmbientacionCocktail.Visibility = Visibility.Hidden;
            ch_MusicaCocktailCenas.Visibility = Visibility.Hidden;
            lb_TipoAmbientacionCenas.Visibility = Visibility.Hidden;
            ch_MusicaCocktail.Visibility = Visibility.Hidden;
            lb_TipoAmbientacion.Visibility = Visibility.Hidden;
            cb_AmbientacionCocktail.Visibility = Visibility.Hidden;

            cb_AmbientacionCocktailCenas.Visibility = Visibility.Hidden;
            rb_LocalOnBreak.Visibility = Visibility.Hidden;
            rb_LocalConvenirOnBreak.Visibility = Visibility.Hidden;
            rb_LocalConvenirCliente.Visibility = Visibility.Hidden;

            lb_TipoAmbientacion.Visibility = Visibility.Hidden;
            cb_AmbientacionCocktail.Visibility = Visibility.Hidden;

            btn_RegistrarCont.IsEnabled = false;

            tb_ValorArriendoLocal.Visibility = Visibility.Hidden;
            tb_ValorArriendoLocal.IsEnabled = false;

            lb_ValorArriendoLocalCenas.Visibility = Visibility.Hidden;

            tb_valorBase.Text = string.Empty;
            tb_valorFinal.Text = string.Empty;

            tb_ValorBaseNoFlyout.Text = string.Empty;
            tb_ValorFinalNoFlyout.Text = string.Empty;
        }

        private void LimpiarCampoRut()
        {
            tb_rutCliente.Text = string.Empty;
        }

        #endregion

        private void btn_calcularFinal_Click(object sender, RoutedEventArgs e)
        {
            btn_Cancelar.IsEnabled = true;
            int asistentes = int.Parse(tb_Asistentes.Text);
            int personal = int.Parse(tb_Personal.Text);
            int valorarriendo = 0;
            if (tb_ValorArriendoLocal.Text == string.Empty)
            {
                valorarriendo = 0;
            }
            else
            {
                valorarriendo = int.Parse(tb_ValorArriendoLocal.Text);
            } 
            int valorbase = int.Parse(tb_ValorBaseNoFlyout.Text);
            _valorFinal = RealizarCalculoFinal(asistentes,personal,valorarriendo,valorbase);
            tb_valorFinal.Text = _valorFinal.ToString();
            tb_ValorFinalNoFlyout.Text = _valorFinal.ToString() + " UF";
        }

        private void btn_Convertir_Click(object sender, RoutedEventArgs e)
        {
            tb_ValorFinalNoFlyout.Text = (_valorFinal * 32767).ToString() + " CLP";
        }
    }




}
