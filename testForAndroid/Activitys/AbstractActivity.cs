using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;


namespace testForAndroid {
    [Activity(Label = "AbstractActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class AbstractActivity : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {

            base.OnCreate(savedInstanceState);


        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Resource.Id.action_settings: {
                        var intent = new Intent(this, typeof(AllOrdersActivity));
                        StartActivity(intent);
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);

        }
    }
}
