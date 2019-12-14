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

    public class OrdersList {
        public Cruises[] mOrders;

        public OrdersList() {
            mOrders = GenerateOrders().ToArray();
        }

        public int NumPhotos => mOrders.Length; 
        

        public Cruises this[int i] => mOrders[i];

        public List<Cruises> GenerateOrders() {
            var cruiseTable = new AbstractTable<Cruises>();
            return cruiseTable.GetAllElements();
        }
        
    }
}
