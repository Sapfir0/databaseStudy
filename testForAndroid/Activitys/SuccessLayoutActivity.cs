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
    [Activity(Label = "Заказ оставлен", Theme = "@style/AppTheme.NoActionBar")]
    public class SuccessLayoutActivity : AbstractActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SuccessLayout);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");
            string departureDateTime = Intent.GetStringExtra("departureDateTime");
            string arrivalDateTime = Intent.GetStringExtra("arrivalDateTime");
            FindViewById<TextView>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<TextView>(Resource.Id.destinationCity).Text = destinationCity;
            FindViewById<TextView>(Resource.Id.departureTime).Text = departureDateTime;
            FindViewById<TextView>(Resource.Id.arrivalTime).Text = arrivalDateTime;


            FindViewById<TextView>(Resource.Id.TrainstationSource).Text = sourceCity + "1";
            FindViewById<TextView>(Resource.Id.TrainstationDestination).Text = destinationCity + "1";

            var toMainBtn = FindViewById<Button>(Resource.Id.toMainBtn);
            toMainBtn.Click += ToMainBtn_Click;

            var toAllOrdersBtn = FindViewById<Button>(Resource.Id.toAllOrdersBtn);
            toAllOrdersBtn.Click += ToAllOrdersBtn_Click;
        }


        private void ToAllOrdersBtn_Click(object sender, EventArgs e) {
            var intent = new Intent(this, typeof(AllOrdersActivity));
            StartActivity(intent);
        }

        private void ToMainBtn_Click(object sender, EventArgs e) {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

    }
}