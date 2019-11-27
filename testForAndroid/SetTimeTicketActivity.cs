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

            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");

            var sourceCityLabel = FindViewById<EditText>(Resource.Id.sourceCity);
            var destinationCityLabel = FindViewById<EditText>(Resource.Id.destinationCity);

            sourceCityLabel.Text = sourceCity;
            destinationCityLabel.Text = destinationCity;


            // пока не будем брать из ui инфу о времени
            Cities source = new Cities(sourceCity);
            Cities dest = new Cities(destinationCity);
            dest.InsertCity();
            source.InsertCity();

            //Toast.MakeText(this, databaseController.GetCity(dest.id).name,  ToastLength.Short).Show();
            //Toast.MakeText(this, databaseController.GetCity(source.id).name, ToastLength.Short).Show();

            //Cruises cruise = new Cruises();
            //cruise.departureTime = DateTime.Now;
            //cruise.arrivingTime = DateTime.Now.AddDays(-1);
            //databaseController.InsertCrus

            //Button setArrivalTime = FindViewById<Button>(Resource.Id.setArrivalTime);
            //setArrivalTime.Click += ToSetArrivalTimeToTicket;

            //Button setDepartureTime = FindViewById<Button>(Resource.Id.setDepartureTime);
            //setDepartureTime.Click += ToSetDepartureTimeToTicket;

        }


    }
}