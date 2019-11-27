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
    class Alert {
        public static void DisplayAlert(Context putThis, string title, string message, string buttonText = "OK") {
            AlertDialog.Builder dialog = new AlertDialog.Builder(putThis);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton(buttonText, (c, ev) => {
                alert.Cancel();
            });
            alert.Show();
        }

    }
}