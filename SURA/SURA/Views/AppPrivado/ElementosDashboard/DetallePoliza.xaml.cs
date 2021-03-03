using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallePoliza : ContentView
    {
        string _NumeroPoliza;
        int _NumeroRiesgo;
        public DetallePoliza()
        {
            InitializeComponent();
            List<Models.MisPolizas> dataFiltrada = App.modeloDashboard.Polizas
                    .Where(m => m.Poliza == App.NumeroPoliza)
                    .ToList();

            List<Models.Riesgo> riesgoData = dataFiltrada.SelectMany(i => i.Riesgos).Where(riesgo => riesgo.NumeroRiesgo == _NumeroRiesgo).ToList();

            List<Models.Riesgo> riesgoFiltro = dataFiltrada.Where(riesgo => riesgo.Poliza == App.NumeroPoliza).SelectMany(i => i.Riesgos).ToList();
            List<int> NumeroRiesgo = riesgoFiltro.Select(i => i.NumeroRiesgo).ToList();
            pickerNumeroRiesgo.ItemsSource = NumeroRiesgo;
            pickerNumeroRiesgo.SelectedIndex = 0;

            CargarItems();
        }

        public DetallePoliza(string NumeroPoliza)
        {
            InitializeComponent();
            _NumeroPoliza = NumeroPoliza;

            List<Models.MisPolizas> dataFiltrada = App.modeloDashboard.Polizas
                    .Where(m => m.Poliza == NumeroPoliza)
                    .ToList();

            List<Models.Riesgo> riesgoData = dataFiltrada.SelectMany(i => i.Riesgos).Where(riesgo => riesgo.NumeroRiesgo == _NumeroRiesgo).ToList();

            List<Models.Riesgo> riesgoFiltro = dataFiltrada.Where(riesgo => riesgo.Poliza == NumeroPoliza).SelectMany(i => i.Riesgos).ToList();
            List<int> NumeroRiesgo = riesgoFiltro.Select(i => i.NumeroRiesgo).ToList();
            pickerNumeroRiesgo.ItemsSource = NumeroRiesgo;
            pickerNumeroRiesgo.SelectedIndex = 0;

            CargarItems();
        }

        private void BtnCoberturas_Clicked(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("Coberturas", _NumeroRiesgo.ToString());
        }

        private void PickerNumeroRiesgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pickerNumeroRiesgo.IsEnabled = false;
            var picker = (Picker)sender;
            int selectedItemm = Convert.ToInt32(picker.SelectedItem);
            _NumeroRiesgo = selectedItemm;
            if (selectedItemm > 0)
            {
                CargarItems();

            }

            pickerNumeroRiesgo.IsEnabled = true;
        }

        private void CargarItems()
        {
            try
            {
                if (Convert.ToInt32(_NumeroRiesgo) > 0)
                {
                    List<Models.MisPolizas> dataFiltrada = App.modeloDashboard.Polizas
                    .Where(m => m.Poliza == _NumeroPoliza)
                    .ToList();

                    List<Models.Riesgo> riesgoData = dataFiltrada.SelectMany(i => i.Riesgos).Where(riesgo => riesgo.NumeroRiesgo == _NumeroRiesgo).ToList();

                    List<Models.Riesgo> riesgoFiltro = dataFiltrada.Where(riesgo => riesgo.Poliza == _NumeroPoliza).SelectMany(i => i.Riesgos).ToList();
                    List<int> NumeroRiesgo = riesgoFiltro.Select(i => i.NumeroRiesgo).ToList();

                    if (Convert.ToInt32(_NumeroRiesgo) > 1)
                    {
                        iconChevron.IsVisible = true;
                    }

                    if (riesgoData[0].NumeroRiesgo != null)
                    {
                        numeroRiesgo.Text = riesgoData[0].NumeroRiesgo.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].NumeroRiesgo == null)
                        {
                            frameNumeroRiesgo.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].SumaAsegurada != null)
                    {
                        sumaAsegurada.Text = riesgoData[0].SumaAseguradaString.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].SumaAsegurada == null)
                        {
                            framesumaAsegurada.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Ano != null)
                    {
                        anio.Text = riesgoData[0].Ano.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Ano == null)
                        {
                            frameAnio.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Marca != null)
                    {
                        marca.Text = riesgoData[0].Marca.ToString();
                    }
                    else
                    {
                        framemarca.IsVisible = false;
                    }

                    if (riesgoData[0].Color != null)
                    {
                        color.Text = riesgoData[0].Color;
                    }
                    else
                    {
                        if (riesgoData[0].Color == null)
                        {
                            framecolor.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Motor != null)
                    {
                        numeroMotor.Text = riesgoData[0].Motor;
                    }
                    else
                    {
                        if (riesgoData[0].Motor == null)
                        {
                            framenumeroMotor.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Chasis != null)
                    {
                        chasis.Text = riesgoData[0].Chasis;
                    }
                    else
                    {
                        if (riesgoData[0].Chasis == null)
                        {
                            framechasis.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Actividad != null)
                    {
                        actividad.Text = riesgoData[0].Actividad.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Actividad == null)
                        {
                            frameactividad.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Placa != null)
                    {
                        placa.Text = riesgoData[0].Placa.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Placa == null)
                        {
                            frameplaca.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Provincia != null)
                    {
                        provincia.Text = riesgoData[0].Provincia.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Provincia == null)
                        {
                            frameProvincia.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Distrito != null)
                    {
                        distrito.Text = riesgoData[0].Distrito.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Distrito == null)
                        {
                            frameDistrito.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Corregimiento != null)
                    {
                        corregimiento.Text = riesgoData[0].Corregimiento.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Corregimiento == null)
                        {
                            frameCorregimiento.IsVisible = false;
                        }
                    }

                    if (riesgoData[0].Manzana != null)
                    {
                        manzana.Text = riesgoData[0].Manzana.ToString();
                    }
                    else
                    {
                        if (riesgoData[0].Manzana == null)
                        {
                            frameManzana.IsVisible = false;
                        }
                    }

                    App.CoberturaList = riesgoData[0].Coberturas;
                    _NumeroRiesgo = Convert.ToInt32(numeroRiesgo.Text);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}