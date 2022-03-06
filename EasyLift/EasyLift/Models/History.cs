using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class History
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } //id
        public DateTime Date { get; set; } //Data wykonania ćwiczenia
        public int IDW { get; set; } //Id ćwiczenia
        public string Weights { get; set; }
        public string Reps { get; set; }
    }
}
