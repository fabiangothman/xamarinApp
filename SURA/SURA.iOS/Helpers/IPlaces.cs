using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Google.Places;
using Google.Maps;
using SURA.Services;
using SURA.iOS.Helpers;
using System.Threading.Tasks;
using SURA.Models;
using CoreLocation;
using Xamarin.Forms;
using MvvmCross.Platform.iOS;
using System.IO;
using System.Drawing;

[assembly: Xamarin.Forms.Dependency(
          typeof(IPlaces))]
namespace SURA.iOS.Helpers
{
    public class IPlaces : UIViewController, IServicioGooglePlaces
    {
        PlacesClient placesClient;
        AutocompleteSessionToken token;

        public IPlaces()
        {
        }

        async Task<string> IServicioGooglePlaces.GetPlaces(string lugar)
        {
            token = new AutocompleteSessionToken();
            string placeid = "";
            var termina = new TaskCompletionSource<string>();
            var Filter = new AutocompleteFilter();
            var neBounds = new CLLocationCoordinate2D(latitude: 10.467171, longitude: -83.243457);
            var swBounds = new CLLocationCoordinate2D(latitude: 6.207067, longitude: -77.327319);
            var Bounds = new CoordinateBounds(coord1: neBounds, coord2: swBounds);
            Filter.Country = "PA";

            PlacesClient.SharedInstance.Autocomplete(lugar, Bounds, Filter, AutocompletePredictionsHandler);

            void AutocompletePredictionsHandler(AutocompletePrediction[] results, NSError error)
            {
                if (error != null)
                {
                    Console.WriteLine($"Autocomplete error: {error.LocalizedDescription}");
                    termina.TrySetResult(string.Empty);
                    return;
                }

                placeid = results.FirstOrDefault().PlaceId;
                termina.TrySetResult(placeid);
            }

            return await termina.Task;
        }

        public async Task<ModeloResultadoDetalle> GetDetails(string placeid)
        {
            ModeloResultadoDetalle modeloResultadoDetalle = new ModeloResultadoDetalle();
            var termino = new TaskCompletionSource<ModeloResultadoDetalle>();
            PlaceField fields = (PlaceField.Name | PlaceField.FormattedAddress | PlaceField.PhoneNumber | PlaceField.OpeningHours | PlaceField.Photos);
            PlacesClient.SharedInstance.FetchPlace(placeid, fields, token, PlaceResultHandler);

            void PlaceResultHandler(Place place, NSError error)
            {
                if (error != null)
                {
                    Console.WriteLine($"Look up place id query error: {error.LocalizedDescription}");
                    //throw new Exception();
                    return;
                }

                if (place == null)
                {
                    Console.WriteLine($"No place details");
                    //throw new Exception();
                    return;
                }

                PlacesClient.SharedInstance.LookUpPhotos(placeid, PlacePhotoMetadataResultHandler);
                void PlacePhotoMetadataResultHandler(PlacePhotoMetadataList photos, NSError error2)
                {
                    if (error2 != null)
                    {
                        Console.WriteLine($"Error: {error2.LocalizedDescription}");
                        return;
                    }

                    var firstPhoto = photos.Results.FirstOrDefault();
                    if (firstPhoto != null)
                    {
                        LoadImage(firstPhoto);
                    }

                }

                void LoadImage(PlacePhotoMetadata photoMetadata)
                {
                    PlacesClient.SharedInstance.LoadPlacePhoto(photoMetadata, PlacePhotoImageResultHandler);

                    void PlacePhotoImageResultHandler(UIImage photo, NSError error3)
                    {
                        if (error3 != null)
                        {
                            Console.WriteLine($"Error: {error3.LocalizedDescription}");
                            return;
                        }

                        Byte[] bytes;

                        using (NSData imageData = photo.AsPNG())
                        {
                            bytes = new Byte[imageData.Length];
                            System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                        }

                        NSRange outRange;
                        var dict = photoMetadata.Attributions.GetAttributes(0,out outRange);
                        
                        var attribution = dict.Values.Where(x => x is NSUrl).FirstOrDefault();

                       

                        if (place.OpeningHours != null)
                        {
                            modeloResultadoDetalle = new ModeloResultadoDetalle
                            {
                                PlaceId = place.Id,
                                FormattedAddress = place.FormattedAddress,
                                FormattedPhoneNumber = place.PhoneNumber,
                                Name = place.Name,
                                WeekdayText = place.OpeningHours.WeekdayText.ToList(),
                                Photos = bytes,
                                photoAttribution = new ModeloResultadoDetalle.PhotoAttribution
                                {
                                    PhotoName = photoMetadata.Attributions.Value,
                                    PhotoUrl = attribution.ToString()
                                }
                            };
                            termino.TrySetResult(modeloResultadoDetalle);
                            
                        }
                        else
                        {
                            modeloResultadoDetalle = new ModeloResultadoDetalle
                            {
                                PlaceId = place.Id,
                                FormattedAddress = place.FormattedAddress,
                                FormattedPhoneNumber = place.PhoneNumber,
                                Name = place.Name,
                                Photos = bytes,
                                photoAttribution = new ModeloResultadoDetalle.PhotoAttribution
                                {
                                    PhotoName = photoMetadata.Attributions.Value,
                                    PhotoUrl = attribution.ToString()
                                }
                            };
                            termino.TrySetResult(modeloResultadoDetalle);
                        }
                    }
                }
            }
            return await termino.Task;
        }
    }
}