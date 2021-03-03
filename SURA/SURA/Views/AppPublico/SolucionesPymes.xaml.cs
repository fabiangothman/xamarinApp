using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolucionesPymes : ContentView
    {
        public SolucionesPymes()
        {
            InitializeComponent();
        }

        void EnviarPagina(string Detalle)
        {
            try
            {
                App.objContenedor.MostrarVista("DetallesContenedor", Detalle);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "EnviarPagina: " + ex.Message, "Ok");
            }
        }

        private void frmEsencial_Tapped(object sender, EventArgs e)
        {
            EnviarPagina("Negocio Protegido");
        }

        //private void frmIntegral_Tapped(object sender, EventArgs e)
        //{
        //    EnviarPagina("Negocio Protegido Integral");
        //}

        //private void frmPremium_Tapped(object sender, EventArgs e)
        //{
        //    EnviarPagina("Negocio Protegido Premium");
        //}
    }
}