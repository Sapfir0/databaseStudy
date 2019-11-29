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

            TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);

            // это мне не нравится
            for(int i=0; i<cruises.Count; i++) {
                TextView textView1 = new TextView(this);
                textView1.Text = sourceCities[i];
                TextView textView2 = new TextView(this);
                textView2.Text = destCities[i];
                TextView textView3 = new TextView(this);
                textView3.Text = departureDate[i];
                TextView textView4 = new TextView(this);
                textView4.Text = arrivalDate[i];

                TableRow tableRow1 = new TableRow(this);

                tableRow1.AddView(textView1);
                tableRow1.AddView(textView2);
                tableRow1.AddView(textView3);
                tableRow1.AddView(textView4);

                tableLayout.AddView(tableRow1);

            }





            SetContentView(tableLayout);


        }
    }
}