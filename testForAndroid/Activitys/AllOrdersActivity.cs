﻿using System;
using System.Collections.Generic;
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
        OrdersAlbum _mOrdersAlbum;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllOrdersLayout);
            

            _mOrdersAlbum = new OrdersAlbum();
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView2);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new AllOrdersAdapter(_mOrdersAlbum);
            mAdapter.ItemClick += OnItemClick;
            mRecyclerView.SetAdapter(mAdapter);
        }
        void OnItemClick (object sender, Order ticket)
        {
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
                // Locate and cache view references:
                DestinationCity = itemView.FindViewById<TextView>(Resource.Id.destinationCity);
                SourceCity = itemView.FindViewById<TextView>(Resource.Id.sourceCity);
                DepartureDate = itemView.FindViewById<TextView>(Resource.Id.departureDate);
                ArrivalDate = itemView.FindViewById<TextView>(Resource.Id.arrivalDate); 


                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }


        public class AllOrdersAdapter : RecyclerView.Adapter {
            public event EventHandler<Order> ItemClick;
            public OrdersAlbum MOrdersAlbum;

            public AllOrdersAdapter(OrdersAlbum ordersAlbum) {
                MOrdersAlbum = ordersAlbum;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.OrderCardView, parent, false);

                OrderViewHolder vh = new OrderViewHolder(itemView, OnClick);
                return vh;
            }

            // Fill in the contents of the photo card (invoked by the layout manager):
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
                    OrderViewHolder vh = holder as OrderViewHolder;
                

                    vh.DestinationCity.Text = MOrdersAlbum[position].DestinationCity;
                    vh.SourceCity.Text = MOrdersAlbum[position].SourceCity;
                    vh.DepartureDate.Text = MOrdersAlbum[position].DepartureDate.ToString();
                    vh.ArrivalDate.Text = MOrdersAlbum[position].ArrivalDate.ToString();

            }

            public override int ItemCount {
                get { return MOrdersAlbum.NumPhotos; }
            }

            void OnClick(int position) {
                ItemClick?.Invoke(this, MOrdersAlbum[position]);
            }
        }
        

    }
}