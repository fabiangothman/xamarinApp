using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace SURA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CargandoPopUp : PopupPage
    {
        public CargandoPopUp()
        {
            InitializeComponent();
        }

        public CargandoPopUp(bool mensaje)
        {
            InitializeComponent();
            lblCarga.Text = "Estamos cargando tu solicitud";
        }

        public void ShowCerrar(TapGestureRecognizer tapGestureRecognizer)
        {
            btnCerrar.IsVisible = true;
            btnCerrar.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}