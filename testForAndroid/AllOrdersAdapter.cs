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

namespace testForAndroid {
    public class AbstractAdapter : BaseAdapter<string> {
        string[] Items;
        Activity Context;
        int View; // TextView
        int Layout; // Resource.Layout

        public AbstractAdapter(Activity context, int layout, int view, string[] items) : base() {
            View = view;
            Layout = layout;
            Context = context;
            Items = items;
        }

        public override long GetItemId(int position) {
            return position;
        }
        public override string this[int position] {
            get { return Items[position]; }
        }
        public override int Count {
            get { return Items.Length; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            View view = convertView; // re-use an existing view, if one is supplied
            if (view == null) // otherwise create a new one
                view = Context.LayoutInflater.Inflate(Layout, null);
            // set view properties to reflect data for the given row
            view.FindViewById<TextView>(View).Text = Items[position];
            // return the view, populated with data, for display
            return view;
        }
    }
}