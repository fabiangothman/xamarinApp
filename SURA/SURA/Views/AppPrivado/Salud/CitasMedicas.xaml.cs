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
    public partial class CitasMedicas : ContentView
    {
        private List<string> list;

        public CitasMedicas()
        {
            InitializeComponent();
            /*list = new List<string>();
            list.Add("test 1");
            list.Add("test 2");
            list.Add("test 3");
            list.Add("test 4");*/
        }

        private void btnProximasCitas_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("ProximasCitas");
        }

        private void btnCitasAnteriores_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("CitasAnteriores");
        }

        private void btnListado_Tapped(object sender, EventArgs e)
        {
            Picker p = new Picker();
            //p.ItemsSource = list;
            this.documentTypePicker.Focus();
        }

        private void btnBuscarEspecializacion_Tapped(object sender, EventArgs e) {
            //
        }
    }
}