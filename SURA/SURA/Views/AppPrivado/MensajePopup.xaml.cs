using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MensajePopup : PopupPage
    {
        Services.ServicioPreferencias servicioPreferencias = new Services.ServicioPreferencias();
        public MensajePopup()
        {
            InitializeComponent();

        }

        private async void equis_Tapped(object sender, EventArgs e)
        {
            if (cbMostrar.IsChecked)
            {
                servicioPreferencias.HabilitarMensaje(true);
            }

            await PopupNavigation.Instance.PopAsync();
        }

        private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            int posicionActual = e.CurrentPosition;
            gridSwipe.IsVisible = false;
            if (posicionActual == 1)
            {
                gridAbajo.IsVisible = true;
            }
        }
    }
}