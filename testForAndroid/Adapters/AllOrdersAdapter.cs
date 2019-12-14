using System;
using Android.Support.V7.Widget;
using Android.Views;


namespace testForAndroid {
    public class AllOrdersAdapter : RecyclerView.Adapter {
            public event EventHandler<Cruises> ItemClick;
            public OrdersList MOrdersList;

            public AllOrdersAdapter(OrdersList ordersList) {
                MOrdersList = ordersList;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.OrderCardView, parent, false);

                AllOrdersActivity.OrderViewHolder vh = new AllOrdersActivity.OrderViewHolder(itemView, OnClick);
                return vh;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
                AllOrdersActivity.OrderViewHolder vh = holder as AllOrdersActivity.OrderViewHolder;
                    
                vh.DestinationCity.Text = MOrdersList[position].DestinationCity;
                vh.SourceCity.Text = MOrdersList[position].SourceCity;
                vh.DepartureDate.Text = MOrdersList[position].DepartureTime.ToString();
                vh.ArrivalDate.Text = MOrdersList[position].ArrivingTime.ToString();
            }

            public override int ItemCount => MOrdersList.NumPhotos;

            void OnClick(int position) {
                ItemClick?.Invoke(this, MOrdersList[position]);
            }
    }
}