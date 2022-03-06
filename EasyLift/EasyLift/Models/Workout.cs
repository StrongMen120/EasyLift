using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //ID Ćwiczenia
        [MaxLength(30)]
        public string Name { get; set; } //Nazwa
        [MaxLength(50)]
        public string Description { get; set; } //Opis
        public BodyElement BodyElm { get; set; } //Część Ciała
    }
    public enum BodyElement : int
    {
        Shoulders = 1,
        Chest = 2,
        Back = 3,
        Biceps = 4,
        Triceps = 5,
        Forearm = 6,
        Abs = 7,
        Glutes = 8,
        Legs = 9,
        Calves = 10,
    }
}
