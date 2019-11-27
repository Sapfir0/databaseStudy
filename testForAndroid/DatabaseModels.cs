using System;
using SQLite;

namespace testForAndroid {

    public class Trains
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int number { get; set; }
        public int companyId { get; set; }
    }

    public class Companys
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int number { get; set; }
    }

    public class Employees
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int companyId { get; set; }
        public int crewId { get; set; }
    }

 
    public class TrainstationsSource
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int code { get; set; }
        public int cityId { get; set; }
    }

    public class TrainstationsDestination
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int code { get; set; }
        public int cityId { get; set; }
    }
}
