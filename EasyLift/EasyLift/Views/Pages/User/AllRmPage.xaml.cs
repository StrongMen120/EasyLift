using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllRmPage : ContentPage
    {
        private Workout _workout;
        public AllRmPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            workoutPicker.ItemsSource = App.Db.GetWorkout();
        }
        private void WorkoutPickerChanged(object sender, EventArgs e)
        {
            _workout = workoutPicker.SelectedItem as Workout;
            tmplName.Text = _workout.Name;
            tmplDesc.Text = _workout.Description;
            tmplImg.Source = $"BE{(int)_workout.BodyElm}.png";
            WrkTmpl.IsVisible = true;
            CalculateRM(_workout.ID);
            
        }
        ////////////////////////////////////////////////
        /// Wyliczanie wszystkich RM dla ćwiczenia o id
        private void CalculateRM(int idWorkout)
        {
            WorkoutRMBest wRMBest = App.Db.GetWorkoutBest(idWorkout); // Pobranie rekordów z bazy danych
            if( wRMBest != null )
            {
                List<AllRmItems> lstRM = new List<AllRmItems>();
                lstRM.Add(new AllRmItems
                {
                    RM = 1,
                    Brzycki = String.Format("{0:0.00}", wRMBest.BrzyckiRes) + " kg",
                    Epley = String.Format("{0:0.00}", wRMBest.EpleyRes) + " kg",
                    Lombardi = String.Format("{0:0.00}", wRMBest.LombardiRes) + " kg",
                    Mayhew = String.Format("{0:0.00}", wRMBest.MayhewRes) + " kg",
                    Średnia = String.Format("{0:0.00}", (wRMBest.BrzyckiRes + wRMBest.EpleyRes + wRMBest.LombardiRes + wRMBest.MayhewRes) / 4) + " kg"
                });
                for( double i = 2; i < 11; i++ ) // Pętla która wylicza wszystkie RM
                {
                    double lomrm,mayrm,eplrm,brzrm;
                    lomrm = wRMBest.LombardiRes / Math.Pow(i, 0.1);
                    mayrm = wRMBest.MayhewRes * (52.2 + 41.9 * Math.Exp(-1 * (i * 0.055))) / 100;
                    eplrm = wRMBest.EpleyRes / (1 + (i / 30));
                    brzrm = wRMBest.BrzyckiRes * (37 - i) / 36;
                    lstRM.Add(new AllRmItems 
                    { 
                        RM = Convert.ToInt32(i),
                        Brzycki = String.Format("{0:0.00}", brzrm) + " kg",
                        Epley = String.Format("{0:0.00}", eplrm) + " kg",
                        Lombardi = String.Format("{0:0.00}", lomrm) + " kg",
                        Mayhew = String.Format("{0:0.00}", mayrm) + " kg",
                        Średnia = String.Format("{0:0.00}", (brzrm + eplrm + lomrm + mayrm) / 4) + " kg"
                    });
                }
                AllRMListView.ItemsSource = null;
                AllRMListView.ItemsSource = lstRM;
                NoDataLabel.IsVisible = false;
                AllRMListView.IsVisible = true;
            }
            else
            {
                AllRMListView.IsVisible = false;
                NoDataLabel.IsVisible = true;
            }
            
        }
        public class AllRmItems
        {
            public int RM { get; set; }
            public string Lombardi { get; set; }
            public string Brzycki { get; set; }
            public string Epley { get; set; }
            public string Mayhew { get; set; }
            public string Średnia { get; set; }
        }
    }
}