using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services
{
    class ServicioDashboard
    {
        public Action DisplayInvalidLoginPrompt;
        public void ObtenerDatos()
        {
            try
            {
                if (App.Token != "" || App.Token != null)
                {
                    var client = new RestClient(App.PathServiciosSURA);
                    client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(App.Token, "Bearer");
                    var request = new RestRequest("/api/Dashboard", Method.GET);
                    request.RequestFormat = DataFormat.Json;
                    //request.Timeout = 5000;
                    var response = client.Execute(request);
                    string DataResponse = response.Content;

                    if (DataResponse == "" || DataResponse == null)
                    {
                        throw new Exception("Usuario o contraseña incorrecta");
                    }
                    else
                    {
                        App.modeloDashboard = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ModeloDashboard>(DataResponse);
                    }
                }
                else
                {
                    DisplayInvalidLoginPrompt();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DataPrueba()
        {
            //CAMPOS OPCIONALES
            //   'FECHA_NACIMIENTO':'/Date(407134800000)/',
           //'TIPO_PERSONA':'NATURAL',
           //'TIPO_IDENTIFICACION':'CEDULA',
           //'EDAD':'37',
           //'GENERO':'MASCULINO',
            try
            {
                string datosPrueba = @"{ 
   'NOMBRE_COMPLETO':'VIRGILIO ELIAS SERRANO CHONG',
   
   'APELLIDO':'SERRANO CHONG',
   'IDENTIFICACION':'8-761-722',
   'POLIZAS':[ 
      { 
         'POLIZA':'0400040303484889',
         'POLIZA_CARATULA':'040303484889',
         'SOLUCION':'Automóvil',
         'VIGI':'09/08/2019',
         'VIGF':'09/08/2020',
         'ASEGURADO':'VIRGILIO ELIAS SERRANO CHONG',
         'IDENTIFICACION':'8-761-722',
         'SALDO':'210.66',
         'PAGO_MINIMO':'35.10',
         'ESTADO':'VIGENTE',
         'PAGO_POR_APLICAR':'67.14',
         'RIESGOS':[ 
            { 
               'Poliza':'0400040303484889',
               'Core':'N',
               'NumeroRiesgo':1,
               'SumaAsegurada':7896.25,
               'Ano':'2015',
               'Marca':'CHEVROLET N300',
               'Color':null,
               'Motor':'LZWACAGA8F6010696   ',
               'Chasis':'LZWACAGA8F6010696   ',
               'Actividad':'AUTO PARTICULAR',
               'Placa':'BA4195    ',
               'Provincia':null,
               'Distrito':null,
               'Corregimiento':null,
               'Manzana':null,
               'COBERTURAS':[ 
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'LESIONES CORPORALES',
                     'Limite':'Por Persona: $50,000.00 / Por Accidente: $100,000.00',
                     'SumaAsegurada':null,
                     'Primas':'36.86',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'DAÑOS A LA PROPIEDAD AJENA',
                     'Limite':'$50,000.00',
                     'SumaAsegurada':null,
                     'Primas':'52.21',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'GASTOS MEDICOS',
                     'Limite':'Por Persona: $5,000.00 / Por Accidente: $25,000.00',
                     'SumaAsegurada':null,
                     'Primas':'15.36',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'COMPRENSIVO',
                     'Limite':'$7,896.25',
                     'SumaAsegurada':null,
                     'Primas':'53.35',
                     'Deducible':'165.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'COLISION O VUELCO',
                     'Limite':'$7,896.25',
                     'SumaAsegurada':null,
                     'Primas':'128.54',
                     'Deducible':'419.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'INCENDIO',
                     'Limite':'$7,896.25',
                     'SumaAsegurada':null,
                     'Primas':'0.00',
                     'Deducible':'165.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'HURTO',
                     'Limite':'$7,896.25',
                     'SumaAsegurada':null,
                     'Primas':'0.00',
                     'Deducible':'165.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'FULL EXTRAS/ENTREGUE LAS LLAVES',
                     'Limite':null,
                     'SumaAsegurada':null,
                     'Primas':'9.43',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'LESIONES CORPORALES SOAT',
                     'Limite':'$10,000.00',
                     'SumaAsegurada':null,
                     'Primas':'6.52',
                     'Deducible':'7.00'
                  },
                  { 
                     'Poliza':'0400040303484889',
                     'Core':'N',
                     'NumeroRiesgo':1,
                     'Cobertura':'DAÑOS A LA PROPIEDAD AJENA SOAT',
                     'Limite':'$5,000.00',
                     'SumaAsegurada':null,
                     'Primas':'23.73',
                     'Deducible':'24.00'
                  }
               ]
            }
         ]
      },
      { 
         'POLIZA':'13-04-760397-0',
         'POLIZA_CARATULA':'13-04-0760397-0',
         'SOLUCION':'Accidentes Personales',
         'VIGI':'30/09/2019',
         'VIGF':'30/09/2020',
         'ASEGURADO':'VIRGILIO SERRANO CHONG',
         'IDENTIFICACION':'8-761-722',
         'SALDO':'138.50',
         'PAGO_MINIMO':'27.70',
         'ESTADO':'VIGENTE',
         'PAGO_POR_APLICAR':'0.00',
         'RIESGOS':[ 
            { 
               'Poliza':'13-04-760397-0',
               'Core':'P',
               'NumeroRiesgo':1,
               'SumaAsegurada':60000.0,
               'Ano':null,
               'Marca':null,
               'Color':null,
               'Motor':null,
               'Chasis':null,
               'Actividad':null,
               'Placa':null,
               'Provincia':null,
               'Distrito':null,
               'Corregimiento':null,
               'Manzana':null,
               'COBERTURAS':[ 
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'MUERTE ACCIDENTAL',
                     'Limite':null,
                     'SumaAsegurada':'60000.00',
                     'Primas':'158.29',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'HOMICIDIO DOLOSO Y CULPOSO',
                     'Limite':null,
                     'SumaAsegurada':'60000.00',
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'MUERTE EN VUELO COMERCIAL',
                     'Limite':null,
                     'SumaAsegurada':'60000.00',
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'DESMEMBRAMIENTO POR ACCIDENTE',
                     'Limite':null,
                     'SumaAsegurada':null,
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'INVALIDEZ TOTAL Y PERMANENTE POR ACCIDENTE',
                     'Limite':null,
                     'SumaAsegurada':'60000.00',
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'GASTOS FUNERARIOS ASEGURADO PRINCIPAL',
                     'Limite':null,
                     'SumaAsegurada':'3000.00',
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  },
                  { 
                     'Poliza':'13-04-760397-0',
                     'Core':'P',
                     'NumeroRiesgo':1,
                     'Cobertura':'GASTOS FUNERARIOS CONYUGE',
                     'Limite':null,
                     'SumaAsegurada':'3000.00',
                     'Primas':'0.00',
                     'Deducible':'0.00'
                  }
               ]
            }
         ]
      }
   ]
}";
                App.Token = "prueba";
                App.modeloDashboard = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ModeloDashboard>(datosPrueba);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
