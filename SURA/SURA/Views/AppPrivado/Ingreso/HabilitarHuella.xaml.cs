using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Ingreso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabilitarHuella : ContentView
    {
        Services.ServicioPreferencias servicioPreferencias = new Services.ServicioPreferencias();
        public HabilitarHuella()
        {
            InitializeComponent();
        }

        private void BtnHabilitarHuella_Clicked(object sender, EventArgs e)
        {
            servicioPreferencias.HabilitarBiometrico();
            AbrirDashboard();
        }

        private void BtnAhoraNo_Clicked(object sender, EventArgs e)
        {
            AbrirDashboard();
        }

        private void AbrirDashboard()
        {
            App.blnPrivado = true;
            App.objContenedor.MostrarVista("Dashboard");
        }
    }
}