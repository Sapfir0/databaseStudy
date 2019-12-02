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

        public static bool DisplayConfirm(Context putThis, string title, string message) {
            AlertDialog.Builder alert = new AlertDialog.Builder(putThis);
            alert.SetTitle(title);
            alert.SetMessage(message);
            bool confirm = false;
            alert.SetPositiveButton("Delete", (senderAlert, args) => {
                Toast.MakeText(putThis, "Deleted!", ToastLength.Short).Show();
                
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(putThis, "Cancelled!", ToastLength.Short).Show();
                return;
                confirm = false;
            });
            Dialog dialog = alert.Create();
            dialog.Show();
            return confirm;
        }

    }
}