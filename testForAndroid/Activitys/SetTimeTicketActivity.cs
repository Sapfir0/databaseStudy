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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class SetTimeTicketActivity : AbstractActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SetTimeTicketLayoutAbstract);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            string sourceCity = Intent.GetStringExtra("sourceCity");
            string destinationCity = Intent.GetStringExtra("destinationCity");

            FindViewById<EditText>(Resource.Id.sourceCity).Text = sourceCity;
            FindViewById<EditText>(Resource.Id.destinationCity).Text = destinationCity;


            Button setArrivalTimeBtn = FindViewById<Button>(Resource.Id.setArrivalDateTimeButton);
            setArrivalTimeBtn.Click += SetArrivalDateTimeButton;

            Button setDepartureTimeBtn = FindViewById<Button>(Resource.Id.setDepartureDateTimeButton);
            setDepartureTimeBtn.Click += SetDepartureDateTimeButtonListener;

            Button applyOrderBtn = FindViewById<Button>(Resource.Id.applyOrderBtn);
            applyOrderBtn.Click += ApplyOrderListener;
        }

        public void SetArrivalDateTimeButton(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<EditText>(Resource.Id.arrivalDateTime);
            GetDatePicker(_dateDisplay);
        }


        public void SetDepartureDateTimeButtonListener(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<EditText>(Resource.Id.departureDateTime);
            GetDatePicker(_dateDisplay);
        }

        private string GetDepartureDateTime() {
            return FindViewById<EditText>(Resource.Id.departureDateTime).Text;
        }

        private string GetArrivalDateTime() {
            return FindViewById<EditText>(Resource.Id.arrivalDateTime).Text; 
        }

        private string GetSourceCity() {
            return FindViewById<EditText>(Resource.Id.sourceCity).Text;
        }

        private string GetDestinationCity() {
            return FindViewById<EditText>(Resource.Id.destinationCity).Text;
        }

        public void ApplyOrderListener(object sender, EventArgs e) {
            DateTime arrivalDateTime;
            DateTime departureDateTime;
            try {
                departureDateTime = Convert.ToDateTime(GetDepartureDateTime());
                arrivalDateTime = Convert.ToDateTime(GetArrivalDateTime());
            } catch(FormatException) {
                Alert.DisplayAlert(this, "Error", "Дата не выбрана");
                return;
            }

            if (departureDateTime > arrivalDateTime) {
                Alert.DisplayAlert(this, "Error", "Дата прибытия должна быть позже даты отравления");
                return;
            }

            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();
            WriteInDB(sourceCity, destinationCity, departureDateTime, arrivalDateTime);


            var intent = new Intent(this, typeof(SuccessLayoutActivity));
            intent.PutExtra("destinationCity", destinationCity);
            intent.PutExtra("sourceCity", sourceCity);
            intent.PutExtra("arrivalDateTime", arrivalDateTime.ToString());
            intent.PutExtra("departureDateTime", departureDateTime.ToString());
            StartActivity(intent);

        }

        public void InitDB() {
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

            InitDB();
        

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

        protected override void OnSaveInstanceState(Bundle outState) {
            outState.PutString("departureDate", GetDepartureDateTime());
            outState.PutString("arrivalDate", GetArrivalDateTime());

            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState) { // не юзается вообще
            string departureDate = Intent.GetStringExtra("departureDate");
            string arrivalDate = Intent.GetStringExtra("arrivalDate");
            FindViewById<EditText>(Resource.Id.departureDateTime).Text = departureDate;
            FindViewById<EditText>(Resource.Id.arrivalDateTime).Text = arrivalDate;
            base.OnRestoreInstanceState(savedInstanceState);
        }

    }
}