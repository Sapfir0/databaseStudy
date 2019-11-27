using System;
using SQLite;

namespace testForAndroid {
    class Cities {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; } = "Russia";
    }

    class Cruises {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull]
        public DateTime departureTime { get; set; }
        [NotNull]
        public DateTime arrivingTime { get; set; }
        public int trainId { get; set; }
        public int crewId { get; set; }
        public int trainstationSourceId { get; set; }
        public int trainstationDestinationId { get; set; }

    }
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
