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

        public delegate void MethodContainer();
        public event MethodContainer OnConfirm;


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

        public void DisplayConfirm(Context putThis, string title, string message) {
            AlertDialog.Builder alert = new AlertDialog.Builder(putThis);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Delete", (senderAlert, args) => {
                Toast.MakeText(putThis, "Deleted!", ToastLength.Short).Show();
                OnConfirm();
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(putThis, "Cancelled!", ToastLength.Short).Show();

            });
            Dialog dialog = alert.Create();
            alert.Show();
        }

    }
}