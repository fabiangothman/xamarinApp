using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cobertura : ContentView
    {
        public Cobertura()
        {
            InitializeComponent();
            CargarItems();

            coberturaListView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                Task.Delay(500);

                // Deselect the item.
                if (sender is ListView lv) lv.SelectedItem = null;

                // Do something with the selection.
            };
        }

        private void CargarItems()
        {
            coberturaListView.ItemsSource = App.CoberturaList;
        }
    }
}