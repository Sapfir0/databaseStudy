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
   class Helper {

        static public List<string> ToListWithOneField(List<string> newList) {
            List<string> stringList = new List<string>();
            for (int i = 0; i < newList.Count; i++) {
                stringList.Add(newList[i]);
            }
            return stringList;
        }

}
}