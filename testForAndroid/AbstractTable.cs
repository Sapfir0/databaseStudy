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


        public T NewRow {
            get {
                return _tableConcrete;
            }
            set {
                _tableConcrete = new T();
            }
        }

        public AbstractTable() {
            _tableConcrete = new T();
            db = SetConnection();
            db.CreateTable<T>();
        }


        public static string GetDatabasePath(string databaseName = "database.db") {

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

        public T GetElement(int id) { // TODO
            return db.Get<T>(id);
        }


        public static void DeleteAll() {
            string completePath =  GetDatabasePath();
            File.Delete(completePath);
        }

        public void Delete(int id) {
            db.Delete<T>(id);
        }

        public bool isEmpty() {
            return db.Table<T>().Count() == 0;
        }

        public List<T> GetAllElements() {
            return db.Table<T>().ToList();
        }

        public int CountOfElements() {
            return db.Table<T>().Count();
        }

        public bool isCurrentCityExists(string existedParam) { // TODO переписать метод на более абстрактный
            int foundVlues = db.Table<Cities>().Where(item => item.Name == existedParam).Count();
            return foundVlues > 0;
        }

        public int InsertElement() {
            int insertedId = db.InsertOrReplace(_tableConcrete);
            return insertedId;
        }

    }
}