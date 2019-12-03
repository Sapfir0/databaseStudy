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
            string departureDateTime = Intent.GetStringExtra("departureDateTime");
            string arrivalDateTime = Intent.GetStringExtra("arrivalDateTime");
            FindViewById<TextView>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<TextView>(Resource.Id.destinationCity).Text = destinationCity;
            FindViewById<TextView>(Resource.Id.departureTime).Text = departureDateTime;
            FindViewById<TextView>(Resource.Id.arrivalTime).Text = arrivalDateTime;

            //var abs = new AbstractTable<Trains>();
            //var train = abs.GetElement(abs.CountOfElements() - 1).Number;

            var abs2 = new AbstractTable<TrainstationsDestination>();
            bool empty = abs2.isEmpty();
            var destTrain = abs2.GetElement(abs2.CountOfElements()).Name;


            var abs3 = new AbstractTable<TrainstationsSource>();
            var sourceTrain = abs3.GetElement(abs3.CountOfElements()).Name;

            FindViewById<TextView>(Resource.Id.TrainstationSource).Text = sourceTrain;
            FindViewById<TextView>(Resource.Id.TrainstationDestination).Text = destTrain;

            var toMainBtn = FindViewById<Button>(Resource.Id.toMainBtn);
            toMainBtn.Click += ToMainBtn_Click;
            
        }

        private void ToMainBtn_Click(object sender, EventArgs e) {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}