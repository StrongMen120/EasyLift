using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLift.Models
{
    public class WorkoutDetail
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; } //ID Szczegół Ćwiczenia
        public int IDW { get; set; } //ID Ćwiczenia
        public int Series { get; set; } //Liczba serii
        public string Reps { get; set; } //Liczba powtórzeń
        public string Weight { get; set; } //Obciążenie
        public int RPM { get; set; } //Skala Rpm
        public int Rate { get; set; } //Tempo  
        public int TimeBrake { get; set; } //Przerwa między seriami
    }
}
