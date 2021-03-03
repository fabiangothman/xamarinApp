using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services
{
    public class NetworkAvailabilityService
    {
        public bool NetworkAvailabilityServiceMethod()
        {
            try
            {
                Xamarin.Essentials.NetworkAccess current = Xamarin.Essentials.Connectivity.NetworkAccess;

                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
