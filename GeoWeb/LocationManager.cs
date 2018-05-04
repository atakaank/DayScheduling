using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;
using System.Net;

namespace GeoWeb
{

    public class DirectionLatLong
    {
        public string Duration { get; set; }
        public string SourceLat { get; set; }
        public string SourceLong { get; set; }
        public string DestinationLat { get; set; }
        public string DestinationLong { get; set; }
    }

    public class LocationManager
    {
        public DirectionLatLong getDirection(string placeAddressSource, string placeAddressDestination)
        {
            DirectionLatLong res = new DirectionLatLong();
            GoogleLocationService locationService = new GoogleLocationService("AIzaSyBjS9eZc7OB_Hk2PDalIJYGowUUeCFQvk4");
            AddressData addressSource = new AddressData();
            addressSource.Address = placeAddressSource;
            AddressData addressDest = new AddressData();
            addressDest.Address = placeAddressDestination;
            var p1 = locationService.GetLatLongFromAddress(addressSource); //addressSource
            var p2 = locationService.GetLatLongFromAddress(addressDest);
            Directions dir = new Directions();
            dir = locationService.GetDirections(addressSource, addressDest);
            res.Duration = dir.Duration;
            res.SourceLat = p1.Latitude.ToString().Replace(",","."); //null reference ??
            res.SourceLong = p1.Longitude.ToString().Replace(",", ".");
            res.DestinationLat  = p2.Latitude.ToString().Replace(",", ".");
            res.DestinationLong = p2.Longitude.ToString().Replace(",", ".");
            return res;
        }

    }
}