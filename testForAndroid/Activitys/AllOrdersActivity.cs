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
    [Activity(Label = "Все заказы")]
    public class AllOrdersActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllOrdersLayout);


            var cruiseTable = new AbstractTable<Cruises>();
            var cruises = cruiseTable.GetAllCruisesWithId();
            //var cruisesTest = cruiseTable.GetAllElements();

            TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);

            
            foreach (var item in cruises) {
                var cityTable = new AbstractTable<Cities>();
                var test = cityTable.GetAllElements();
                var destCity = cityTable.GetElement(item.TrainstationDestinationId+1);
                var sourceCity = cityTable.GetElement(item.TrainstationSourceId+1);

                CreateRow(tableLayout, item.Id.ToString(), sourceCity.Name, destCity.Name, 
                          item.DepartureTime.Date.ToString(), item.ArrivingTime.Date.ToString());
            }

            SetContentView(tableLayout);

        }

        public void CreateRow(TableLayout tableLayout, string id, string sourceCity, string destCity, string departureDate, string arrivalDate) {
            TextView textView0 = new TextView(this);
            textView0.Text = id;
            TextView textView1 = new TextView(this);
            textView1.Text = sourceCity;
            TextView textView2 = new TextView(this);
            textView2.Text = destCity;
            TextView textView3 = new TextView(this);
            textView3.Text = departureDate;
            TextView textView4 = new TextView(this);
            textView4.Text = arrivalDate;

            TableRow tableRow1 = new TableRow(this);

            tableRow1.AddView(textView0);
            tableRow1.AddView(textView1);
            tableRow1.AddView(textView2);
            tableRow1.AddView(textView3);
            tableRow1.AddView(textView4);

            tableRow1.LongClick += DeleteRow;
            tableLayout.AddView(tableRow1);

        }

        TableRow currentRow;
        private void DeleteRow(object sender, EventArgs e) {
            Alert alert = new Alert();
            currentRow = (TableRow)sender;
            alert.OnConfirm += Alert_OnConfirm;

            string sourceCity = ((TextView)currentRow.GetChildAt(1)).Text;
            string destCity = ((TextView)currentRow.GetChildAt(2)).Text;

            alert.DisplayConfirm(this, "Удалить запись?", $"Будет удален заказанный билет \nиз {sourceCity} в {destCity}");

        }

        private void Alert_OnConfirm() {
            TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            
            string id = ((TextView)currentRow.GetChildAt(0)).Text;
            tableLayout.RemoveView(currentRow);

            var cruise = new AbstractTable<Cruises>();
            cruise.Delete(Convert.ToInt32(id));
        }
    }
}