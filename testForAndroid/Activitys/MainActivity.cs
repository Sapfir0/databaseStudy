using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Newtonsoft.Json;


namespace testForAndroid {
    [Activity(Label = "Бронирование билетов", 
        Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AbstractActivity {
        string allCitiesInfo;

        protected override void OnCreate(Bundle savedInstanceState) {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            AssetManager assets = Assets;
            using (StreamReader sr = new StreamReader(assets.Open("russian-cities.json"))) {
                allCitiesInfo = sr.ReadToEnd();
            }

            var cities = JsonConvert.DeserializeObject<List<CitiesListAdapter>>(allCitiesInfo); // можно наверно добавить лямбду, где десериализовать в лист строк, и братьтолько name

            List<string> stringCities = new List<string>();
            for (int i = 0; i < cities.Count; i++) {
                stringCities.Add(cities[i].Name);
            }
            //AbstractTable<Cities>.DeleteAll();
            //InitDB(stringCities);


            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Resource.Layout.autoCompleteCities, stringCities);
            var autoCompleteSourceCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteSourceCity);
            autoCompleteSourceCityView.Adapter = autoCompleteAdapter;


            var autoCompleteDestinationCityView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteDestinationCity);
            autoCompleteDestinationCityView.Adapter = autoCompleteAdapter;
            Button goToSetTimeToTicket = FindViewById<Button>(Resource.Id.goSetTimeBtn);
            goToSetTimeToTicket.Click += ToSetTimeToTicket;

        }


        public void ToSetTimeToTicket(object sender, EventArgs e) {
            string destinationCity = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteDestinationCity).Text.ToString();
            string sourceCity = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteSourceCity).Text.ToString();

            if (string.IsNullOrEmpty(sourceCity)) {
                Alert.DisplayAlert(this, "Error", "Укажи откуда едешь", "Я понял");
            } else if (string.IsNullOrEmpty(destinationCity)) {
                Alert.DisplayAlert(this, "Error", "Укажи куда едешь", "Я понял");
            }
            else if (!allCitiesInfo.Contains(sourceCity)) {
                Alert.DisplayAlert(this, "Error", "Некорректное название города отправления" + $"\"{sourceCity}\"", "Я понял");
            }
            else if(!allCitiesInfo.Contains(destinationCity)) {
                Alert.DisplayAlert(this, "Error", "Некорректное название города прибытия" + $"\"{destinationCity}\"", "Я понял");
            } else {
                var intent = new Intent(this, typeof(SetTimeTicketActivity));
                intent.PutExtra("destinationCity", destinationCity);
                intent.PutExtra("sourceCity", sourceCity);

                StartActivity(intent);
            }

        }


        public static void InitDB(List<string> cities) {
            var companyRow = new AbstractTable<Companys>();

            companyRow.NewRow.Number = 12345;
            int companyId = companyRow.InsertElement(); // мы не должны создавать каждый раз одинаковую компанию

            var crewRow = new AbstractTable<Crews>();
            int crewId = crewRow.InsertElement();

            var employeeRow = new AbstractTable<Employees>();
            employeeRow.NewRow.FirstName = "Васян";
            employeeRow.NewRow.LastName = "Васянович";
            employeeRow.NewRow.CrewId = crewId;
            employeeRow.NewRow.CompanyId = 0;

            var trainRow = new AbstractTable<Trains>();
            trainRow.NewRow.CompanyId = 0;
            trainRow.NewRow.Number = 75743;
            int trainId = trainRow.InsertElement();

            var cityRow = new AbstractTable<Cities>();
            foreach (var city in cities) {
                cityRow.NewRow.Name = city;
                cityRow.InsertElement();
            }

        }
    }
}

