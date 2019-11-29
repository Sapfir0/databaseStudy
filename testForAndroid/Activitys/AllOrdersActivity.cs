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

            List<string> departureDate = new List<string>();
            List<string> arrivalDate = new List<string>();
            List<string> sourceCities = new List<string>();
            List<string> destCities = new List<string>();


            var cruiseTable = new AbstractTable<Cruises>();
            List<Cruises> cruises = cruiseTable.GetAllElements();
            
            foreach (var item in cruises) {
                departureDate.Add(item.DepartureTime.Date.ToString());
                arrivalDate.Add(item.ArrivingTime.Date.ToString());

                var cityTable = new AbstractTable<Cities>();
                var destCity = cityTable.GetElement(item.TrainstationDestinationId); // допускаем, что в городе один вокзал
                var sourceCity = cityTable.GetElement(item.TrainstationSourceId);

                destCities.Add(destCity.Name);
                sourceCities.Add(sourceCity.Name);
            }


            var layout = Resource.Layout.AllOrdersLayoutForAdapterList;
            var sourceCityView = Resource.Id.sourceCity;
            var sourceCityListAdapter = new AbstractAdapter(this, layout, sourceCityView, sourceCities.ToArray());

            var destCityView = Resource.Id.destinationCity;
            var destCityListAdapter = new AbstractAdapter(this, layout, destCityView, destCities.ToArray());



            var sourceCityListView = FindViewById<ListView>(Resource.Id.sourceCityListView);
            sourceCityListView.Adapter = sourceCityListAdapter;

            var destCityListView = FindViewById<ListView>(Resource.Id.destCityListView);
            destCityListView.Adapter = destCityListAdapter;



        }
    }
}