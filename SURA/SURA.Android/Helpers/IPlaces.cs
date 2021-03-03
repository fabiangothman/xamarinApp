using System;
using System.Threading.Tasks;
using Android.Content;
using Google.Places;
using Plugin.CurrentActivity;
using SURA.Droid.Helpers;
using SURA.Models;
using SURA.Services;
using Java.Lang;
using System.Linq;
using System.Collections.Generic;
using Android.Gms.Extensions;
using Android.Graphics;
using Android.Gms.Common.Apis;
using System.IO;
using HtmlAgilityPack;

[assembly: Xamarin.Forms.Dependency(typeof(IPlaces))]
namespace SURA.Droid.Helpers
{
    public class IPlaces : IServicioGooglePlaces
    {
        ModeloResultadoDetalle modeloResultadoDetalle;
        AutocompleteSessionToken token;
        Context context = CrossCurrentActivity.Current.Activity;
        IPlacesClient placesClient;

        public IPlaces()
        {
            if (!PlacesApi.IsInitialized)
            {
                PlacesApi.Initialize(context, Android.App.Application.Context.Resources.GetString(Resource.String.key));
                placesClient = PlacesApi.CreateClient(context);
            }            
        }

        public async Task<string> GetPlaces(string lugar)
        {
            string PlaceID = "";
            token = AutocompleteSessionToken.NewInstance();

            var request = FindAutocompletePredictionsRequest.InvokeBuilder()
            .SetSessionToken(token)
            .SetCountry("PA")
            .SetQuery(lugar)
            .Build();

            var PredictionsTask = await placesClient.FindAutocompletePredictions(request);

            try
            {
                var response = (FindAutocompletePredictionsResponse)PredictionsTask;
                PlaceID = response.AutocompletePredictions.FirstOrDefault().PlaceId;
            }
            catch(System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }
           
            return PlaceID;
        }


        public async Task<ModeloResultadoDetalle> GetDetails(string placeid)
        {

            List<Place.Field> fields = new List<Place.Field>();

            fields.Add(Place.Field.Id);
            fields.Add(Place.Field.Name);
            fields.Add(Place.Field.Address);
            fields.Add(Place.Field.PhoneNumber);
            fields.Add(Place.Field.OpeningHours);
            fields.Add(Place.Field.PhotoMetadatas);

            FetchPlaceRequest detailsRequest = FetchPlaceRequest.InvokeBuilder(placeid, fields)
                .SetSessionToken(token)
                .Build();

            var taskDetails = await placesClient.FetchPlace(detailsRequest);

            try
            {
                var detailsResponse = (FetchPlaceResponse)taskDetails;
                Place placeDetails = detailsResponse.Place;
                PhotoMetadata metadataFoto = detailsResponse.Place.PhotoMetadatas.First();

                FetchPhotoRequest photoRequest = FetchPhotoRequest.InvokeBuilder(metadataFoto)
                    .SetMaxWidth((Integer)300)
                    .SetMaxHeight((Integer)200)
                    .Build();

                var taskPhoto = await placesClient.FetchPhoto(photoRequest);

                var respuesta = (FetchPhotoResponse)taskPhoto;
                var bitmap = respuesta.Bitmap;
                byte[] bytes;

                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bytes = stream.ToArray();
                }

                var document = new HtmlDocument();
                document.LoadHtml(metadataFoto.Attributions);

                var AttributionLink = document.DocumentNode.ChildNodes[0].Attributes["href"].Value;
                var AttributionName = document.DocumentNode.InnerText;

                if (placeDetails.OpeningHours != null)
                {
                    modeloResultadoDetalle = new ModeloResultadoDetalle
                    {
                        PlaceId = placeDetails.Id,
                        FormattedAddress = placeDetails.Address,
                        FormattedPhoneNumber = placeDetails.PhoneNumber,
                        Name = placeDetails.Name,
                        WeekdayText = placeDetails.OpeningHours.WeekdayText.ToList(),
                        Photos = bytes,
                        photoAttribution = new ModeloResultadoDetalle.PhotoAttribution { PhotoUrl = AttributionLink, PhotoName=AttributionName}
                    };
                }
                else
                {
                    modeloResultadoDetalle = new ModeloResultadoDetalle
                    {
                        PlaceId = placeDetails.Id,
                        FormattedAddress = placeDetails.Address,
                        FormattedPhoneNumber = placeDetails.PhoneNumber,
                        Name = placeDetails.Name,
                        Photos = bytes,
                        photoAttribution = new ModeloResultadoDetalle.PhotoAttribution { PhotoUrl = AttributionLink , PhotoName = AttributionName }
                    };
                }

            }
            catch (System.Exception exception)
            {
                await App.Current.MainPage.DisplayAlert("SURA", exception.Message + ": Intente más tarde.", "Ok");
            }
   
            return modeloResultadoDetalle;
        }
    }
}