using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico.AsistenciaVial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciaVial3 : ContentView
    {
        string Data = "";
        public AsistenciaVial3()
        {
            InitializeComponent();
            CargarVista();
        }

        private void CargarVista()
        {
            try
            {
                if (App.blnPrivado)
                {
                    entryPrimerNombre.IsReadOnly = true;
                    entryApellido.IsReadOnly = true;
                    entryPlacaVehiculo.IsReadOnly = true;

                    entryPrimerNombre.TextColor = Color.FromHex("#808080") ;
                    entryApellido.TextColor = Color.FromHex("#808080");
                    entryPlacaVehiculo.TextColor = Color.FromHex("#808080");

                    string Marca_Modelo = App.modeloDashboard.Polizas[0].Riesgos[0].Marca;
                    entryPrimerNombre.Text = App.modeloDashboard.Nombre;
                    entryApellido.Text = App.modeloDashboard.Apellido;
                    entryPlacaVehiculo.Text = App.modeloDashboard.Polizas[0].Riesgos[0].Placa.ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                if (App.blnPrivado)
                {
                    if (string.IsNullOrWhiteSpace(entryPrimerNombre.Text) || string.IsNullOrWhiteSpace(entryApellido.Text) || string.IsNullOrWhiteSpace(entryNumeroTelefonico.Text) || string.IsNullOrWhiteSpace(entryPlacaVehiculo.Text))
                    {
                        await App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                    }
                    else
                    {
                        Models.ConnectRegisteredUser connectRegisteredUser = new Models.ConnectRegisteredUser()
                        {
                            AccountId = "0011Y00002noEzWQAU",
                            ServiceList = new Models.Service()
                            {
                                Situation = App.connectContactModel.TipoServicio,
                                ExternalList = new Models.External()
                                {
                                    IdString = "222"
                                },
                                ClientList = new Models.Client()
                                {
                                    Firstname = entryPrimerNombre.Text,
                                    Lastname = entryApellido.Text,
                                    Phone1 = entryNumeroTelefonico.Text,
                                    //Email = entryCorreoElectronico.Text
                                },
                                VehicleList = new Models.Vehicle()
                                {
                                    //Year = entryCarYear.Text,
                                    //Make = entryMarcaVehiculo.Text,
                                    //Model = entryModeloVehiculo.Text,
                                    Plate = entryPlacaVehiculo.Text,
                                    //Color = entryColorVehiculo.Text,
                                    //Vin = entryNumeroVinVehiculo.Text
                                },
                                RouteList = new Models.Route()
                                {
                                    Situation = new Models.Situation()
                                    {
                                        Address = App.AsistenciaVialCoordenadas.address,
                                        Latitude = App.AsistenciaVialCoordenadas.latitude.ToString(),
                                        Longitude = App.AsistenciaVialCoordenadas.longitude.ToString()
                                    }
                                }
                            }
                        };

                        var settings = new Newtonsoft.Json.JsonSerializerSettings
                        {
                            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                        };
                        settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(connectRegisteredUser, Formatting.Indented, settings);
                        Data = json;
                        //await App.Current.MainPage.DisplayAlert("Prueba",json,"Ok");
                        Services.AsistenciaVialService asistencia = new Services.AsistenciaVialService();
                        asistencia.AsistenciaVial(Data);
                        LimpiarFormulario();
                    }
                }
                else
                {

                    if (string.IsNullOrWhiteSpace(entryNumeroTelefonico.Text))
                    {
                        await App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                    }
                    else
                    {

                        Models.ConnectUnregisteredUser connectUnregisteredUser = new Models.ConnectUnregisteredUser()
                        {
                            account_id = "0011Y00002noEzWQAU",
                            service = new Models.ServiceU()
                            {
                                situation = App.connectContactModel.TipoServicio,
                                client = new Models.ClientU()
                                {
                                    firstname = entryPrimerNombre.Text,
                                    lastname = entryApellido.Text,
                                    phone1 = entryNumeroTelefonico.Text,
                                    //email = entryCorreoElectronico.Text
                                },
                                vehicle = new Models.VehicleU()
                                {
                                    //year = entryCarYear.Text,
                                    //make = entryMarcaVehiculo.Text,
                                    //model = entryModeloVehiculo.Text,
                                    plate = entryPlacaVehiculo.Text,
                                    //color = entryColorVehiculo.Text,
                                    //vin = entryNumeroVinVehiculo.Text
                                },
                                route = new Models.RouteU()
                                {
                                    situation = new Models.SituationU()
                                    {
                                        address = App.AsistenciaVialCoordenadas.address,
                                        latitude = App.AsistenciaVialCoordenadas.latitude.ToString(),
                                        longitude = App.AsistenciaVialCoordenadas.longitude.ToString()
                                    }
                                }
                            }
                        };

                        var settings = new JsonSerializerSettings
                        {
                            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                        };
                        settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

                        var json = JsonConvert.SerializeObject(connectUnregisteredUser, Formatting.Indented, settings);
                        Data = json;

                        Services.AsistenciaVialService asistencia = new Services.AsistenciaVialService();
                        asistencia.AsistenciaVial(Data);
                        LimpiarFormulario();
                    }
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA","Asistencia vial 3: "+ex.Message,"Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private void LimpiarFormulario()
        {
            entryPrimerNombre.Text = string.Empty;
            entryApellido.Text = string.Empty;
            entryNumeroTelefonico.Text = string.Empty;
            entryPlacaVehiculo.Text = string.Empty;
        }

    }
}