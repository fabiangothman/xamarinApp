using Rg.Plugins.Popup.Services;
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
    public partial class SolucionesPersonas : ContentView
    {
        public SolucionesPersonas()
        {
            InitializeComponent();
        }

        async void EnviarPaginaAsync(string Detalle)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                App.objContenedor.MostrarVista("DetallesContenedor", Detalle);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "EnviarPagina: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private void frmAuto_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Auto");
        }

        private void frmVidaIndividual_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Vida");
        }

        private void frmAsistenciaFuneraria_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Asistencia Funeraria");
        }

        private void frmEnfermedadesGraves_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Protección y Asistencia Médica (PAM)");
        }

        private void frmAccidentesPersonales_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Accidentes personales");
        }

        private void frmProteccionHogar_Tapped(object sender, EventArgs e)
        {
            EnviarPaginaAsync("Seguro de Hogar");
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
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Absolute) });
                    OuterGrid.Children.Remove(frmAuto);
                    OuterGrid.Children.Remove(frmVidaIndividual);
                    OuterGrid.Children.Remove(frmAccidentesPersonales);
                    OuterGrid.Children.Remove(frmAsistenciaFuneraria);
                    OuterGrid.Children.Remove(frmEnfermedadesGraves);
                    OuterGrid.Children.Remove(frmMultiproteccionHogar);
                    OuterGrid.Children.Add(frmAuto, 1, 1);
                    OuterGrid.Children.Add(frmVidaIndividual, 3, 1);
                    OuterGrid.Children.Add(frmEnfermedadesGraves, 1, 3);
                    OuterGrid.Children.Add(frmAsistenciaFuneraria, 3, 3);
                    OuterGrid.Children.Add(frmAccidentesPersonales, 5, 1);
                    OuterGrid.Children.Add(frmMultiproteccionHogar, 5, 3);
                }
                else
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.ColumnDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) });
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
                    OuterGrid.Children.Remove(frmAuto);
                    OuterGrid.Children.Remove(frmVidaIndividual);
                    OuterGrid.Children.Remove(frmAccidentesPersonales);
                    OuterGrid.Children.Remove(frmAsistenciaFuneraria);
                    OuterGrid.Children.Remove(frmEnfermedadesGraves);
                    OuterGrid.Children.Remove(frmMultiproteccionHogar);
                    OuterGrid.Children.Add(frmAuto, 1, 1);
                    OuterGrid.Children.Add(frmVidaIndividual, 3, 1);
                    OuterGrid.Children.Add(frmEnfermedadesGraves, 3, 3);
                    OuterGrid.Children.Add(frmAsistenciaFuneraria, 1, 3);
                    OuterGrid.Children.Add(frmAccidentesPersonales, 1, 5);
                    OuterGrid.Children.Add(frmMultiproteccionHogar, 3, 5);
                }
            }
        }
    }
}