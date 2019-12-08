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
    [Activity(Label = "Заказ оставлен")]
    public class SuccessLayoutActivity : AbstractActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SuccessLayout);


            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");
            string departureDate = Intent.GetStringExtra("departureDate");
            string departureTime = Intent.GetStringExtra("departureTime");

            string arrivalDateTime = Intent.GetStringExtra("arrivalDateTime");
            FindViewById<TextView>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<TextView>(Resource.Id.destinationCity).Text = destinationCity;
            FindViewById<TextView>(Resource.Id.departureTime).Text = $"{departureDate} {departureTime}";
            FindViewById<TextView>(Resource.Id.arrivalTime).Text = arrivalDateTime;


            FindViewById<TextView>(Resource.Id.TrainstationSource).Text = sourceCity + "1";
            FindViewById<TextView>(Resource.Id.TrainstationDestination).Text = destinationCity + "1";

            var toMainBtn = FindViewById<Button>(Resource.Id.toMainBtn);
            toMainBtn.Click += ToMainBtn_Click;
            
        }

        private void ToMainBtn_Click(object sender, EventArgs e) {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}