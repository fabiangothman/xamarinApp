using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SURA.Models
{
    public class ModelPlataformaPago
    {
        public List<ModelPolizaAPagar> PolizasAPagar { get; set; }
        public ObservableCollection<ModelTarjeta> TarjetasCreadas { get; set; }
        public List<ReciboPolizaModel> RecibosPolizas { get; set; }

        public string TokenTarjeta { get; set; }
        public string CVVTarjeta { get; set; }

        public string PagoTotal
        {
            get
            {
                return PolizasAPagar.Where(w => w.Selected).Sum(s => s.TotalPoliza).
                        ToString("C", new CultureInfo("es-PA"));
            }
        }

        public string DescuentoTotal
        {
            get
            {
                return PolizasAPagar.Where(w => w.Selected).Sum(s => s.Descuento).ToString("C", new CultureInfo("es-PA"));
            }
        }

        public string NumeroRecibo { get; set; }
        public string NombreCliente { get; set; }
    }
}
