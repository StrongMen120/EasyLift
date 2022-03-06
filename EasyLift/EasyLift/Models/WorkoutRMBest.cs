using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class WorkoutRMBest
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDW { get; set; } //id ćwiczenia
        public double LombardiRes { get; set; }
        public double BrzyckiRes { get; set; }
        public double EpleyRes { get; set; }
        public double MayhewRes { get; set; }
    }
}
