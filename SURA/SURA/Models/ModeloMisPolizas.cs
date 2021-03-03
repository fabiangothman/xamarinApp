using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SURA.Models
{
    public partial class MisPolizas
    {
        private string decSaldo;
        private string pagoMinimo;

        //Para la lista de Mis Polizas.xaml
       

        [JsonProperty("POLIZA", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Poliza { get; set; }

        [JsonProperty("POLIZA_CARATULA", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PolizaCaratula { get; set; }

        [JsonProperty("SOLUCION", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Solucion { get; set; }

        [JsonProperty("VIGI", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Vigi { get; set; }

        [JsonProperty("VIGF", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Vigf { get; set; }

        [JsonProperty("ASEGURADO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Asegurado { get; set; }

        [JsonProperty("IDENTIFICACION", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Identificacion { get; set; }

        [JsonProperty("SALDO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Saldo
        {
            get
            {
                return this.decSaldo;
            }
            set
            {
                this.decSaldo = value;
            }
        }

        public string SaldoString
        {
            get
            {
                if (Convert.ToDecimal(decSaldo) >= 0)
                    return Convert.ToDecimal(this.decSaldo).ToString("C", new CultureInfo("es-PA"));
                else
                    return "-" + (Convert.ToDecimal(this.decSaldo) * -1).ToString("C", new CultureInfo("es-PA"));
            }
        }

        [JsonProperty("PAGO_MINIMO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PAGO_MINIMO
        {
            get
            {
                return this.pagoMinimo;
            }
            set
            {
                this.pagoMinimo = value;
            }
        }
        public string Pago_MinimiString
        {
            get
            {
                if (Convert.ToDecimal(pagoMinimo) >= 0)
                    return Convert.ToDecimal(this.pagoMinimo).ToString("C", new CultureInfo("es-PA"));
                else
                    return "-" + (Convert.ToDecimal(this.pagoMinimo) * -1).ToString("C", new CultureInfo("es-PA"));
            }
        }

        [JsonProperty("ESTADO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Estado { get; set; }

        [JsonProperty("PAGO_POR_APLICAR", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PagoPorAplicar { get; set; }

        [JsonProperty("RIESGOS", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Riesgo> Riesgos { get; set; }
    }

    public partial class Riesgo
    {

        private decimal sumaAsegurada;

        [JsonProperty("Poliza", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Poliza { get; set; }

        [JsonProperty("Core", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Core { get; set; }

        [JsonProperty("NumeroRiesgo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int NumeroRiesgo { get; set; }

        [JsonProperty("SumaAsegurada", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumaAsegurada
        {
            get
            {
                return this.sumaAsegurada;
            }
            set
            {
                this.sumaAsegurada = Convert.ToDecimal(value);
            }
        }

        public string SumaAseguradaString
        {
            get
            {
                if (Convert.ToUInt32(sumaAsegurada) >= 0)
                    return Convert.ToUInt32(this.sumaAsegurada).ToString("C", new CultureInfo("es-PA"));
                else
                    return "-" + (Convert.ToUInt32(this.sumaAsegurada) * -1).ToString("C", new CultureInfo("es-PA"));


            }
        }

        [JsonProperty("Ano")]
        public string Ano { get; set; }

        [JsonProperty("Marca")]
        public string Marca { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("Motor")]
        public string Motor { get; set; }

        [JsonProperty("Chasis")]
        public string Chasis { get; set; }

        [JsonProperty("Actividad")]
        public string Actividad { get; set; }

        [JsonProperty("Placa")]
        public string Placa { get; set; }

        [JsonProperty("Provincia")]
        public string Provincia { get; set; }

        [JsonProperty("Distrito")]
        public string Distrito { get; set; }

        [JsonProperty("Corregimiento")]
        public string Corregimiento { get; set; }

        [JsonProperty("Manzana")]
        public object Manzana { get; set; }

        [JsonProperty("COBERTURAS", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Coberturas> Coberturas { get; set; }
    }

    public partial class Coberturas
    {

        private string sumaAsegurada;
        private string String;
        private double primas;
        private double deducible;

        [JsonProperty("Poliza", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Poliza { get; set; }

        [JsonProperty("Core", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Core { get; set; }

        [JsonProperty("NumeroRiesgo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroRiesgo { get; set; }

        [JsonProperty("Cobertura")]
        public string CoberturaCobertura { get; set; }
        //Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore

        [JsonProperty("Limite")]
        public string Limite { get; set; }

        [JsonProperty("SumaAsegurada")]
        public string SumaAsegurada
        {
            get
            {
                if (this.sumaAsegurada == null)
                {
                    return sumaAsegurada;
                }
                else
                {
                    return this.sumaAsegurada;
                }

            }
            set
            {
                this.sumaAsegurada = value;
            }
        }

        public string SumaAseguradaString
        {
            get
            {
                if (sumaAsegurada == null)
                {
                    return String = "";
                }
                else
                {
                    if (Convert.ToDouble(sumaAsegurada) >= 0)
                        return Convert.ToDouble(this.sumaAsegurada).ToString("C", new CultureInfo("es-PA"));
                    else
                        return (Convert.ToDouble(this.sumaAsegurada) * -1).ToString("C", new CultureInfo("es-PA"));
                }



            }
        }
        [JsonProperty("Primas", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Primas
        {
            get
            {
                if (this.primas == null)
                {
                    return primas;
                }
                else
                {
                    return this.primas;
                }

            }
            set
            {
                this.primas = value;
            }
        }

        public string PrimasString
        {
            get
            {
                if (primas == null)
                {
                    return String = "";
                }
                else
                {
                    if (Convert.ToDouble(primas) >= 0)
                        return Convert.ToDouble(this.primas).ToString("C", new CultureInfo("es-PA"));
                    else
                        return (Convert.ToDouble(this.primas) * -1).ToString("C", new CultureInfo("es-PA"));
                }



            }
        }
        [JsonProperty("Deducible")]
        public double Deducible
        {
            get
            {
                if (this.deducible == null)
                {
                    return deducible;
                }
                else
                {
                    return this.deducible;
                }

            }
            set
            {
                this.deducible = value;
            }
        }

        public string DeducibleString
        {
            get
            {
                if (deducible == null)
                {
                    return String = "";
                }
                else
                {
                    if (Convert.ToDouble(deducible) >= 0)
                        return Convert.ToDouble(this.deducible).ToString("C", new CultureInfo("es-PA"));
                    else
                        return (Convert.ToDouble(this.deducible) * -1).ToString("C", new CultureInfo("es-PA"));
                }



            }
        }
    }
}
