using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SURA.Models;
using Xamarin.Forms;

namespace SURA.Services
{
    public interface IServicioGooglePlaces
    {
        Task<string> GetPlaces(string lugar);

        Task<ModeloResultadoDetalle> GetDetails(string placeid);
    }
}
