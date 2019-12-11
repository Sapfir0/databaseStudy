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
                var cityTable = new AbstractTable<Cities>();
                var testing = cityTable.GetAllElements();
                var destCity = cityTable.GetElement(item.TrainstationDestinationId);
                var sourceCity = cityTable.GetElement(item.TrainstationSourceId);

                var Order = new Order();
                Order.arrivalDate = item.ArrivingTime;
                Order.departureDate = item.DepartureTime;
                Order.destinationCity = destCity.Name;
                Order.sourceCity = sourceCity.Name;
                Order.id = item.Id;
                tempOrdersArray.Add(Order);
            }

            return tempOrdersArray;
        }

        // Pick a random photo and swap it with the top:
        public int RandomSwap() {
            // Save the photo at the top:
            Order tmpOrder = mPhotos[0];

            // Generate a next random index between 1 and 
            // Length (noninclusive):
            int rnd = mRandom.Next(1, mPhotos.Length);

            // Exchange top photo with randomly-chosen photo:
            mPhotos[0] = mPhotos[rnd];
            mPhotos[rnd] = tmpOrder;

            // Return the index of which photo was swapped with the top:
            return rnd;
        }

        // Shuffle the order of the photos:
        public void Shuffle() {
            // Use the Fisher-Yates shuffle algorithm:
            for (int idx = 0; idx < mPhotos.Length; ++idx) {
                // Save the photo at idx:
                Order tmpOrder = mPhotos[idx];

                // Generate a next random index between idx (inclusive) and 
                // Length (noninclusive):
                int rnd = mRandom.Next(idx, mPhotos.Length);

                // Exchange photo at idx with randomly-chosen (later) photo:
                mPhotos[idx] = mPhotos[rnd];
                mPhotos[rnd] = tmpOrder;
            }
        }
        
    }
}
