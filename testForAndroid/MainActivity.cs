using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

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
            /*var db = new SQLiteConnection(compltePath);
            db.CreateTables<Citys, Trains,
                Companys, Employees, Cruises>();
            db.CreateTables<TrainstationsSource, TrainstationsDestination>();
            */


            var autoCompleteOptions = new string[] {
              "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
              "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
              "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
            };
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, 
                Resource.Layout.list_item, autoCompleteOptions);
            var autoCompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteSourceCity);
            autoCompleteTextView.Adapter = autoCompleteAdapter;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

