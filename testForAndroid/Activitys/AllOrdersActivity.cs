using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace testForAndroid {
    [Activity(Label = "AllOrdersActivity")]
    public class AllOrdersActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllOrdersLayout);

            var database = new AbstractTable<Cities>();
            var newList = database.GetAllElements();
            List<string> stringCityes = new List<string>();
            for (int i = 0; i < newList.Count; i++) {
                stringCityes.Add(newList[i].Name);
            }

            var cruiseTable = new AbstractTable<Cruises>();
            List<Cruises> cruises = cruiseTable.GetAllElements();
            List<string> departureDate = new List<string>();
            List<string> arrivalDate = new List<string>();
            foreach (var item in cruises) {
                departureDate.Add(item.DepartureTime.Date.ToString());
                arrivalDate.Add(item.ArrivingTime.Date.ToString());
            }


            var listView = FindViewById<ListView>(Resource.Id.listView);
            var BaseExpandableListAdapter = new AllOrdersAdapter(this, stringCityes.ToArray());
            listView.Adapter = BaseExpandableListAdapter;

            var arrivalDateAdapter = new AllOrdersAdapter(this, arrivalDate.ToArray());
            var departureDateAdapter = new AllOrdersAdapter(this, departureDate.ToArray());

            listView.Adapter = arrivalDateAdapter;
            listView.Adapter = departureDateAdapter;

            //foreach (var item in cities) {
            //    Console.Write("Название города:", item.name);
            //    Console.Write(item.name);
            //    Console.Write("\n");
            //}

            //foreach (var item in cruises) {
            //    Console.Write("Время отбытия/прибытия");
            //    Console.Write(item.departureTime);
            //    Console.Write(item.arrivingTime);
            //    Console.Write("\n");
            //}
        }
    }
}