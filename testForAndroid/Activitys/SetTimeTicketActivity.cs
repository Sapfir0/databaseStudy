﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;


namespace testForAndroid {
    [Activity(Label = "Выбрать время", Theme = "@style/AppTheme.NoActionBar")]
    public class SetTimeTicketActivity : AbstractActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SetTimeTicketLayoutAbstract);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");

            FindViewById<TextView>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<TextView>(Resource.Id.destinationCity).Text = destinationCity;


            Button setDepartureDateBtn = FindViewById<Button>(Resource.Id.setDepartureDateButton);
            setDepartureDateBtn.Click += SetDepartureDateButtonListener;

            Button setDepartureTimeBtn = FindViewById<Button>(Resource.Id.setDepartureTimeButton);
            setDepartureTimeBtn.Click += SetDepartureTimeButtonListener;

            var departureDate = FindViewById<EditText>(Resource.Id.departureDate);
            departureDate.AfterTextChanged += DepartureDateTime_AfterTextChanged;
            var departureTime = FindViewById<EditText>(Resource.Id.departureTime);
            departureTime.AfterTextChanged += DepartureDateTime_AfterTextChanged;

        }

        private void DepartureDateTime_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e) {
            DateTime departureDate;
            DateTime departureTime;
            try {
                departureDate = Convert.ToDateTime(GetDepartureDate());
                departureTime = Convert.ToDateTime(GetDepartureTime());

            } catch (FormatException) {
                return;
            }
            
            var _dateDisplay = FindViewById<EditText>(Resource.Id.departureDate);
            var _timeDisplay = FindViewById<EditText>(Resource.Id.departureTime);


            var rand = new Random();
            var cruises = new AbstractTable<Cruises>();

            for(int i = 0; i < rand.Next(3, 6); i++) {
                var arrivalDate = GenerateRandomCruises(GetSourceCity(), GetDestinationCity(), _dateDisplay.Text, _timeDisplay.Text);
                var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
                var tableRow = new TableRow(this);
                var availableCruise = new TextView(this) {
                    Text = $" прибытие в {arrivalDate}"
                };

                var cruiseId = new TextView(this) {
                    Text = cruises.CountOfElements().ToString()
                };

                var applyOrder = new Button(this) {
                    Text = "Удобно"
                };
                applyOrder.Click += ApplyOrderListener;

                tableRow.AddView(cruiseId);
                tableRow.AddView(availableCruise);
                tableRow.AddView(applyOrder);
                tableLayout.AddView(tableRow);
            }

        }

        private void SetDepartureTimeButtonListener(object sender, EventArgs e) {
            var timeDisplay = FindViewById<EditText>(Resource.Id.departureTime);
            GetTimePicker(timeDisplay);
        }

        public void SetDepartureDateButtonListener(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<EditText>(Resource.Id.departureDate);
            GetDatePicker(_dateDisplay);
        }

        public DateTime GenerateRandomCruises(string sourceCity, string destCity, string departureDate, string departureTime) {
            var time = Convert.ToDateTime(departureTime);
            var date = Convert.ToDateTime(departureDate);

            var cruiseTable = new AbstractTable<Cruises>();

            var combineDate = date.AddHours(time.Hour).AddMinutes(time.Minute);
            cruiseTable.NewRow.DepartureTime = combineDate;
            cruiseTable.NewRow.ArrivingTime = GenerateDateInRandomNumberOfDays(cruiseTable.NewRow.DepartureTime);
            cruiseTable.NewRow.DestinationCity = destCity;
            cruiseTable.NewRow.SourceCity = sourceCity;


            cruiseTable.InsertElement();
            return cruiseTable.NewRow.ArrivingTime;
        }

        public DateTime GenerateDateInRandomNumberOfDays(DateTime date) {
            Random rand = new Random();
            DateTime newDate = date;
            newDate = newDate.AddHours(rand.Next(0, 100));
            newDate = newDate.AddMinutes(rand.Next(0, 60));
            return newDate;
        }

        private string GetDepartureDate() {
            return FindViewById<TextView>(Resource.Id.departureDate).Text;
        }

        private string GetDepartureTime() {
            return FindViewById<TextView>(Resource.Id.departureTime).Text;
        }

        private string GetSourceCity() {
            return FindViewById<TextView>(Resource.Id.sourceCity).Text;
        }

        private string GetDestinationCity() {
            return FindViewById<TextView>(Resource.Id.destinationCity).Text;
        }

        public void ApplyOrderListener(object sender, EventArgs e) {
            DateTime departureDate;
            DateTime departureTime;
            try {
                departureDate = Convert.ToDateTime(GetDepartureDate());
                departureTime = Convert.ToDateTime(GetDepartureDate());

            } catch (FormatException) {
                Alert.DisplayAlert(this, "Error", "Дата/время не выбрано");
                return;
            }

            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();


            var button = (Button)sender;
            var tableRow = (TableRow)button.Parent;
            var dateView = (TextView)tableRow.GetChildAt(1);
            var date = dateView.Text;
            string[] words = date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


            var cruiseId = ((TextView)tableRow.GetChildAt(0)).Text;
            var user = new AbstractTable<User>();
            user.NewRow.CruiseId = Convert.ToInt32(cruiseId) ;
            user.InsertElement();

            var intent = new Intent(this, typeof(SuccessLayoutActivity));
            intent.PutExtra("destinationCity", destinationCity);
            intent.PutExtra("sourceCity", sourceCity);
            intent.PutExtra("arrivalDateTime", words[2] + " " + words[3]);
            intent.PutExtra("departureDate", departureDate.Date.ToString());
            intent.PutExtra("departureTime", departureTime.TimeOfDay.ToString());

            StartActivity(intent);

        }


        // ахах
        public void WriteInDB(string sourceCity, string destinationCity, DateTime departureDateTime, DateTime arrivalDateTime) { //TODO поработать над функцией
            // записываем город в таблицу городов, если такого еще нет
            // создать TrainstationsSource/Destination, название вокзала можно просто генерить из названия города + "1" или "Пассажирский"
            // CityId будет ссылаться на город с тем же названием
            // создать компанию с рандомным номером(можно например РЖД и использовать его id везде)
            // сгенерить несколько сотдников(запонмить сколько сгенерилось), и создать команду
            // создать поезд с рандомным номером и юзнуть айди компании, запомнить айди поезда
            // !!! создать рейс с айди поезда, айди команды, айди двух вокзалов и выставить время от юзера     
           
            var cruiseRow = new AbstractTable<Cruises>();
            cruiseRow.NewRow.ArrivingTime = arrivalDateTime;
            cruiseRow.NewRow.DepartureTime = departureDateTime;

            cruiseRow.NewRow.DestinationCity = destinationCity;
            cruiseRow.NewRow.SourceCity = sourceCity;
            cruiseRow.InsertElement();

        }

        public void GetDatePicker(TextView textView) { // плохо, что эта функция здесь
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                textView.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public void GetTimePicker(TextView textView) {
            // Instantiate a TimePickerFragment (defined below) 
            TimePickerFragment frag = TimePickerFragment.NewInstance(

                // Create and pass in a delegate that updates the Activity time display 
                // with the passed-in time value:
                delegate (DateTime time) {
                    textView.Text = time.ToShortTimeString();
                });

            // Launch the TimePicker dialog fragment (defined below):
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

    }
}