using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class Plan
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //ID Planu treningowego
        [MaxLength(20)]
        public string Name { get; set; } //ID Nazwa planu
        public string Img { get; set; } ////Nazwa obrazka
        [MaxLength(45)]
        public string Description { get; set; } //Opis planu
        public string IdWorkoutDetails { get; set; } //lista Id ćwiczeń oddzielona ;
    }
}
