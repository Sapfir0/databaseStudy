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
    class Cruises : AbstractTable {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivingTime { get; set; }
        public int trainId { get; set; }
        public int crewId { get; set; }
        public int trainstationSourceId { get; set; }
        public int trainstationDestinationId { get; set; }

        public Cruises() {

        }


        public Cruises(DateTime _arrivingTime, DateTime _departureTime) {
            CreateTable();
            arrivingTime = _arrivingTime;
            departureTime = _departureTime;
        }

        public void CreateTable() {
            db.CreateTable<Cruises>();
        }

        public int InsertCruise() {
            db.Insert(this);
            return id;

        }

        public Cruises GetCruise(int id) {
            //System.Linq.Expressions.Expression
            return db.Table<Cruises>().Where(item => item.id == id).FirstOrDefault();
        }
    }
}