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
        private readonly Cruises[] _mOrders;

        public OrdersList() {
            _mOrders = GenerateOrders().ToArray();
        }

        public int NumPhotos => _mOrders.Length; 
        

        public Cruises this[int i] => _mOrders[i];

        private List<Cruises> GenerateOrders() {
            var cruiseTable = new AbstractTable<Cruises>();
            return cruiseTable.GetAllElements();
        }
        
    }
}
