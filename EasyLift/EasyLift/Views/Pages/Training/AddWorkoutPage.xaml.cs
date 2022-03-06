using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkoutPage : ContentPage
    {
        private Plan _plan;
        private WorkoutDetail _workout;
        public AddWorkoutPage(Plan plan)
        {
            _plan = plan;
            InitializeComponent();
        }
        public AddWorkoutPage(Plan plan, WorkoutDetail wrk)
        {
            _plan = plan;
            _workout = wrk;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            workoutPicker.ItemsSource = App.Db.GetWorkout();
            if( _workout == null )
            {
                workoutListView.ItemsSource = new List<WorkDet>() {
                  new WorkDet {Id = 1, Reps = 0.ToString(), Weights = 0.ToString()},
                  new WorkDet {Id = 2, Reps = 0.ToString(), Weights = 0.ToString()},
                  new WorkDet {Id = 3, Reps = 0.ToString(), Weights = 0.ToString()},
                  new WorkDet {Id = 4, Reps = 0.ToString(), Weights = 0.ToString()},
                };                                          
                Btn11.IsVisible = true;
                Btn21.IsVisible = true;
            }
            else
            {
                List<WorkDet> lstRes = new List<WorkDet>();
                string[] sW = _workout.Weight.Split(';');
                string[] sR = _workout.Reps.Split(';');
                for( int i = 0; i < _workout.Series; i++ )
                {
                    lstRes.Add(new WorkDet { Id = i + 1, Reps = sR[i], Weights = sW[i]});
                }
                workoutListView.ItemsSource = lstRes;
                entryTime.Text = "" + _workout.TimeBrake;
                entryRPM.Text = "" + _workout.RPM;
                entryRate.Text = "" + _workout.Rate;
                workoutPicker.SelectedIndex = _workout.IDW - 1;
                Btn12.IsVisible = true;
                Btn22.IsVisible = true;
            }
        }
        
        private void AddWorkautDetail_Clicked(object sender, EventArgs e)
        {
            if( _workout == null )
            {
                if( workoutPicker.SelectedItem != null )
                {
                    WorkoutDetail res = new  WorkoutDetail();
                    List<WorkDet> lstSeries = workoutListView.ItemsSource as List<WorkDet>;
                    Workout selWrk = workoutPicker.SelectedItem as Workout;
                    res.IDW = selWrk.ID;
                    res.Series = lstSeries.Count;
                    foreach( var series in lstSeries )
                    {
                        res.Reps += series.Reps + ";";
                        res.Weight += series.Weights + ";";
                    }
                    if( !string.IsNullOrEmpty(entryRate.Text) )
                    {
                        res.Rate = Convert.ToInt32(entryRate.Text);
                    }
                    if( !string.IsNullOrEmpty(entryRPM.Text) )
                    {
                        res.RPM = Convert.ToInt32(entryRPM.Text);
                    }
                    if( !string.IsNullOrEmpty(entryTime.Text) )
                    {
                        res.TimeBrake = Convert.ToInt32(entryTime.Text);
                    }
                    _plan.IdWorkoutDetails += App.Db.SeveWorkoutDetail(res) + ";";
                    App.Db.UpdatePlan(_plan);
                    Navigation.PopAsync();
                }
            }
        }
        private void DelWorkautDetail_Clicked(object sender, EventArgs e)
        {
            if( _workout != null )
            {
                _plan.IdWorkoutDetails.Replace($"{_workout.ID};", string.Empty);
                App.Db.UpdatePlan(_plan);
                App.Db.DeleteWorkoutDetail(_workout);
                Navigation.PopAsync();
            }
        }
        private void UpdateWorkautDetail_Clicked(object sender, EventArgs e)
        {
            if( _workout != null )
            {
                List<WorkDet> lstSeries = workoutListView.ItemsSource as List<WorkDet>;
                Workout selWrk = workoutPicker.SelectedItem as Workout;
                _workout.IDW = selWrk.ID;
                _workout.Series = lstSeries.Count;
                foreach( var series in lstSeries )
                {
                    _workout.Reps += series.Reps + ";";
                    _workout.Weight += series.Weights + ";";
                }
                if( !string.IsNullOrEmpty(entryRate.Text) ) {
                    _workout.Rate = Convert.ToInt32(entryRate.Text);
                }
                else {
                    _workout.Rate = 0;
                }
                if( !string.IsNullOrEmpty(entryRPM.Text) ){
                    _workout.RPM = Convert.ToInt32(entryRPM.Text);
                }
                else {
                    _workout.RPM = 0;
                }
                if( !string.IsNullOrEmpty(entryTime.Text) ) {
                    _workout.TimeBrake = Convert.ToInt32(entryTime.Text);
                }
                else {
                    _workout.TimeBrake = 0;
                }
                _plan.IdWorkoutDetails += App.Db.UpdateWorkoutDetail(_workout) + ";";
                App.Db.UpdatePlan(_plan);
                Navigation.PopAsync();
            }
        }
        private async void AddWorkaut_Clicked(object sender, EventArgs e)
        {
            if( _workout == null )
            {
                var result = await Navigation.ShowPopupAsync( new AddWrkPopup()
                {
                    IsLightDismissEnabled = false
                });
                if( result != null )
                {
                    if( Convert.ToString(result) == "Name" )
                    {
                        await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wprowadnono nazwy ćwiczenia !", Button = "Anuluj" }));
                    }
                    else
                    if( Convert.ToString(result) == "Description" )
                    {
                        await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wprowadnono opisu ćwiczenia !", Button = "Anuluj" }));
                    }
                    else
                    if( Convert.ToString(result) == "BodyElm" )
                    {
                        await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie przypisano partii ciała do ćwiczenia !", Button = "Anuluj" }));
                    }
                    else
                    {
                        App.Db.SeveWorkout(result as Workout);
                        workoutPicker.ItemsSource = null;
                        workoutPicker.ItemsSource = App.Db.GetWorkout();
                    }
                }
            }
        }
        private void AddSeries(object sender, EventArgs e)
        {
            List<WorkDet> lstSeries = workoutListView.ItemsSource as List<WorkDet>;
            if( lstSeries.Count < 8 )
            {
                workoutListView.ItemsSource = null;
                lstSeries.Add(new WorkDet { Id = lstSeries.Count + 1, Weights = "0", Reps = "0" });
                workoutListView.ItemsSource = lstSeries;
            }
        }
        private void RemoveSeries(object sender, EventArgs e)
        {
            List<WorkDet> lstSeries = workoutListView.ItemsSource as List<WorkDet>;
            if( lstSeries.Count != 1 )
            {
                workoutListView.ItemsSource = null;
                lstSeries.RemoveAt(lstSeries.Count - 1);
                workoutListView.ItemsSource = lstSeries;
            }
        }
        private class WorkDet
        {
            public int Id { get; set; }
            public string Weights { get; set; }
            public string Reps { get; set; }
        }
    }
}