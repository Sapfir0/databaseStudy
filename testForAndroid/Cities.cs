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
using SQLite;

namespace testForAndroid {
    class Cities : AbstractTable {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }


        public Cities(string _name) {
            CreateTable();
            country = "Russia";
            name = _name;
        }

        public void CreateTable() {
            db.CreateTable<Cities>();
        }

        public int InsertCity() {
            db.Insert(this);
            return id;

        }

        //public Cities GetCity(int id) {
        //    return db.Get<Cities>(id);
        //}
    }
}