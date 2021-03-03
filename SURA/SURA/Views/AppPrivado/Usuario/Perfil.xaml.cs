using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentView
    {
        public Perfil()
        {
            InitializeComponent();
        }

        private void btnCambiarClave_Tapped(object sender, EventArgs e)
        {
            //App.objContenedor.MostrarVista("Contrasena");
            App.objContenedor.MostrarVista("RecuperarContra");
        }

        private void btnEditarPerfil_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("EditarPerfil");
        }
    }
}