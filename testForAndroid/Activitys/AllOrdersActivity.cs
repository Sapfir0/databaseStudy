using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace testForAndroid {
    [Activity(Label = "Все заказы")]
    public class AllOrdersActivity : Activity {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        AllOrdersAdapter mAdapter;
        OrdersList _mOrdersList;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllOrdersLayout);
            

            _mOrdersList = new OrdersList();
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView2);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new AllOrdersAdapter(_mOrdersList);
            mAdapter.ItemClick += OnItemClick;
            mRecyclerView.SetAdapter(mAdapter);
        }
        void OnItemClick (object sender, Cruises ticket) {
            Alert alert = new Alert();
            alert.OnConfirm += () => {
                //tableLayout.RemoveView(orderLayout);
                var cruise = new AbstractTable<Cruises>();
                cruise.Delete(Convert.ToInt32(ticket.Id));

                mAdapter.NotifyDataSetChanged();
            };
            alert.DisplayConfirm(this, "Удалить запись?", $"Будет удален заказанный билет \nиз {ticket.SourceCity} в {ticket.DestinationCity}");
        }

        public class OrderViewHolder : RecyclerView.ViewHolder {
            public TextView DestinationCity { get; private set; }
            public TextView SourceCity { get; private set; }
            public TextView DepartureDate { get; private set; }
            public TextView ArrivalDate { get; private set; }

            // Get references to the views defined in the CardView layout.
            public OrderViewHolder(View itemView, Action<int> listener) : base(itemView) {
                DestinationCity = itemView.FindViewById<TextView>(Resource.Id.destinationCity);
                SourceCity = itemView.FindViewById<TextView>(Resource.Id.sourceCity);
                DepartureDate = itemView.FindViewById<TextView>(Resource.Id.departureDate);
                ArrivalDate = itemView.FindViewById<TextView>(Resource.Id.arrivalDate); 

                itemView.Click += (sender, e) => listener(LayoutPosition);
            }
        }



        

    }
}