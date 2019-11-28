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
    public class SetTimeTicketActivity : AppCompatActivity {
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
            var sourceCity = GetSourceCity();
            var destinationCity = GetDestinationCity();
            var departureDateTime = GetDepartureDateTime();
            var arrivalDateTime = GetArrivalDateTime();

            DateTime arrivalDate;
            DateTime departureDate;
            try {
                arrivalDate = Convert.ToDateTime(arrivalDateTime);
                departureDate = Convert.ToDateTime(departureDateTime);
            } catch(FormatException) {
                Alert.DisplayAlert(this, "Error", "Дата не выбрана");
                return;
            }

            if (departureDate > arrivalDate) {
                Alert.DisplayAlert(this, "Error", "Дата прибытия должна быть позже даты отравления");
                return;
            }

            var sourceCityRow = new AbstractTable<Cities>();
            sourceCityRow.TableConcrete.Name = sourceCity;

            var destCityRow = new AbstractTable<Cities>();
            destCityRow.TableConcrete.Name = destinationCity;

            int destId = destCityRow.InsertElement();
            sourceCityRow.InsertElement();

            var cruiseTable = new AbstractTable<Cruises>();
            cruiseTable.TableConcrete.ArrivingTime = arrivalDate;
            cruiseTable.TableConcrete.DepartureTime = departureDate;

            cruiseTable.InsertElement();
                

            var intent = new Intent(this, typeof(SuccessLayoutActivity));
            StartActivity(intent);

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

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }



    }
}