using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using controlWork;
using SQLite;
using System.Collections.Generic;
using Android.Content;


namespace testForAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            string databaseName = "database.db";
            string databasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(databasePath, databaseName);
            var db = new SQLiteConnection(completePath);

            db.CreateTables<Citys, Trains,
                Companys, Employees, Cruises>();
            db.CreateTables<TrainstationsSource, TrainstationsDestination>();



            var autoCompleteOptions = new string[] {
              "Москва", "Волгоград", "Екатеринбург", "Санкт-Петербург", "Воронеж"
            };
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this,
                Resource.Layout.list_item, autoCompleteOptions);
            var autoCompleteSourceCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteSourceCity);
            autoCompleteSourceCityView.Adapter = autoCompleteAdapter;


            var autoCompleteDestinationCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteDestinationCity);
            autoCompleteDestinationCityView.Adapter = autoCompleteAdapter;

            Button goToSetTimeToTicket = FindViewById<Button>(Resource.Id.goSetTimeBtn);
            goToSetTimeToTicket.Click += ToSetTimeToTicket;


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public void DisplayAlert(string title, string message)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Title");
            alert.SetMessage("Simple Alert");
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
        }


        public void ToSetTimeToTicket(object sender, EventArgs e)
        {
            var userObject = AutoCompleteDestinationCityView_OnClicked();
            if (userObject.destinationCity is null)
            {
                Console.WriteLine("дест сити пустой");
            }
            else if (userObject.sourceCity is null)
            {
                Console.WriteLine("сорс сити пустой");
                DisplayAlert("Error", "Убери точку", "Я понял");

            }
            else
            {
                var intent = new Intent(this, typeof(SetTimeTicketActivity));
                intent.PutExtra("destinationCity", userObject.destinationCity);
                intent.PutExtra("sourceCity", userObject.sourceCity);

                StartActivity(intent);
            }

        }

        // думаю скоро эта функция будет выброшена
        private UserObject AutoCompleteDestinationCityView_OnClicked()
        {
            string autoCompleteDestinationCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteDestinationCity).Text.ToString();
            string autoCompleteSourceCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteSourceCity).Text.ToString();

            UserObject info = new UserObject(); // должен быть синглтоном для юзера

            info.destinationCity = autoCompleteSourceCityView;
            info.sourceCity = autoCompleteDestinationCityView;
            Console.WriteLine(info.destinationCity);

            return info;
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

