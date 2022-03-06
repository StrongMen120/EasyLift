using EasyLift.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyLift
{
    public partial class StatisticsPage : ContentPage
    {
        private DateTime _date;
        private Workout _workout;
        private bool _charOption; // true-Objętość false-intensywność
        public StatisticsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            workoutPicker.ItemsSource = App.Db.GetWorkout();
            _date = DateTime.Now;
            MonthPicker.Text = _date.ToString("MMMM - yyyy").ToUpper();
        }
        private void MonthBefore(object sender, EventArgs e)
        {
            _date = _date.AddMonths(1);
            MonthPicker.Text = _date.ToString("MMMM - yyyy").ToUpper();
            CreateChartView();
        }
        private void MonthAfter(object sender, EventArgs e)
        {
            _date = _date.AddMonths(-1);
            MonthPicker.Text = _date.ToString("MMMM - yyyy").ToUpper();
            CreateChartView();
        }
        private void WorkoutPickerChanged(object sender, EventArgs e)
        {
            _workout = workoutPicker.SelectedItem as Workout;
            CreateChartView();
        }
        private void CreateChartView()
        {
            if( _workout == null )
            {
                labelError.IsVisible = true;
                chartViewLine.IsVisible = false;
            }
            else
            {
                if(_charOption )
                {
                    DrawVolumesChar();
                }
                else
                {
                    DrawIntensityChar();
                }
            }
        }
        private void DrawIntensityChar()
        {
            List<ChartEntry> lstres = new List<ChartEntry>();
            List<History> res = App.Db.GetHistoryWorkout(_workout.ID).Where(p => p.Date.Month == _date.Month).OrderBy(p => p.Date).ToList();
            WorkoutRMBest wEMBest = App.Db.GetWorkoutBest(_workout.ID);
            if( res.Count == 0 || wEMBest == null)
            {
                labelError.IsVisible = true;
                chartViewLine.IsVisible = false;
            }
            else
            {
                foreach( var wrk in res )
                {
                    string[] tReps = wrk.Reps.Split(';');
                    string[] tWeights = wrk.Weights.Split(';');
                    double volume = 0;
                    int allReps = 0;
                    for( int i = 0; i < tReps.Length; i++ )
                    {
                        if( !string.IsNullOrEmpty(tReps[i]) || !string.IsNullOrEmpty(tWeights[i]) )
                        {
                            allReps += Convert.ToInt32(tReps[i]);
                            volume += Convert.ToDouble(tReps[i]) * Convert.ToDouble(tWeights[i]);
                        }
                    }
                    
                    double RM = (wEMBest.BrzyckiRes+wEMBest.EpleyRes+wEMBest.LombardiRes+wEMBest.MayhewRes)/4;
                    double val = ((volume/allReps)/RM)*100;
                    lstres.Add(new ChartEntry(Convert.ToSingle(val)) { Label = wrk.Date.Day + "." + wrk.Date.Month, ValueLabel = String.Format("{0:0.0}", val) + " %", ValueLabelColor = SKColor.Parse("#FFFFFF"), Color = SKColor.Parse("#b43c96") });
                }
                labelError.IsVisible = false;
                chartViewLine.IsVisible = true;
                chartViewLine.Chart = null;
                chartViewLine.Chart = new LineChart
                {
                    Entries = lstres,
                    ValueLabelOrientation = Orientation.Vertical,
                    LabelTextSize = 30,
                    LineMode = LineMode.Straight,
                    BackgroundColor = SKColor.Parse("#282828"),
                    LabelColor = SKColor.Parse("#FFFFFF"),
                    LineSize = 5,
                };
            }
        }

        //Rysowanie wykresu objętości
        private void DrawVolumesChar()
        {
            List<ChartEntry> lstres = new List<ChartEntry>();
            List<History> res = App.Db.GetHistoryWorkout(_workout.ID).Where(p => p.Date.Month == _date.Month).OrderBy(p => p.Date).ToList();
            if( res.Count == 0 )
            {
                labelError.IsVisible = true;
                chartViewLine.IsVisible = false;
            }
            else
            {
                foreach( var wrk in res )
                {
                    string[] tReps = wrk.Reps.Split(';');
                    string[] tWeights = wrk.Weights.Split(';');
                    float val = 0;
                    for( int i = 0; i < tReps.Length; i++ )
                    {
                        if( !string.IsNullOrEmpty(tReps[i]) || !string.IsNullOrEmpty(tWeights[i]) )
                        {
                            val += Convert.ToSingle(tReps[i]) * Convert.ToSingle(tWeights[i]);
                        }
                    }
                    // Dodawanie punktów do wykresu
                    lstres.Add(new ChartEntry(val) { Label = wrk.Date.Day + "." + wrk.Date.Month, ValueLabel = "" + val, ValueLabelColor = SKColor.Parse("#FFFFFF"), Color = SKColor.Parse("#b43c96") });
                }
                labelError.IsVisible = false;
                chartViewLine.IsVisible = true;
                chartViewLine.Chart = new LineChart
                {
                    Entries = lstres, //Lista elementów do narysowania
                    ValueLabelOrientation = Orientation.Vertical, // rysowanie pionowe
                    LabelTextSize = 30, // Rozmiar tekstu 30
                    LineMode = LineMode.Straight, // Linia prosta
                    BackgroundColor = SKColor.Parse("#282828"), // tło ciemne
                    LabelColor = SKColor.Parse("#FFFFFF"), // kolor napisów biały
                    LineSize = 5, // Grubość lini
                };
            }
        }
        private void btnVolumeClicked(object sender, EventArgs e)
        {
            _charOption = true;
            CreateChartView();
        }

        private void btnIntensityClicked(object sender, EventArgs e)
        {
            _charOption = false;
            CreateChartView();
        }
    }
}
