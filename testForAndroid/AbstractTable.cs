using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = System.Environment;

namespace testForAndroid {
    class AbstractTable {
        protected static SQLiteConnection db;

        public AbstractTable() {
            db = SetConnection();
        }


        public string GetDatabasePath(string databaseName = "database.db") {

            string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(databasePath, databaseName);
            return completePath;
        }

        public SQLiteConnection SetConnection() {
            string completePath = GetDatabasePath();
            var db = new SQLiteConnection(completePath);
            return db;
        }






    }
}