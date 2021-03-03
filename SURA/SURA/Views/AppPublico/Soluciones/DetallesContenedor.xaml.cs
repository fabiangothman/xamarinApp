
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SURA.Models;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using SURA.Views.AppPublico.Soluciones;

namespace SURA.Views.AppPublico.Soluciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesContenedor : ContentView
    {
        string _solucion;
        ModeloJson Productosjson = null;

        //set y get de booleano del campo empresa y el nombre del asunto
        public bool NegocioValue { get; private set; }
        public string Asuntotext { get; private set; }

        public DetallesContenedor(string solucion)
        {
            InitializeComponent();
            CargarDetalles(solucion);
            _solucion = solucion;
        }

        private void btnVentajas_Clicked(object sender, EventArgs e)
        {
            this.Contenedor.Children.Clear();
            btnVentajas.TextColor = Color.White;
            btnCoberturas.TextColor = Color.FromHex("#1ACDF0");
            btnParaMi.TextColor = Color.FromHex("#1ACDF0");
            btnDanosTerceros.TextColor = Color.FromHex("#1ACDF0");
            btnPlanes.TextColor = Color.FromHex("#1ACDF0");
            CargarVentajas(_solucion);
        }

        private void btnDanosTerceros_Clicked(object sender, EventArgs e)
        {
            this.Contenedor.Children.Clear();
            CargarDanosTerceros(_solucion);
            btnVentajas.TextColor = Color.FromHex("#1ACDF0");
            btnCoberturas.TextColor = Color.FromHex("#1ACDF0");
            btnParaMi.TextColor = Color.FromHex("#1ACDF0");
            btnPlanes.TextColor = Color.FromHex("#1ACDF0");
            btnDanosTerceros.TextColor = Color.White;
        }

        private void btnCoberturas_Clicked(object sender, EventArgs e)
        {
            this.Contenedor.Children.Clear();
            btnVentajas.TextColor = Color.FromHex("#1ACDF0");
            btnCoberturas.TextColor = Color.White;
            btnParaMi.TextColor = Color.FromHex("#1ACDF0");
            btnDanosTerceros.TextColor = Color.FromHex("#1ACDF0");
            btnPlanes.TextColor = Color.FromHex("#1ACDF0");
            CargarCoberturas(_solucion);
        }

        private void btnParaMi_Clicked(object sender, EventArgs e)
        {
            this.Contenedor.Children.Clear();
            btnVentajas.TextColor = Color.FromHex("#1ACDF0");
            btnCoberturas.TextColor = Color.FromHex("#1ACDF0");
            btnParaMi.TextColor = Color.White;
            btnDanosTerceros.TextColor = Color.FromHex("#1ACDF0");
            btnPlanes.TextColor = Color.FromHex("#1ACDF0");
            CargarPorqueYo(_solucion);
        }

        private void btnPlanes_Clicked(object sender, EventArgs e)
        {
            this.Contenedor.Children.Clear();
            btnVentajas.TextColor = Color.FromHex("#1ACDF0");
            btnCoberturas.TextColor = Color.FromHex("#1ACDF0");
            btnParaMi.TextColor = Color.FromHex("#1ACDF0");
            btnPlanes.TextColor = Color.White;
            btnDanosTerceros.TextColor = Color.FromHex("#1ACDF0");
            CargarPlanes(_solucion);
        }

        private void CargarDetalles(string solucion)
        {
            UIService();

            //var grid = new Grid { RowSpacing = 10 };
            switch (solucion)
            {
                case "Seguro de Auto":
                    App.blnSolucionPersonal = true;
                    Scroll.IsVisible = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroAuto[0].ProductBanner;
                    //lblSlogan1.IsVisible = false;
                    lblSlogan1.Text = Productosjson.SeguroAuto[0].Slogan1;

                    //lblSlogan2.Text = Productosjson.SeguroAuto[0].Slogan2;

                    productIcon.Source = "seguroautoWhite";

                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnCoberturas.Text = "COBERTURA COMPLETA";
                    btnCoberturas.IsVisible = true;
                    btnParaMi.IsVisible = false;
                    btnPlanes.IsVisible = false;
                    btnDanosTerceros.IsVisible = true;

                    NegocioValue = false;
                    CargarVentajas(solucion);
                    break;
                case "Seguro de Vida":
                    App.blnSolucionPersonal = true;
                    Scroll.IsVisible = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroVida[0].ProductBanner;
                    lblSlogan1.Text = Productosjson.SeguroVida[0].Slogan1;
                    //lblSlogan2.Text = Productosjson.SeguroVida[0].Slogan2;
                    productIcon.Source = "segurovidaWhite";

                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnCoberturas.Text = "COBERTURAS";
                    btnCoberturas.IsVisible = true;
                    btnParaMi.IsVisible = true;
                    btnDanosTerceros.IsVisible = false;
                    btnPlanes.IsVisible = false;

                    NegocioValue = false;
                    CargarVentajas(solucion);
                    break;

                case "Seguro de Accidentes personales":
                    App.blnSolucionPersonal = true;
                    Scroll.IsVisible = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroAccidente[0].ProductBanner;
                    //lblSlogan1.IsVisible = false;
                    lblSlogan1.Text = Productosjson.SeguroAccidente[0].Slogan1;
                    //lblSlogan2.Text = Productosjson.SeguroAccidente[0].Slogan2;
                    productIcon.Source = "seguroautoWhite";

                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnCoberturas.Text = "COBERTURAS";
                    btnCoberturas.IsVisible = true;
                    btnParaMi.IsVisible = false;
                    btnDanosTerceros.IsVisible = false;

                    NegocioValue = false;
                    CargarVentajas(solucion);
                    break;

                case "Seguro de Protección y Asistencia Médica (PAM)":
                    App.blnSolucionPersonal = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroPam[0].ProductBanner;
                    lblSlogan1.Text = Productosjson.SeguroPam[0].Slogan1;
                    //lblSlogan2.Text = Productosjson.SeguroPam[0].Slogan2;
                    productIcon.Source = "enfermedadesgravesWhite";
                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnCoberturas.Text = "COBERTURAS";
                    btnCoberturas.IsVisible = true;
                    btnParaMi.IsVisible = true;
                    btnDanosTerceros.IsVisible = false;
                    btnPlanes.IsVisible = false;

                    CargarVentajas(solucion);
                    NegocioValue = false;
                    break;

                case "Seguro de Asistencia Funeraria":
                    App.blnSolucionPersonal = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroAsistenciaFuneraria[0].ProductBanner;
                    //lblSlogan1.IsVisible = false;
                    lblSlogan1.Text = Productosjson.SeguroAsistenciaFuneraria[0].Slogan1;
                    //lblSlogan2.Text = Productosjson.SeguroAsistenciaFuneraria[0].Slogan2;
                    productIcon.Source = "asistenciafunerariaWhite";
                    btnParaMi.Focus();

                    btnPlanes.IsVisible = true;
                    btnParaMi.IsVisible = true;
                    btnPlanes.TextColor = Color.White;
                    btnCoberturas.IsVisible = false;
                    btnVentajas.IsVisible = false;
                    btnDanosTerceros.IsVisible = false;
                    NegocioValue = false;
                    CargarPlanes(solucion);
                    break;

                case "Negocio Protegido":
                    App.blnSolucionPersonal = false;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroNegocio[0].ProductBanner;
                    lblSlogan1.Text = Productosjson.SeguroNegocio[0].Slogan1;
                    //lblSlogan2.Text = Productosjson.SeguroNegocio[0].Slogan2;
                    productIcon.Source = "negocioprotegidoWhite";
                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnPlanes.IsVisible = true;
                    btnCoberturas.IsVisible = false;
                    btnDanosTerceros.IsVisible = false;
                    btnParaMi.IsVisible = false;
                    NegocioValue = true;
                    CargarVentajas(solucion);
                    break;

                case "Seguro de Hogar":
                    App.blnSolucionPersonal = true;
                    PanelDefinicion.IsVisible = true;
                    Banner.Source = Productosjson.SeguroHogar[0].ProductBanner;
                    lblSlogan1.Text = Productosjson.SeguroHogar[0].Slogan1; ;
                    //lblSlogan2.Text = Productosjson.SeguroHogar[0].Slogan2; ;
                    productIcon.Source = "multiproteccionhogarWhite";
                    btnVentajas.Focus();
                    btnVentajas.TextColor = Color.White;
                    btnCoberturas.Text = "COBERTURAS";
                    btnCoberturas.IsVisible = true;
                    btnParaMi.IsVisible = false;
                    btnDanosTerceros.IsVisible = false;
                    btnPlanes.IsVisible = false;

                    NegocioValue = false;
                    CargarVentajas(solucion);
                    break;
                default:
                    break;
            }
            Asuntotext = solucion;
        }

        private void CargarVentajas(string solucion)
        {
            var obj = Productosjson.SeguroAuto[0].Ventajas;
            bool tieneTitulo = false;
            int posicion = 0;
            switch (solucion)
            {
                case "Seguro de Auto":
                    obj = Productosjson.SeguroAuto[0].Ventajas;
                    break;
                case "Seguro de Vida":
                    obj = Productosjson.SeguroVida[0].Ventajas; //error index was out of range. Must be non-negative and less than the size of the collection
                    break;
                case "Seguro de Protección y Asistencia Médica (PAM)":
                    obj = Productosjson.SeguroPam[0].Ventajas;
                    break;
                case "Seguro de Hogar":
                    obj = Productosjson.SeguroHogar[0].Ventajas;
                    break;
                case "Seguro de Accidentes personales":
                    obj = Productosjson.SeguroAccidente[0].Ventajas;
                    break;
                case "Negocio Protegido":
                    obj = Productosjson.SeguroNegocio[0].Ventajas;
                    break;
                default:
                    App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error", "Ok");
                    break;
            }

            Grid grid = new Grid();

            grid.RowDefinitions = new RowDefinitionCollection();
            grid.ColumnDefinitions = new ColumnDefinitionCollection();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            ////App.Current.MainPage.DisplayAlert("SURA Cantidad Ventajas", obj.Count.ToString(), "Ok");

            for (var i = 0; i < obj.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (obj[i].Titulo != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA titulo",i.ToString()+""+posicion.ToString()+" "+ obj[i].Titulo, "Ok");
                    var lblNuevo = new Label { Text = obj[i].Titulo, TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
                    grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                    Grid.SetColumnSpan(lblNuevo, 2);
                    tieneTitulo = true;
                }
                if (obj[i].Descripcion != null)
                {
                    if (tieneTitulo)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion con titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Titulo, "Ok");
                        posicion = posicion + 1;
                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                    else
                    {
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion sin titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Descripcion, "Ok");

                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                }
                if (obj[i].Lista != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA Cantidad Lista", Productosjson.SeguroAuto[0].Ventajas[i].Lista.Count.ToString(), "OK");
                    for (var j = 1; j <= obj[i].Lista.Count; j++)
                    {
                        ////posicion = posicion + 1;
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        var lblPunto = new Label { Text = "•", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                        var lblNuevo = new Label { Text = obj[i].Lista[j - 1], TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblPunto, 0, posicion);
                        grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                        posicion = posicion + 1;
                    }
                }
                if (!string.IsNullOrEmpty(obj[i].Disclaimer))
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    var lblNuevo = new Label { Text = obj[i].Disclaimer, TextColor = Color.FromHex("#e7df00"), FontAttributes = FontAttributes.Bold, FontSize = 18, VerticalOptions = LayoutOptions.Center };
                    grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                    
                    posicion = posicion + 1;
                }
                //posicion++;
                tieneTitulo = false;
            }
            Contenedor.Children.Add(grid);
        }

        private void CargarCoberturas(string solucion)
        {
            var obj = Productosjson.SeguroAuto[0].Cobertura;
            bool tieneTitulo = false;
            int posicion = 0;
            switch (solucion)
            {
                case "Seguro de Auto":
                    obj = Productosjson.SeguroAuto[0].Cobertura;
                    break;
                case "Seguro de Vida":
                    obj = Productosjson.SeguroVida[0].Cobertura; //error index was out of range. Must be non-negative and less than the size of the collection
                    break;
                case "Seguro de Protección y Asistencia Médica (PAM)":
                    obj = Productosjson.SeguroPam[0].Cobertura;
                    break;
                case "Seguro de Asistencia Funeraria":
                    obj = Productosjson.SeguroAsistenciaFuneraria[0].Cobertura; //Error Object reference not set to an instance of an object
                    break;
                case "Seguro de Hogar":
                    obj = Productosjson.SeguroHogar[0].Cobertura;
                    break;
                case "Seguro de Accidentes personales":
                    obj = Productosjson.SeguroAccidente[0].Cobertura;
                    break;
                default:
                    App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error", "Ok");
                    break;
            }

            Grid grid = new Grid();

            grid.RowDefinitions = new RowDefinitionCollection();
            grid.ColumnDefinitions = new ColumnDefinitionCollection();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //App.Current.MainPage.DisplayAlert("SURA Cantidad Ventajas", obj.Count.ToString(), "Ok");

            for (var i = 0; i < obj.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (obj[i].Titulo != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA titulo",i.ToString()+""+posicion.ToString()+" "+ obj[i].Titulo, "Ok");
                    var lblNuevo = new Label { Text = obj[i].Titulo, TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
                    grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                    Grid.SetColumnSpan(lblNuevo, 2);
                    tieneTitulo = true;
                }
                if (obj[i].Descripcion != null)
                {
                    if (tieneTitulo)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion con titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Titulo, "Ok");
                        posicion = posicion + 1;
                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                    else
                    {
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion sin titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Descripcion, "Ok");

                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                }
                if (obj[i].Lista != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA Cantidad Lista", Productosjson.SeguroAuto[0].Ventajas[i].Lista.Count.ToString(), "OK");
                    for (var j = 1; j <= obj[i].Lista.Count; j++)
                    {
                        posicion = posicion + 1;
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        var lblPunto = new Label { Text = "•", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                        var lblNuevo = new Label { Text = obj[i].Lista[j - 1], TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblPunto, 0, posicion);
                        grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                    }
                    posicion = posicion + 1;
                }
                //posicion++;
                tieneTitulo = false;
            }
            Contenedor.Children.Add(grid);
        }

        private void CargarPorqueYo(string solucion)
        {
            var obj = Productosjson.SeguroAuto[0].PorqueYo;
            bool tieneTitulo = false;
            int posicion = 0;
            switch (solucion)
            {
                case "Seguro de Vida":
                    obj = Productosjson.SeguroVida[0].PorqueYo; //error index was out of range. Must be non-negative and less than the size of the collection
                    break;
                case "Seguro de Protección y Asistencia Médica (PAM)":
                    obj = Productosjson.SeguroPam[0].PorqueYo;
                    break;
                case "Seguro de Asistencia Funeraria":
                    obj = Productosjson.SeguroAsistenciaFuneraria[0].PorqueYo; //Error Object reference not set to an instance of an object
                    break;
                case "Seguro de Accidentes personales":
                    obj = Productosjson.SeguroAccidente[0].PorqueYo;
                    break;
                default:
                    App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error", "Ok");
                    break;
            }

            Grid grid = new Grid();

            grid.RowDefinitions = new RowDefinitionCollection();
            grid.ColumnDefinitions = new ColumnDefinitionCollection();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //App.Current.MainPage.DisplayAlert("SURA Cantidad Ventajas", obj.Count.ToString(), "Ok");

            for (var i = 0; i < obj.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (obj[i].Titulo != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA titulo",i.ToString()+""+posicion.ToString()+" "+ obj[i].Titulo, "Ok");
                    var lblNuevo = new Label { Text = obj[i].Titulo, TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
                    grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                    Grid.SetColumnSpan(lblNuevo, 2);
                    tieneTitulo = true;
                }
                if (obj[i].Descripcion != null)
                {
                    if (tieneTitulo)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion con titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Titulo, "Ok");
                        posicion = posicion + 1;
                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                    else
                    {
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion sin titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Descripcion, "Ok");

                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                }
                if (obj[i].Lista != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA Cantidad Lista", Productosjson.SeguroAuto[0].Ventajas[i].Lista.Count.ToString(), "OK");
                    for (var j = 1; j <= obj[i].Lista.Count; j++)
                    {
                        posicion = posicion + 1;
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        var lblPunto = new Label { Text = "•", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                        var lblNuevo = new Label { Text = obj[i].Lista[j - 1], TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblPunto, 0, posicion);
                        grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                    }
                    posicion = posicion + 1;
                }
                //posicion++;
                tieneTitulo = false;
            }
            Contenedor.Children.Add(grid);
        }

        private void CargarPlanes(string solucion)
        {
            try
            {
                var obj = Productosjson.SeguroNegocio[0].Planes;
                bool tieneTitulo = false;
                int posicion = 0;
                switch (solucion)
                {
                    case "Seguro de Asistencia Funeraria":
                        obj = Productosjson.SeguroAsistenciaFuneraria[0].Planes;
                        break;
                    case "Seguro de Accidentes personales":
                        obj = Productosjson.SeguroAccidente[0].Planes;
                        break;
                    case "Negocio Protegido":
                        obj = Productosjson.SeguroNegocio[0].Planes;
                        break;
                    default:
                        App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error", "Ok");
                        break;
                }

                Grid grid = new Grid();

                grid.RowDefinitions = new RowDefinitionCollection();
                grid.ColumnDefinitions = new ColumnDefinitionCollection();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                for (var i = 0; i < obj.Count; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    if (obj[i].Titulo != null)
                    {
                        //App.Current.MainPage.DisplayAlert("SURA titulo",i.ToString()+""+posicion.ToString()+" "+ obj[i].Titulo, "Ok");
                        var lblNuevo = new Label { Text = obj[i].Titulo, TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        tieneTitulo = true;
                    }
                    if (obj[i].Descripcion != null)
                    {
                        if (tieneTitulo)
                        {
                            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            //App.Current.MainPage.DisplayAlert("SURA Descripcion con titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Titulo, "Ok");
                            posicion = posicion + 1;
                            var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                            grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                            Grid.SetColumnSpan(lblNuevo, 2);
                            posicion = posicion + 1;
                        }
                        else
                        {
                            //App.Current.MainPage.DisplayAlert("SURA Descripcion sin titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Descripcion, "Ok");

                            var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                            grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                            Grid.SetColumnSpan(lblNuevo, 2);
                            posicion = posicion + 1;
                        }
                    }
                    if (obj[i].Lista != null)
                    {
                        //App.Current.MainPage.DisplayAlert("SURA Cantidad Lista", Productosjson.SeguroAuto[0].Ventajas[i].Lista.Count.ToString(), "OK");
                        for (var j = 1; j <= obj[i].Lista.Count; j++)
                        {
                            posicion = posicion + 1;
                            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            var lblPunto = new Label { Text = "•", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                            var lblNuevo = new Label { Text = obj[i].Lista[j - 1], TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                            grid.Children.Add(lblPunto, 0, posicion);
                            grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                        }
                        posicion = posicion + 1;
                    }
                    //posicion++;
                    tieneTitulo = false;
                }
                Contenedor.Children.Add(grid);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cargar planes: " + ex.Message, "Ok");
            }
        }

        private void CargarDanosTerceros(string solucion)
        {
            var obj = Productosjson.SeguroNegocio[0].DanosTerceros;
            bool tieneTitulo = false;
            int posicion = 0;
            switch (solucion)
            {
                case "Seguro de Auto":
                    obj = Productosjson.SeguroAuto[0].DanosTerceros; //Error Object reference not set to an instance of an object
                    break;
                default:
                    App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error", "Ok");
                    break;
            }

            Grid grid = new Grid();

            grid.RowDefinitions = new RowDefinitionCollection();
            grid.ColumnDefinitions = new ColumnDefinitionCollection();

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //App.Current.MainPage.DisplayAlert("SURA Cantidad Ventajas", obj.Count.ToString(), "Ok");

            for (var i = 0; i < obj.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (obj[i].Titulo != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA titulo",i.ToString()+""+posicion.ToString()+" "+ obj[i].Titulo, "Ok");
                    var lblNuevo = new Label { Text = obj[i].Titulo, TextColor = Color.White, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
                    grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                    Grid.SetColumnSpan(lblNuevo, 2);
                    tieneTitulo = true;
                }
                if (obj[i].Descripcion != null)
                {
                    if (tieneTitulo)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion con titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Titulo, "Ok");
                        posicion = posicion + 1;
                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                    else
                    {
                        //App.Current.MainPage.DisplayAlert("SURA Descripcion sin titulo", i.ToString() + "" + posicion.ToString() + " " + obj[i].Descripcion, "Ok");

                        var lblNuevo = new Label { Text = obj[i].Descripcion, TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblNuevo, 0, posicion); //Columna, fila
                        Grid.SetColumnSpan(lblNuevo, 2);
                        posicion = posicion + 1;
                    }
                }
                if (obj[i].Lista != null)
                {
                    //App.Current.MainPage.DisplayAlert("SURA Cantidad Lista", Productosjson.SeguroAuto[0].Ventajas[i].Lista.Count.ToString(), "OK");
                    for (var j = 1; j <= obj[i].Lista.Count; j++)
                    {
                        posicion = posicion + 1;
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        var lblPunto = new Label { Text = "•", TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                        var lblNuevo = new Label { Text = obj[i].Lista[j - 1], TextColor = Color.White, VerticalOptions = LayoutOptions.Center };
                        grid.Children.Add(lblPunto, 0, posicion);
                        grid.Children.Add(lblNuevo, 1, posicion); //Columna, fila
                    }
                    posicion = posicion + 1;
                }
                //posicion++;
                tieneTitulo = false;
            }
            Contenedor.Children.Add(grid);
        }

        private void UIService()
        {
            string nombreArchivo = "SURA.Data.DatosProductos.json";
            Assembly assembly = typeof(ModeloJson).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(nombreArchivo);

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var modeloJson = ModeloJson.FromJson(json);
                Productosjson = modeloJson;
            }
        }

        //boton manda valores al formulario
        private async void BtnContactenos_Clicked(object sender, EventArgs e)
        {
            try
            {
                //declara variables que son enviadas en el onclicked
                //var item = (Frame)sender;
                var myboolean = NegocioValue;
                var titulonext = Asuntotext;

                //declara el dictionary que lleva ambos valores
                Dictionary<string, string> informacionAsunto = new Dictionary<string, string>();
                informacionAsunto.Add("asunto", titulonext);
                informacionAsunto.Add("boolean", myboolean.ToString());

                App.objContenedor.MostrarVista("FormularioContacto", informacionAsunto);
                //enviar asunto y boolean
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Botón Contactenos: " + ex.Message, "Ok");
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
                    sloganGrid.RowDefinitions.Clear();
                    sloganGrid.ColumnDefinitions.Clear();
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90.5, GridUnitType.Star) });
                }
                else
                {
                    sloganGrid.RowDefinitions.Clear();
                    sloganGrid.ColumnDefinitions.Clear();
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    sloganGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Star) });
                    sloganGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90.5, GridUnitType.Star) });
                }
            }
        }
    }
}