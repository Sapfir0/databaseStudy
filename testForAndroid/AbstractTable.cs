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

    class AbstractTable<T> where T : new() {
        private T _tableConcrete;
        protected static SQLiteConnection db;


        public T tableConcrete {
            get {
                return _tableConcrete;
            }
            set {
                _tableConcrete = new T();
            }
        }

        public AbstractTable() {
            db = SetConnection();
            CreateTable();
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

        public void CreateTable() {
            db.CreateTable<T>();
        }

        //public T GetElement(int id) {
        //    return db.Table<T>().Where(item => item.id == id).FirstOrDefault();
        //}

        public int InsertElement() {
            int insertedId = db.Insert(_tableConcrete);
            return insertedId;

        }
    }
}