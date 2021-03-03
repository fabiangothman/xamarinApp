using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Salud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiSalud : ContentView
    {
        public MiSalud()
        {
            InitializeComponent();
            CargarItems();
        }

        private void CargarItems()
        {
            //lblEiqueta.IsVisible = true;
        }

        private void btnCitasMedicasIngresar_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("CitasMedicas");
        }

        private void btnConsultaVirtualIngresar_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("ConsultaVirtual");
        }

    }
}