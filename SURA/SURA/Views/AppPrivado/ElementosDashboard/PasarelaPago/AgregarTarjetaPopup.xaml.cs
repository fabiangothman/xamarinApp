using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SURA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarTarjetaPopup : PopupPage
    {
        public AgregarTarjetaPopup()
        {
            InitializeComponent();
            var yearList = new List<string>();
            for (var i = 0; i<10; i++)
            {
                yearList.Add((DateTime.Now.Year + i).ToString().Substring(2));
            }
            pickerAno.ItemsSource = yearList;
        }

        void Agregar_Tapped(System.Object sender, System.EventArgs e)
        {
            var errorText = "";
            var datosTarjeta = new Dictionary<string, string>() {
                { "numeroTarjeta", cardNumero.Text},
                { "tarjetaHabiente", cardNombre.Text},
                { "fechaExpiracion", $"{pickerMes.SelectedItem}{pickerAno.SelectedItem}"},
                { "cvv", cardCVV.Text},
            };

            foreach (KeyValuePair<string, string> entry in datosTarjeta)
            {
                if(entry.Value.Length == 0)
                {
                    errorText = "Procure llenar todos los campos";
                    break;
                }


                if (entry.Key == "numeroTarjeta")
                {
                    if(entry.Value.Length < 13){
                        errorText = "El número de la tarjeta debe tener entre 13 y 18 dígitos";
                        break;
                    } else if(entry.Value.Length > 18)
                    {
                        errorText = "El número de la tarjeta debe tener entre 13 y 18 dígitos";
                        break;
                    }
                    
                } else if(entry.Key == "cvv" && entry.Value.Length < 3)
                {
                    errorText = "El código CVV debe tener de 3 a 4 dígitos";
                    break;
                }
            }

            if(pickerMes.SelectedItem == null || pickerAno.SelectedItem == null)
            {
                errorText = "Revise los campos de fecha de caducidad";
            }


            if (!string.IsNullOrEmpty(errorText))
            {
                cardError.Text = errorText;
                return;
            }
            cardError.Text = "";

            _ = RegistroTarjeta(datosTarjeta);
        }

        public async Task RegistroTarjeta(Dictionary<string, string> parameters)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);

                await new ServicioTarjetas().CrearTokenTarjetas(parameters, ChkGuardar.IsChecked);
                _=PopupNavigation.Instance.PopAsync();
            }
            catch(Exception e)
            {
                _ = App.Current.MainPage.DisplayAlert("SURA", "Error: " + e.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
            
        }

        void Cancelar_Tapped(System.Object sender, System.EventArgs e) => PopupNavigation.Instance.PopAsync();
    }
}