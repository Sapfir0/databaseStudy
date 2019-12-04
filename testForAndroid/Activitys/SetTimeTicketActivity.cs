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
            //InitDB();
            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");

            FindViewById<TextView>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<TextView>(Resource.Id.destinationCity).Text = destinationCity;


            Button setDepartureTimeBtn = FindViewById<Button>(Resource.Id.setDepartureDateTimeButton);
            setDepartureTimeBtn.Click += SetDepartureDateTimeButtonListener;

            var departureDateTime = FindViewById<EditText>(Resource.Id.departureDateTime);
            departureDateTime.AfterTextChanged += DepartureDateTime_AfterTextChanged; ;
        }

        private void DepartureDateTime_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e) {
            var sourceCity = FindViewById<TextView>(Resource.Id.sourceCity);
            var destCity = FindViewById<TextView>(Resource.Id.destinationCity);
            var _dateDisplay = FindViewById<EditText>(Resource.Id.departureDateTime);

            var cityTable = new AbstractTable<Cities>();
            cityTable.NewRow.Name = sourceCity.Text;
            int sourceCityId = cityTable.InsertElement();

            var cityTable2 = new AbstractTable<Cities>();
            cityTable2.NewRow.Name = destCity.Text;
            int destCityId = cityTable2.InsertElement();


            var rand = new Random();
            var cruises = new AbstractTable<Cruises>();

            for(int i=0; i< rand.Next(3, 6); i++) {
                var arrivalDate = GenerateRandomCruises(sourceCityId, destCityId, _dateDisplay.Text);
                var tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);
                var tableRow = new TableRow(this);
                var availableCruise = new TextView(this);
                availableCruise.Text = $" прибытие в {arrivalDate}";

                var cruiseId = new TextView(this);
                cruiseId.Text = cruises.CountOfElements().ToString();

                var applyOrder = new Button(this);
                applyOrder.Text = "Удобно";
                applyOrder.Click += ApplyOrderListener;

                tableRow.AddView(cruiseId);
                tableRow.AddView(availableCruise);
                tableRow.AddView(applyOrder);
                tableLayout.AddView(tableRow);
            }

        }



        public void SetDepartureDateTimeButtonListener(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<EditText>(Resource.Id.departureDateTime);
            GetDatePicker(_dateDisplay);
        }

        public DateTime GenerateRandomCruises(int sourceCityId, int destCityId, string departureDate) {


            var cruiseTable = new AbstractTable<Cruises>();
            cruiseTable.NewRow.DepartureTime = Convert.ToDateTime(departureDate);
            cruiseTable.NewRow.ArrivingTime = GenerateDateInRandomNumberOfDays(cruiseTable.NewRow.DepartureTime);
            cruiseTable.NewRow.TrainstationDestinationId = destCityId;
            cruiseTable.NewRow.TrainstationSourceId = sourceCityId;
            cruiseTable.NewRow.TrainId = 0;
            cruiseTable.NewRow.CrewId = 0;

            cruiseTable.InsertElement();
            return cruiseTable.NewRow.ArrivingTime;
        }

        public DateTime GenerateDateInRandomNumberOfDays(DateTime date) {
            Random rand = new Random();
            DateTime newDate = date;
            //newDate = newDate.AddDays(rand.Next(1, 3));
            newDate = newDate.AddHours(rand.Next(0, 100));
            newDate = newDate.AddMinutes(rand.Next(0, 60));
            return newDate;
        }

        private string GetDepartureDateTime() {
            return FindViewById<TextView>(Resource.Id.departureDateTime).Text;
        }

        private string GetSourceCity() {
            return FindViewById<TextView>(Resource.Id.sourceCity).Text;
        }

        private string GetDestinationCity() {
            return FindViewById<TextView>(Resource.Id.destinationCity).Text;
        }

        public void ApplyOrderListener(object sender, EventArgs e) {
            DateTime departureDateTime;
            try {
                departureDateTime = Convert.ToDateTime(GetDepartureDateTime());
            } catch(FormatException) {
                Alert.DisplayAlert(this, "Error", "Дата не выбрана");
                return;
            }

            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();


            var button = (Button)sender;
            var tableRow = (TableRow)button.Parent;
            var dateView = (TextView)tableRow.GetChildAt(1);
            var date = dateView.Text;
            string[] words = date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


            var cruiseTable = new AbstractTable<Cruises>();
            var foo = cruiseTable.GetAllElements();
            var cruiseId = ((TextView)tableRow.GetChildAt(0)).Text;
            var user = new AbstractTable<User>();
            user.NewRow.CruiseId = Convert.ToInt32(cruiseId) ;
            user.InsertElement();

            var intent = new Intent(this, typeof(SuccessLayoutActivity));
            intent.PutExtra("destinationCity", destinationCity);
            intent.PutExtra("sourceCity", sourceCity);
            intent.PutExtra("arrivalDateTime", words[2] + " " + words[3]);
            intent.PutExtra("departureDateTime", departureDateTime.ToString());
            StartActivity(intent);

        }

        public static void InitDB() {
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

            //InitDB();
        

            var sourceCityRow = new AbstractTable<Cities>();

            sourceCityRow.NewRow.Name = sourceCity;
            int sourceCityId = sourceCityRow.CountOfElements() + 1;
            sourceCityRow.InsertElement();

            var destCityRow = new AbstractTable<Cities>();
            destCityRow.NewRow.Name = destinationCity;
            int destCityId = destCityRow.CountOfElements() + 1;
            destCityRow.InsertElement();

            // я же аутест, я не понимаю как контролировать бд снаружи, а метод я однажды уже вызвал. Так что вызова больше не будет
            var destTrainstationRow = new AbstractTable<TrainstationsDestination>();  // мы не должны создавать каждый раз одинаковый вокзал
            destTrainstationRow.NewRow.Name = destinationCity + "1";
            destTrainstationRow.NewRow.CityId = destCityId;
            destTrainstationRow.InsertElement();

            var sourceTrainstationRow = new AbstractTable<TrainstationsSource>();
            sourceTrainstationRow.NewRow.Name = sourceCity + "2";
            sourceTrainstationRow.NewRow.CityId = sourceCityId;
            sourceTrainstationRow.InsertElement();
            // написать функцию инсерт if no exists
            
            var cruiseRow = new AbstractTable<Cruises>();
            cruiseRow.NewRow.ArrivingTime = arrivalDateTime;
            cruiseRow.NewRow.DepartureTime = departureDateTime;
            cruiseRow.NewRow.CrewId = 0;
            cruiseRow.NewRow.TrainId = 0; // ахах вот это лулз
            cruiseRow.NewRow.TrainstationDestinationId = destCityId;
            cruiseRow.NewRow.TrainstationSourceId = sourceCityId;
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