using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico.Contacto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioContacto : ContentView
    {
        string respuesta;
        bool esEmpresa = false;
        public FormularioContacto()
        {
            InitializeComponent();

            entAsunto.IsReadOnly = false;
            entAsunto.Placeholder = "Asunto*";
            frameEntryAsunto.BackgroundColor = Color.White;
        }

        // Trayendo el dictionario de detail view container
        public FormularioContacto(Dictionary<string, string> miAsunto)
        {
            InitializeComponent();

            //declara variables y almacena los datos del directorio
            var asunto = miAsunto["asunto"];
            var empresaEnableorDisable = miAsunto["boolean"];
            //le da los valores a los campos entry de company y asunto

            if (string.IsNullOrWhiteSpace(asunto))
            {
                entAsunto.IsReadOnly = false;
                entAsunto.Placeholder = "Asunto*";
                frameEntryAsunto.BackgroundColor = Color.White;
            }
            else
            {
                entAsunto.Text = asunto;
            }
            esEmpresa = Convert.ToBoolean(empresaEnableorDisable);
            frameEntryCompany.IsVisible = esEmpresa;
        }


        private async void SentMode_Tapped(object sender, EventArgs e)
        {
            try
            {
                string action = await App.Current.MainPage.DisplayActionSheet("Seleccione:", "Cancelar", null, "Correo electrónico", "Telefónico", "Físico");

                if (action == "Cancelar")
                {
                    entModoContacto.Placeholder = "Medio de respuesta:*";
                }
                else
                {
                    entModoContacto.Text = action.ToString();
                    entModoContacto.TextColor = Color.Black;
                    respuesta = action;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void Enviar_Clicked(object sender, EventArgs e)
        {
            string nombre = entNombre.Text;
            string correo = entCorreo.Text;
            string celular = entCelular.Text;
            string asunto = entAsunto.Text;
            string descripcion = entDescripcion.Text;
            string accion = respuesta;

            if (!esEmpresa)
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(celular) || string.IsNullOrWhiteSpace(asunto) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(accion))
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                }
                else
                {
                    LimpiarFormulario();
                }
            }
            else
            {
                string empresa = entEmpresa.Text;
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(celular) || string.IsNullOrWhiteSpace(asunto) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(empresa) || string.IsNullOrWhiteSpace(accion))
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                }
                else
                {
                    LimpiarFormulario();
                }
            }
        }

        private void LimpiarFormulario()
        {
            App.Current.MainPage.DisplayAlert("SURA", "Su mensaje ha sido enviado satisfactoriamente", "Ok");
            Services.SURAContactService sURAContactService = new Services.SURAContactService();
            sURAContactService.ContactSURA(entNombre.Text, entCorreo.Text, entCelular.Text, entAsunto.Text, entDescripcion.Text, entModoContacto.Text, entEmpresa.Text);
            entModoContacto.Text = "";
            App.Current.MainPage.DisplayAlert("SURA", "Su mensaje ha sido enviado satisfactoriamente", "Ok");

            entNombre.Text = "";
            entCorreo.Text = "";
            entCelular.Text = "";
            entAsunto.Text = "";
            entDescripcion.Text = "";
            entEmpresa.Text = "";
            entModoContacto.Placeholder = "Medio de respuesta:*";
        }

        private void entCorreo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string email = entCorreo.Text;
                if (string.IsNullOrWhiteSpace(email))
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Ingrese su correo electrónico", "Ok");
                }
                else
                {
                    if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                    {
                        App.Current.MainPage.DisplayAlert("SURA", "El correo electrónico ingresado no es correcto, vuelva a ingresarlo", "Ok");
                        entCorreo.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Campo de correo no enfocado: " + ex.Message, "OK");
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
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.Children.Remove(ImgLogo);
                    OuterGrid.Children.Remove(StckSlogan);
                    OuterGrid.Children.Remove(ScrllFormulario);
                    OuterGrid.Children.Remove(frmIngresar);
                    OuterGrid.Children.Add(StckSlogan, 1, 1);
                    OuterGrid.Children.Add(ScrllFormulario, 2, 1);
                    frmIngresar.Margin = new Thickness(0,2,0,5);
                    OuterGrid.Children.Add(frmIngresar, 2, 2);
                }
                else
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.ColumnDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(65, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Absolute) });
                    OuterGrid.Children.Remove(ImgLogo);
                    OuterGrid.Children.Remove(StckSlogan);
                    OuterGrid.Children.Remove(ScrllFormulario);
                    OuterGrid.Children.Remove(frmIngresar);
                    OuterGrid.Children.Add(ImgLogo, 1, 1);
                    OuterGrid.Children.Add(StckSlogan, 1, 2);
                    OuterGrid.Children.Add(ScrllFormulario, 1, 3);
                    frmIngresar.Margin = new Thickness(30, 8, 30, 8);
                    OuterGrid.Children.Add(frmIngresar, 1, 4);
                    
                }
            }
        }
    } 
}