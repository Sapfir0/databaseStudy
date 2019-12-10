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

            TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);

            
            foreach (var item in cruises) {
                var cityTable = new AbstractTable<Cities>();
                var testing = cityTable.GetAllElements();
                var destCity = cityTable.GetElement(item.TrainstationDestinationId);
                var sourceCity = cityTable.GetElement(item.TrainstationSourceId);

                CreateRow(tableLayout, item.Id.ToString(), sourceCity.Name, destCity.Name, 
                          item.DepartureTime.ToString(), item.ArrivingTime.ToString());
            }

            SetContentView(tableLayout);

        }

        public void CreateRow(TableLayout tableLayout, string id, string sourceCity, string destCity, string departureDate, string arrivalDate)  {
            var textList = new string[] { id, $"{sourceCity} {departureDate}", $"{destCity} {arrivalDate}" };
            var textViewList = new List<TextView>(textList.Length);
            foreach (var text in textList)  {
                var tempView = new TextView(this);
                tempView.SetHeight(100);
                tempView.Text = text;
                textViewList.Add(tempView);
            }

            var idRow = new TableRow(this);
            idRow.AddView(textViewList[0]);
            
            var sourceRow = new TableRow(this);
            var destRow = new TableRow(this);

            sourceRow.AddView(textViewList[1]);
            destRow.AddView(textViewList[2]);


            var orderLayout = new LinearLayout(this) {
                Orientation = Orientation.Vertical,
            };
            orderLayout.LongClick += DeleteRow;
            orderLayout.AddView(idRow);
            orderLayout.AddView(sourceRow);
            orderLayout.AddView(destRow);

            tableLayout.AddView(orderLayout);
        }

        private void DeleteRow(object sender, EventArgs e) {
            Alert alert = new Alert();
            var orderLayout = (LinearLayout)sender;
            alert.OnConfirm += () => {
                TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);

                var textView = (TableRow)orderLayout.GetChildAt(0);
                if (textView is null) return;

                var id = ((TextView)textView.GetChildAt(0)).Text;

                tableLayout.RemoveView(orderLayout);

                var cruise = new AbstractTable<Cruises>();
                cruise.Delete(Convert.ToInt32(id));
            };

            var sourceRow = ((TableRow)orderLayout.GetChildAt(1));
            var source = ((TextView)sourceRow.GetChildAt(0)).Text;
            
            var destRow = ((TableRow)orderLayout.GetChildAt(2));
            var dest = ((TextView)destRow.GetChildAt(0)).Text;

            alert.DisplayConfirm(this, "Удалить запись?", $"Будет удален заказанный билет \nиз {source} в {dest}");

        }

   
    }
}