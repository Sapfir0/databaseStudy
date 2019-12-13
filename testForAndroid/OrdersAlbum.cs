using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace testForAndroid {
    // Photo: contains image resource ID and caption:
    public class Order {

        public int id;
        public string sourceCity;
        public string destinationCity;
        public DateTime departureDate;
        public DateTime arrivalDate;

        public int Id => id;

        public string SourceCity {
            get { return sourceCity; }
        }

        public string DestinationCity {
            get { return destinationCity; }
        }

        public DateTime DepartureDate {
            get { return departureDate; }
        }

        public DateTime ArrivalDate {
            get { return arrivalDate; }
        }
    }

    // Photo album: holds image resource IDs and caption:
    public class OrdersAlbum {
        // Built-in photo collection - this could be replaced with
        // a photo database:
        

        // Array of photos that make up the album:
        public Order[] mPhotos;

        // Random number generator for shuffling the photos:
        Random mRandom;

        // Create an instance copy of the built-in photo list and
        // create the random number generator:
        public OrdersAlbum() {
            mPhotos = GenerateOrders().ToArray();
            mRandom = new Random();
        }

        // Return the number of photos in the photo album:
        public int NumPhotos {
            get { return mPhotos.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public Order this[int i] {
            get { return mPhotos[i]; }
        }

        public List<Order> GenerateOrders() {
            
            var cruiseTable = new AbstractTable<Cruises>();
            var cruises = cruiseTable.GetAllCruisesWithId();
            var tempOrdersArray = new List<Order>();
            
            foreach (var item in cruises) {

                var Order = new Order();
                Order.arrivalDate = item.ArrivingTime;
                Order.departureDate = item.DepartureTime;
                Order.destinationCity = item.DestinationCity;
                Order.sourceCity = item.SourceCity;
                Order.id = item.Id;
                tempOrdersArray.Add(Order);
            }

            return tempOrdersArray;
        }
        
    }
}
