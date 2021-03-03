using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SURA.Models
{
    public class ModelPolizaAPagar : INotifyPropertyChanged
    {
        [JsonProperty("polid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int PolId { get; set; }

        [JsonProperty("poliza", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Poliza { get; set; }

        [JsonProperty("polizaCaratula", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PolizaCaratula { get; set; }

        [JsonProperty("solucion", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Solucion { get; set; }

        [JsonProperty("vigenciaInicial", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string VigenciaInicial { get; set; }

        [JsonProperty("vigenciaFinal", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string VigenciaFinal { get; set; }

        [JsonProperty("estado", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Estado { get; set; }

        [JsonProperty("saldo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Saldo { get; set; }

        public string SaldoString { get { return Saldo.ToString("C", new CultureInfo("es-PA")); } }

        [JsonProperty("primaSinImpuesto", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double PrimaSinImpuesto { get; set; }

        [JsonProperty("pagoSugerido", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double PagoSugerido
        {
            get { return _pagoSugerido; }
            set
            {
                _pagoSugerido = value;
                _montoPagarValue = value.ToString();
            }
        }

        public double _pagoSugerido { get; set; }

        [JsonProperty("pagoPorAplicar", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double PagoPorAplicar { get; set; }

        public string PagoPorAplicarString
        {
            get
            {
                return PagoPorAplicar.ToString("C", new CultureInfo("es-PA"));
            }
        }
        [JsonProperty("pagoMinimo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double PagoMinimo { get; set; }

        [JsonProperty("prontoPago", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ModelProntoPago ProntoPago { get; set; }
        public bool AplicaProntoPago
        {
            get
            {
                if (ProntoPago.FechaAplica && ProntoPago.SolucionAplica)
                    return true;

                return false;
            }
        }

        public string PagoMinimoValidation
        {
            get
            {
                if (Convert.ToDouble(MontoPagarSeleccionado) < PagoMinimo && Selected)
                {
                    return $"Debe ingresar un pago mínimo de {PagoMinimo.ToString("C", new CultureInfo("es-PA"))}";
                }
                else
                {
                    return "";
                }
            }
        }

        private bool _selected { get; set; } = true;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                NotifyPropertyChanged("Selected");
                NotifyPropertyChanged("PagoMinimoValidation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _montoPagarValue { get; set; }
        public string MontoPagarSeleccionado
        {
            get
            {
                return _montoPagarValue;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "0";
                }
                try
                {
                    Convert.ToDouble(value);
                    _montoPagarValue = value;
                }
                catch (Exception e)
                {
                    NotifyPropertyChanged("MontoPagarSeleccionado");
                }
                NotifyPropertyChanged("PagoMinimoValidation");
            }
        }

        public string MontoPagarSeleccionadoString
        {
            get
            {
                return Convert.ToDouble(MontoPagarSeleccionado).ToString("C", new CultureInfo("es-PA"));
            }
        }

        public string MontoSugeridoString
        {
            get
            {
                return "Monto (Sugerido " + PagoSugerido.ToString("C", new CultureInfo("es-PA")) + ")";
            }
        }


        public double Descuento
        {
            get
            {
                if (Convert.ToDouble(MontoPagarSeleccionado) >= Saldo && ProntoPago.FechaAplica && ProntoPago.SolucionAplica)
                {
                    return ProntoPago.Descuento;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string DescuentoString
        {
            get
            {
                if (Convert.ToDouble(MontoPagarSeleccionado) >= Saldo && ProntoPago.FechaAplica && ProntoPago.SolucionAplica)
                {
                    return ProntoPago.Descuento.ToString("C", new CultureInfo("es-PA"));
                }
                else
                {
                    return 0.ToString("C", new CultureInfo("es-PA"));
                }
            }
        }

        public double TotalPoliza
        {
            get
            {
                if (Descuento == 0)
                {
                    return Convert.ToDouble(MontoPagarSeleccionado);
                }
                else
                {
                    return ProntoPago.Total;
                }
            }
        }

        public string TotalPolizaString
        {
            get
            {
                if (Descuento == 0)
                {
                    return Convert.ToDouble(MontoPagarSeleccionado).ToString("C", new CultureInfo("es-PA"));
                }
                else
                {
                    return ProntoPago.Total.ToString("C", new CultureInfo("es-PA"));
                }
            }
        }
    }

    public class ModelProntoPago
    {
        [JsonProperty("fechaAplica", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool FechaAplica { get; set; }

        [JsonProperty("solucionAplica", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool SolucionAplica { get; set; }

        [JsonProperty("porcentajeDescuento", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double PorcentajeDescuento { get; set; }

        [JsonProperty("descuento", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Descuento { get; set; }

        [JsonProperty("total", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Total { get; set; }
    }
}
