using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class Records
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
        public PowerLift Wrk { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
    }
    public enum PowerLift : int
    {
        Squat = 0,
        BenchPress = 1,
        Deadlift = 2
    }
}
