using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico.MiPago
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPagoPublic : ContentView
    {
        private List<string> list;

        public MiPagoPublic()
        {
            InitializeComponent();
            CargarItems();
        }

        private void CargarItems()
        {
            list = new List<string>();

            list.Add("test 1");
            list.Add("test 2");
            list.Add("test 3");
            list.Add("test 4");
        }

        private void btnBuscar_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("MiPagoResultados");
        }

        private void btnSoporte_Tapped(object sender, EventArgs e)
        {
            //
        }

        private void btnListado_Tapped(object sender, EventArgs e)
        {
            Picker p = new Picker();
            p.ItemsSource = list;
            this.documentTypePicker.Focus();
        }
    }
}