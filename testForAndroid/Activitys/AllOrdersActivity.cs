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
            SetContentView(Resource.Layout.AllOrders);

            var database = new AbstractTable<Cities>();
            var newList = database.GetAllElements();
            List<string> stringCityes = new List<string>();
            for (int i = 0; i < newList.Count; i++) {
                stringCityes.Add(newList[i].name);
            }
            var ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, stringCityes);

            var autoCompleteSourceCityView = FindViewById<TextView>(Resource.Id.sourceCity);
            autoCompleteSourceCityView.Adapter = ListAdapter;
        }
    }
}