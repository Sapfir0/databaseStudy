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

            var datetime = CombineDatePlusTime(departureDate, departureTime);
            if (datetime <= DateTime.Now) {
               Alert.DisplayAlert(this, "Ошибка", "Эта дата уже прошла");
                return;
            }


            var rand = new Random();

            for(int i = 0; i < rand.Next(3, 6); i++) {
                var arrivalDate = GenerateDateInRandomNumberOfDays(Convert.ToDateTime(GetDepartureTime()));
                var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
                var tableRow = new TableRow(this);
                var availableCruise = new TextView(this) {
                    Text = $" прибытие в {arrivalDate}"
                };

                var applyOrder = new Button(this) {
                    Text = "Удобно"
                };
                applyOrder.Click += ApplyOrderListener;

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
            var dateDisplay = FindViewById<EditText>(Resource.Id.departureDate);
            GetDatePicker(dateDisplay);
        }

        private DateTime CombineDatePlusTime(DateTime date, DateTime time) {
            var combineDate = date.AddHours(time.Hour).AddMinutes(time.Minute);
            return combineDate;
        }

        private DateTime GenerateDateInRandomNumberOfDays(DateTime date) {
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
                departureTime = Convert.ToDateTime(GetDepartureTime());
            } catch (FormatException) {
                Alert.DisplayAlert(this, "Error", "Дата/время не выбрано");
                return;
            }

            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();
            
            var button = (Button)sender;
            var tableRow = (TableRow)button.Parent;
            var date = ((TextView)tableRow.GetChildAt(0)).Text;
            string[] words = date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var arrivalDateTime = Convert.ToDateTime(words[2] + " " + words[3]);
            var departureDateTime = CombineDatePlusTime(departureDate, departureTime);
            WriteInDB(sourceCity, destinationCity, departureDateTime, arrivalDateTime);

            var intent = new Intent(this, typeof(SuccessLayoutActivity));
            intent.PutExtra("destinationCity", destinationCity);
            intent.PutExtra("sourceCity", sourceCity);
            intent.PutExtra("arrivalDateTime", words[2] + " " + words[3]);
            intent.PutExtra("departureDateTime", departureDateTime.ToString());

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
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (DateTime time) {
                    textView.Text = time.ToShortTimeString();
                });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

    }
}