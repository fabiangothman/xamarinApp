using SURA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notificaciones : ContentView
    {
        public static ObservableCollection<ModeloNotificaciones> dataModel = new ObservableCollection<ModeloNotificaciones>();
        public Notificaciones()
        {
            InitializeComponent();
            CargarItems();
        }

        private void CargarItems()
        {
            try
            {

                List<ModeloNotificaciones> notificationsData = App.Database.GetNotifications();
                if (notificationsData.Count > 0)
                {
                    foreach (ModeloNotificaciones item in App.Database.GetNotifications())
                    {
                        dataModel.Add(item);
                    }
                    listView.ItemsSource = dataModel;
                }
                else
                {
                    lblEiqueta.IsVisible = true;
                    listView.IsVisible = false;
                }


            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cargar items: " + ex.Message, "Ok");
            }
        }

        public void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);

                Task<bool> respuesta = App.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar el mensaje?", "OK", "Cancelar");
                respuesta.ContinueWith(task =>
                {
                    if (task.Result)
                    {
                        var notificacion = (ModeloNotificaciones)mi.CommandParameter;
                        dataModel.Remove(notificacion);
                        App.Database.EliminarNotificacion(notificacion);
                    }
                });
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "On Delete: " + ex.Message, "OK");
            }
        }

        private void Notificacion_Tapped(object sender, EventArgs e)
        {
            ViewCell varCelda = (ViewCell)sender;
            Models.ModeloNotificaciones varDato = (Models.ModeloNotificaciones)varCelda.BindingContext;
        }
    }
}