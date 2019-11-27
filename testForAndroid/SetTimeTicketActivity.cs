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

namespace testForAndroid
{
    [Activity(Label = "SetTimeTicketActivity")]
    public class SetTimeTicketActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetTimeTicketLayout);

            String sourceCity = Intent.GetStringExtra("sourceCity");
            String destinationCity = Intent.GetStringExtra("destinationCity");

            //var sourceCityLabel = FindViewById<EditText>(Resource.Id.sourceCity);
            //var destinationCityLabel = FindViewById<EditText>(Resource.Id.destinationCity);

            //sourceCityLabel.Text = sourceCity;
            //destinationCityLabel.Text = destinationCity;


            //Button setArrivalTime = FindViewById<Button>(Resource.Id.setArrivalTime);
            //setArrivalTime.Click += ToSetArrivalTimeToTicket;

            //Button setDepartureTime = FindViewById<Button>(Resource.Id.setDepartureTime);
            //setDepartureTime.Click += ToSetDepartureTimeToTicket;

        }

        //public void ToSetArrivalTimeToTicket(object sender, EventArgs e) {
        //    var intent = new Intent(this, typeof(SetArrivalTimeActivity));
        //    StartActivity(intent);
        //}

        //public void ToSetDepartureTimeToTicket(object sender, EventArgs e) {
        //    var intent = new Intent(this, typeof(SetDepartureTimeActivity));
        //    StartActivity(intent);
        //}
    }
}