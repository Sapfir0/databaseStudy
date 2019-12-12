using System;
using SQLite;

namespace testForAndroid {
    class Cities {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; } = "Russia";
    }

    class User { //аххах это будет работать только в моем контексте, т.к. у меня один юзер
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CruiseId { get; set; }
    }

    class Cruises {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public DateTime DepartureTime { get; set; }
        [NotNull]
        public DateTime ArrivingTime { get; set; }
        public int TrainId { get; set; }
        public int CrewId { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }

    }
    public class Trains
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public int Number { get; set; }
        public int CompanyId { get; set; }
    }

    public class Companys
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public int Number { get; set; }
        public string Name { get; set; } = "РЖД";
    }

    public class Employees
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public int CrewId { get; set; }
    }

    public class Crews {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public int CountOfEmployees { get; set; }
    }

 
    public class TrainstationsSource
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int CityId { get; set; }
    }

    public class TrainstationsDestination
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int CityId { get; set; }
    }
}
