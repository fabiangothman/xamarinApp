using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfesionalesSalud : ContentView
    {
        public ProfesionalesSalud()
        {
            InitializeComponent();
        }
        
        private void btnIniciarConsulta_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("ResultadosProfesionalesSalud");
        }
    }
}