using System;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;


namespace testForAndroid {
    public class AllOrdersAdapter : RecyclerView.Adapter {
            public event EventHandler<Cruises> ItemClick;
            private readonly OrdersList _mOrdersList;

            public AllOrdersAdapter(OrdersList ordersList) {
                _mOrdersList = ordersList;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
                var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.OrderCardView, parent, false);

                var vh = new AllOrdersActivity.OrderViewHolder(itemView, OnClick);
                return vh;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
                var vh = holder as AllOrdersActivity.OrderViewHolder;
                    
                vh.DestinationCity.Text = _mOrdersList[position].DestinationCity;
                vh.SourceCity.Text = _mOrdersList[position].SourceCity;
                vh.DepartureDate.Text = _mOrdersList[position].DepartureTime.ToString(CultureInfo.InvariantCulture);
                vh.ArrivalDate.Text = _mOrdersList[position].ArrivingTime.ToString(CultureInfo.InvariantCulture);
            }

            public override int ItemCount => _mOrdersList.NumPhotos;

            private void OnClick(int position) {
                ItemClick?.Invoke(this, _mOrdersList[position]);
            }
    }
}