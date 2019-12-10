using System;
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
        PhotoAlbumAdapter mAdapter;
        PhotoAlbum mPhotoAlbum;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllOrdersLayout);
            

            mPhotoAlbum = new PhotoAlbum();
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView2);

            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);
            mRecyclerView.SetAdapter(mAdapter);


            var cruiseTable = new AbstractTable<Cruises>();
            var cruises = cruiseTable.GetAllCruisesWithId();

            //TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);


            //foreach (var item in cruises) {
            //    var cityTable = new AbstractTable<Cities>();
            //    var testing = cityTable.GetAllElements();
            //    var destCity = cityTable.GetElement(item.TrainstationDestinationId);
            //    var sourceCity = cityTable.GetElement(item.TrainstationSourceId);

            //    CreateRow(tableLayout, item.Id.ToString(), sourceCity.Name, destCity.Name,
            //              item.DepartureTime.ToString(), item.ArrivingTime.ToString());
            //}

            //SetContentView(tableLayout);

        }

        public class PhotoViewHolder : RecyclerView.ViewHolder {
            public TextView DestinationCity { get; private set; }
            public TextView SourceCity { get; private set; }
            public TextView DepartureDate { get; private set; }
            public TextView ArrivalDate { get; private set; }

            // Get references to the views defined in the CardView layout.
            public PhotoViewHolder(View itemView, Action<int> listener)
                : base(itemView) {
                // Locate and cache view references:
                DestinationCity = itemView.FindViewById<TextView>(Resource.Id.destinationCity);
                SourceCity = itemView.FindViewById<TextView>(Resource.Id.sourceCity);
                DepartureDate = itemView.FindViewById<TextView>(Resource.Id.departureDate);
                ArrivalDate = itemView.FindViewById<TextView>(Resource.Id.arrivalTime); //TODO СРОЧНО ТУТ ОШИБКА


                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }


        public class PhotoAlbumAdapter : RecyclerView.Adapter {
            public event EventHandler<int> ItemClick;
            public PhotoAlbum mPhotoAlbum;

            public PhotoAlbumAdapter(PhotoAlbum photoAlbum) {
                mPhotoAlbum = photoAlbum;
            }

            public override RecyclerView.ViewHolder
                OnCreateViewHolder(ViewGroup parent, int viewType) {
                
                View itemView = LayoutInflater.From(parent.Context).
                            Inflate(Resource.Layout.PhotoCardView, parent, false);

                PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
                return vh;
            }

            // Fill in the contents of the photo card (invoked by the layout manager):
            public override void
                OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
                    PhotoViewHolder vh = holder as PhotoViewHolder;

                    vh.DestinationCity.Text = mPhotoAlbum[position].DestinationCity;
                    vh.SourceCity.Text = mPhotoAlbum[position].SourceCity;
                    vh.DepartureDate.Text = mPhotoAlbum[position].DepartureDate.ToString();
                    vh.ArrivalDate.Text = mPhotoAlbum[position].ArrivalDate.ToString();

            }

            public override int ItemCount {
                get { return mPhotoAlbum.NumPhotos; }
            }

            void OnClick(int position) {
                ItemClick?.Invoke(this, position);
            }
        }


        //public void CreateRow(TableLayout tableLayout, string id, string sourceCity, string destCity, string departureDate, string arrivalDate) {
        //    var textList = new string[] { id, $"{sourceCity} {departureDate}", $"{destCity} {arrivalDate}" };
        //    var textViewList = new List<TextView>(textList.Length);
        //    foreach (var text in textList) {
        //        var tempView = new TextView(this);
        //        tempView.SetHeight(100);
        //        tempView.Text = text;
        //        textViewList.Add(tempView);
        //    }

        //    var idRow = new TableRow(this);
        //    idRow.AddView(textViewList[0]);

        //    var sourceRow = new TableRow(this);
        //    var destRow = new TableRow(this);

        //    sourceRow.AddView(textViewList[1]);
        //    destRow.AddView(textViewList[2]);


        //    var orderLayout = new LinearLayout(this) {
        //        Orientation = Orientation.Vertical,
        //    };
        //    orderLayout.LongClick += DeleteRow;
        //    orderLayout.AddView(idRow);
        //    orderLayout.AddView(sourceRow);
        //    orderLayout.AddView(destRow);

        //    tableLayout.AddView(orderLayout);
        //}

        //private void DeleteRow(object sender, EventArgs e) {
        //    Alert alert = new Alert();
        //    var orderLayout = (LinearLayout)sender;
        //    alert.OnConfirm += () => {
        //        TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout);

        //        var textView = (TableRow)orderLayout.GetChildAt(0);
        //        if (textView is null) return;

        //        var id = ((TextView)textView.GetChildAt(0)).Text;

        //        tableLayout.RemoveView(orderLayout);

        //        var cruise = new AbstractTable<Cruises>();
        //        cruise.Delete(Convert.ToInt32(id));
        //    };

        //    var sourceRow = ((TableRow)orderLayout.GetChildAt(1));
        //    var source = ((TextView)sourceRow.GetChildAt(0)).Text;

        //    var destRow = ((TableRow)orderLayout.GetChildAt(2));
        //    var dest = ((TextView)destRow.GetChildAt(0)).Text;

        //    alert.DisplayConfirm(this, "Удалить запись?", $"Будет удален заказанный билет \nиз {source} в {dest}");

        //}


    }
}