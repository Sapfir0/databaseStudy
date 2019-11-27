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
            var sourceCityTable = new AbstractTable<Cities>();
            sourceCityTable.tableConcrete = new Cities();
            sourceCityTable.tableConcrete.name = sourceCity;
            
            var destCityTable = new AbstractTable<Cities>();
            destCityTable.tableConcrete = new Cities();
            destCityTable.tableConcrete.name = destinationCity;

            destCityTable.InsertElement();
            sourceCityTable.InsertElement();

            //Toast.MakeText(this, source.GetCity(dest.id).name, ToastLength.Short).Show();
            //Toast.MakeText(this, dest.GetCity(source.id).name, ToastLength.Short).Show();

            Button setArrivalTime = FindViewById<Button>(Resource.Id.setArrivalDateTimeButton);
            setArrivalTime.Click += SetArrivalDateTimeButton;

            Button setDepartureTime = FindViewById<Button>(Resource.Id.setDepartureDateTimeButton);
            setDepartureTime.Click += SetDestinationDateTimeButton;

        }

        public void SetArrivalDateTimeButton(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<TextView>(Resource.Id.arrivalDateTime);
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                _dateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public void SetDestinationDateTimeButton(object sender, EventArgs e) {
            var _dateDisplay = FindViewById<TextView>(Resource.Id.departureDateTime);

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                _dateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public class DatePickerFragment : DialogFragment,
                          DatePickerDialog.IOnDateSetListener {
            // TAG can be any string of your choice.
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

            // Initialize this value to prevent NullReferenceExceptions.
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected) {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState) {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                               this,
                                                               currently.Year,
                                                               currently.Month - 1,
                                                               currently.Day);
                return dialog;
            }

            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                //Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }
        TextView _dateDisplay;



    }
}