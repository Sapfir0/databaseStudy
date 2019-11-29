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

            //var bar = new AbstractTable<Cities>();
            //var foo= bar.GetAllElements();
            //foreach (var item in foo) {
            //    Console.WriteLine(item.Name);
            //}

            List<string> departureDate = new List<string>();
            List<string> arrivalDate = new List<string>();
            List<string> sourceCities = new List<string>();
            List<string> destCities = new List<string>();


            var cruiseTable = new AbstractTable<Cruises>();
            List<Cruises> cruises = cruiseTable.GetAllElements();
            
            foreach (var item in cruises) {
                departureDate.Add(item.DepartureTime.Date.ToString());
                arrivalDate.Add(item.ArrivingTime.Date.ToString());
                //var trainstationSTable = new AbstractTable<TrainstationsSource>();
                //var trainstationDTable = new AbstractTable<TrainstationsDestination>();
                //var destTrainstation = trainstationDTable.GetElement(item.TrainstationDestinationId);
                //var sourceTrainstation = trainstationSTable.GetElement(item.TrainstationSourceId);

                var cityTable = new AbstractTable<Cities>();
                var destCity = cityTable.GetElement(item.TrainstationDestinationId);
                var sourceCity = cityTable.GetElement(item.TrainstationSourceId);

                destCities.Add(destCity.Name);
                sourceCities.Add(sourceCity.Name);
            }



            var layout = Resource.Layout.AllOrdersLayoutForAdapterList;
            var sourceCityView = Resource.Id.sourceCity;
            var sourceCityListAdapter = new AbstractAdapter(this, layout, sourceCityView, sourceCities.ToArray());

            var destCityView = Resource.Id.destinationCity;
            var destCityListAdapter = new AbstractAdapter(this, layout, destCityView, destCities.ToArray());

            for (int i=0; i<cruises.Count; i++) {
                Console.Write(sourceCities[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < cruises.Count; i++) {
                Console.Write(destCities[i]);
            }


            var sourceCityListView = FindViewById<ListView>(Resource.Id.sourceCityListView);
            sourceCityListView.Adapter = sourceCityListAdapter;

            //var destCityListView = FindViewById<ListView>(Resource.Id.destCityListView);
            //destCityListView.Adapter = destCityListAdapter;

            //var arrivalDateAdapter = new AllOrdersAdapter(this, arrivalDate.ToArray());
            //var departureDateAdapter = new AllOrdersAdapter(this, departureDate.ToArray());

            //listView.Adapter = arrivalDateAdapter;
            //listView.Adapter = departureDateAdapter;

        }
    }
}