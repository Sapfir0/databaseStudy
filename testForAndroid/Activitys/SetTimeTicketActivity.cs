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
            
            var departureDate = FindViewById<EditText>(Resource.Id.departureDate);
            departureDate.AfterTextChanged += DepartureDateTime_AfterTextChanged;
        }

        private void DepartureDateTime_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e) {
            DateTime departureDate;
            try {
                departureDate = Convert.ToDateTime(GetDepartureDate());

            } catch (FormatException) {
                return;
            }

            var rand = new Random();
 

            var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
            tableLayout.RemoveAllViewsInLayout();
            
            for (int i = 0; i < rand.Next(1,4); i++) {
                var sourceCruise = new TextView(this) {
                    Text = $"Отбытие {GenerateDateInCurrentDay(departureDate).TimeOfDay}"
                };

                var availableCruise = new TextView(this) {
                    Text = $"Прибытие в {GenerateDateInRandomNumberOfDays(departureDate)}"
                };

                var applyOrder = new Button(this) {
                    Text = "Удобно"
                };
                applyOrder.SetWidth(150);
                applyOrder.Click += ApplyOrderListener;


                var cruisesLayout = new LinearLayout(this);
                cruisesLayout.Orientation = Orientation.Vertical;

                cruisesLayout.AddView(sourceCruise);

                cruisesLayout.AddView(availableCruise);
                cruisesLayout.AddView(applyOrder);
                
                
                tableLayout.AddView(cruisesLayout);
            }

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

        private DateTime GenerateDateInCurrentDay(DateTime datetime) {
            Random rand = new Random();
            var hours = datetime.Hour;
            var minuts = datetime.Minute;

            var hoursBalance = 24 - (hours);
            var minuteBalance = 60 - minuts;

            DateTime newDate = datetime;

            newDate = newDate.AddHours(rand.Next(0, hoursBalance));
            newDate = newDate.AddMinutes(rand.Next(0, minuteBalance));
            return newDate;
        }

        private string GetDepartureDate() {
            return FindViewById<TextView>(Resource.Id.departureDate).Text;
        }
        
        private string GetSourceCity() {
            return FindViewById<TextView>(Resource.Id.sourceCity).Text;
        }

        private string GetDestinationCity() {
            return FindViewById<TextView>(Resource.Id.destinationCity).Text;
        }

        public void ApplyOrderListener(object sender, EventArgs e) {

            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();
            
            var button = (Button)sender;
            var cruiseLayout = (LinearLayout)button.Parent;
            var departureTimeView = ((TextView)cruiseLayout.GetChildAt(0)).Text;
            var arrivalTimeView = ((TextView)cruiseLayout.GetChildAt(1)).Text;

            string[] departureTimeWords = departureTimeView.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string[] arrivalTimeWords = arrivalTimeView.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var arrivalDateTime = Convert.ToDateTime(arrivalTimeWords[2] + " " + arrivalTimeWords[3]);

            var departureTime = Convert.ToDateTime(departureTimeWords[1]);
            var departureDate = Convert.ToDateTime(GetDepartureDate());
            var departureDateTime = CombineDatePlusTime(departureDate, departureTime);
            
            WriteInDB(sourceCity, destinationCity, departureDateTime, arrivalDateTime);
            
             var intent = new Intent(this, typeof(SuccessLayoutActivity));
             intent.PutExtra("destinationCity", destinationCity);
             intent.PutExtra("sourceCity", sourceCity);
             intent.PutExtra("arrivalDateTime", arrivalTimeWords[2] + " " + arrivalTimeWords[3]);
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

    }
}