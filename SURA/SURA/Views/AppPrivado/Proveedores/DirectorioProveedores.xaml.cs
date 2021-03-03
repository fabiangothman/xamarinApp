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
    public partial class DirectorioProveedores : ContentView
    {
        public DirectorioProveedores()
        {
            InitializeComponent();
        }

        private void btnProfesionalesSalud_Clicked(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("ProfesionalesSalud");
        }
    }
}