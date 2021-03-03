using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico.AsistenciaVial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciaVial1 : ContentView
    {
        Services.NetworkAvailabilityService networkAvailability = new Services.NetworkAvailabilityService();
        bool NetworkStatus;
        public AsistenciaVial1()
        {
            InitializeComponent();
        }

        private void FrmGrúa_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "towBreakdown";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();

                if (NetworkStatus == true)
                {

                    App.objContenedor.MostrarVista("AsistenciaVial2");

                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }

        }
        private void FrmAccidente_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "towCollision";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                if (NetworkStatus == true)
                {
                    App.objContenedor.MostrarVista("AsistenciaVial2");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }
        }
        private void FrmCombustible_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "fuelDelivery";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                if (NetworkStatus == true)
                {
                    App.objContenedor.MostrarVista("AsistenciaVial2");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }
        }
        private void FrmCambioLlanta_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "flatTire";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                if (NetworkStatus == true)
                {
                    App.objContenedor.MostrarVista("AsistenciaVial2");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }
        }
        private void FrmCerrajería_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "locksmith";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                if (NetworkStatus == true)
                {
                    App.objContenedor.MostrarVista("AsistenciaVial2");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }
        }
        private void FrmCargarBatería_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.connectContactModel.TipoServicio = "jumpStart";
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                if (NetworkStatus == true)
                {
                    App.objContenedor.MostrarVista("AsistenciaVial2");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al tratar de conseguir el acceso a la red: " + NetworkStatus); ;
            }
        }

        private double width;
        private double height;
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.ColumnDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.Children.Remove(frmGrúa);
                    OuterGrid.Children.Remove(frmAccidente);
                    OuterGrid.Children.Remove(frmCombustible);
                    OuterGrid.Children.Remove(frmCambioLlanta);
                    OuterGrid.Children.Remove(frmCerrajería);
                    OuterGrid.Children.Remove(frmCargarBatería);
                    OuterGrid.Children.Remove(lblRequiere);
                    OuterGrid.Children.Add(lblRequiere, 0, 1);
                    Grid.SetColumnSpan(lblRequiere, 6);
                    OuterGrid.Children.Add(frmGrúa, 1, 3);
                    OuterGrid.Children.Add(frmAccidente, 3, 3);
                    OuterGrid.Children.Add(frmCombustible, 5, 3);
                    OuterGrid.Children.Add(frmCambioLlanta, 1, 5);
                    OuterGrid.Children.Add(frmCerrajería, 3, 5);
                    OuterGrid.Children.Add(frmCargarBatería, 5, 5);
                }
                else
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.ColumnDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.Children.Remove(frmGrúa);
                    OuterGrid.Children.Remove(frmAccidente);
                    OuterGrid.Children.Remove(frmCombustible);
                    OuterGrid.Children.Remove(frmCambioLlanta);
                    OuterGrid.Children.Remove(frmCerrajería);
                    OuterGrid.Children.Remove(frmCargarBatería);
                    OuterGrid.Children.Remove(lblRequiere);
                    OuterGrid.Children.Add(lblRequiere, 1, 1);
                    Grid.SetColumnSpan(lblRequiere, 3);
                    OuterGrid.Children.Add(frmGrúa, 1, 3);
                    OuterGrid.Children.Add(frmAccidente, 3, 3);
                    OuterGrid.Children.Add(frmCombustible, 1, 5);
                    OuterGrid.Children.Add(frmCambioLlanta, 3, 5);
                    OuterGrid.Children.Add(frmCerrajería, 1, 7);
                    OuterGrid.Children.Add(frmCargarBatería, 3, 7);
                }
            }
        }


    }
}