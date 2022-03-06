using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingPlay : ContentPage
    {
        private Plan _plan;
        private List<WorkExpSave> _lstWork;
        private bool _isStopTime;
        private double _seconds;
        public TrainingPlay(Plan plan)
        {
            _plan = plan;
            _isStopTime = true;
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Workout> lstWor = App.Db.GetWorkout();
            List<WorkoutDetail> lstWorD = App.Db.GetWorkoutDetail();
            List<WorkoutList> res = CreatedWorkoutList(lstWorD, lstWor);
            WorkListView.ItemsSource = res;
        }
        private List<WorkoutList> CreatedWorkoutList(List<WorkoutDetail> lstWrkD, List<Workout> lstWrk)
        {
            List<WorkoutList> res = new List<WorkoutList>();
            List<WorkExpSave> save = new List<WorkExpSave>();
            int iWork = 0;
            if ( !string.IsNullOrEmpty(_plan.IdWorkoutDetails) )
            {
                foreach ( var idWorkD in _plan.IdWorkoutDetails.Split(';') )
                {
                    if ( !string.IsNullOrEmpty(idWorkD) )
                    {
                        var x = lstWrkD.FirstOrDefault(p => p.ID.ToString() == idWorkD);
                        if( x != null )
                        {
                            var z = lstWrk.FirstOrDefault(p=> p.ID == x.IDW);
                            if( z != null )
                            {
                                List<Series> lesSeries = new List<Series>();
                                List<WorkSerSave> workSave = new List<WorkSerSave>();
                                int iSeries = 0;
                                string idSeries = iWork + ";" + iSeries;
                                for( int j = 0; j < x.Series; j++ )
                                {
                                    lesSeries.Add(new Series { Reps = Convert.ToInt32(x.Reps.Split(';')[iSeries]), Weight = Convert.ToInt32(x.Weight.Split(';')[iSeries]), IDS = iWork + ";" + iSeries });
                                    workSave.Add(new WorkSerSave { Time = (x.TimeBrake != 0) ? x.TimeBrake : 60 });
                                    iSeries++;
                                }
                                res.Add(new WorkoutList { ID = iWork, Img = $"BE{ (int) z.BodyElm }.png", Name = z.Name, RPM = x.RPM, Time = x.TimeBrake, Rate = x.Rate, LstSeries = lesSeries });
                                save.Add(new WorkExpSave { IDW = x.IDW, LstSeries = workSave });
                            }
                        }
                        
                    }
                    iWork++;
                }
            }
            _lstWork = save;
            return res;
        }
        private void SetTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if( _isStopTime )
                {
                    _isStopTime = true;
                    return false;
                }
                else
                {
                    if( _seconds <= 0.0 ) 
                    {
                        _seconds = 0.0;
                        Timer.Text = $"{_seconds.ToString("0.0", CultureInfo.InvariantCulture)} sek";
                        Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Koniec Przerwy !", Content = "Czas na odpoczynek się skończył pora trenować !", Button = "OK" }));
                        _isStopTime = true;
                        return false;
                    }
                    _seconds -= 0.1;
                    Timer.Text = $"{_seconds.ToString("0.0", CultureInfo.InvariantCulture)} sek";
                    return true;
                }
            });
            
        }
        private void ResetTime(object sender, EventArgs e)
        {
            btnTimer.Text = "Reset";
            SetTimer(60);
        }
        private void SetTimer(double sec)
        {
            _seconds = sec;
            if( _isStopTime ) {
                _isStopTime = false;
                SetTimer();
            }  
        }
        private void TapLabel_Tapped(object sender, EventArgs e)
        {
            if( sender is Label label )
            {
                foreach ( var item in _lstWork[Convert.ToInt32(label.ClassId)].LstSeries )
                {
                    if( item.Reps == 0 && item.Weight == 0 )
                    {
                        if( label.Parent.Parent.Parent is Frame frame )
                        {
                            SetTimer(60);
                            frame.BackgroundColor = Color.Red;
                        } 
                    }
                    else
                    {
                        if( label.Parent.Parent.Parent is Frame frame )
                        {
                            SetTimer(60);
                            frame.BackgroundColor = Color.LightGreen;
                        }  
                    }
                }
            }
        }
        private void CheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            if( sender is CheckBox checkBox )
            {
                if( checkBox.Parent.Parent.Parent is StackLayout sl )
                {
                    if( checkBox.IsChecked )
                    {
                        if( checkBox.Parent is Grid grid )
                        {
                            Button btn1 = (Button) grid.Children[0];
                            Button btn2 = (Button) grid.Children[2];
                            Button btn3 = (Button) grid.Children[3];
                            Button btn4 = (Button) grid.Children[5];
                            Label lb1 = (Label) grid.Children[1];
                            Label lb2 = (Label) grid.Children[4];
                            btn1.IsEnabled = false;
                            btn2.IsEnabled = false;
                            btn3.IsEnabled = false;
                            btn4.IsEnabled = false;
                            var x = checkBox.ClassId.Split(';');
                            _lstWork[Convert.ToInt32(x[0])].LstSeries[Convert.ToInt32(x[1])].Weight = Convert.ToDouble(lb1.Text.Replace(" kg", ""));
                            _lstWork[Convert.ToInt32(x[0])].LstSeries[Convert.ToInt32(x[1])].Reps = Convert.ToInt32(lb2.Text);
                            SetTimer(_lstWork[Convert.ToInt32(x[0])].LstSeries[Convert.ToInt32(x[1])].Time);
                            sl.BackgroundColor = Color.Green;
                        }
                    }
                    else
                    {
                        if( checkBox.Parent is Grid grid )
                        {
                            Button btn1 =(Button) grid.Children[0];
                            Button btn2 =(Button) grid.Children[2];
                            Button btn3 =(Button) grid.Children[3];
                            Button btn4 =(Button) grid.Children[5];
                            btn1.IsEnabled = true;
                            btn2.IsEnabled = true;
                            btn3.IsEnabled = true;
                            btn4.IsEnabled = true;
                            var x = checkBox.ClassId.Split(';');
                            _lstWork[Convert.ToInt32(checkBox.ClassId.Split(';')[0])].LstSeries[Convert.ToInt32(checkBox.ClassId.Split(';')[1])].Weight = 0;
                            _lstWork[Convert.ToInt32(checkBox.ClassId.Split(';')[0])].LstSeries[Convert.ToInt32(checkBox.ClassId.Split(';')[1])].Reps = 0;
                            _isStopTime = true;
                            Timer.Text = $"0.0 sek";
                            sl.BackgroundColor = Color.Blue;
                        }
                    }
                }
            }
        }
        private void ButtonAddWeight(object sender, EventArgs e)
        {
            if( sender is Button button )
            {
                if( button.Parent is Grid grd )
                {
                    if( grd.Children[1] is Label lbl )
                    {
                        double var = Convert.ToDouble( lbl.Text.Replace(" kg", "") );
                        if( var > 500 )
                            lbl.Text = $"{500} kg";
                        else
                            lbl.Text = $"{var + 0.5} kg";
                    }
                }
            }
        }
        private void ButtonRemWeight(object sender, EventArgs e)
        {
            if( sender is Button button )
            {
                if( button.Parent is Grid grd )
                {
                    if( grd.Children[1] is Label lbl )
                    {
                        double var = Convert.ToDouble( lbl.Text.Replace(" kg", "") );
                        if( var <= 0.5 )
                            lbl.Text = $"{0} kg";
                        else
                            lbl.Text = $"{var - 0.5} kg";
                    }
                }
            }
        }
        private void ButtonAddReps(object sender, EventArgs e)
        {
            if( sender is Button button )
            {
                if( button.Parent is Grid grd )
                {
                    if( grd.Children[4] is Label lbl )
                    {
                        int var = Convert.ToInt32( lbl.Text );
                        if( var >= 30 )
                            lbl.Text = $"{30}";
                        else
                            lbl.Text = $"{var + 1}";
                    }
                }
            }
        }
        private void ButtonRemReps(object sender, EventArgs e)
        {
            if( sender is Button button )
            {
                if( button.Parent is Grid grd )
                {
                    if( grd.Children[4] is Label lbl )
                    {
                        int var = Convert.ToInt32( lbl.Text );
                        if( var <= 0 )
                            lbl.Text = $"{0}";
                        else
                            lbl.Text = $"{var - 1}";
                    }
                }
            }
        }
        private void SaveTraining(object sender, EventArgs e)
        {
            foreach( var item in _lstWork )
            {
                string sWeight = string.Empty;
                string sReps = string.Empty;
                WorkoutRMBest wEMBest = App.Db.GetWorkoutBest(item.IDW);
                List<WorkoutRMBest> lstBest = new List<WorkoutRMBest>();
                foreach( var item2 in item.LstSeries )
                {
                    if( item2.Weight != 0 && item2.Reps != 0 )
                    {
                        sWeight += item2.Weight + ";";
                        sReps += item2.Reps + ";";
                        lstBest.Add( ReadBest(item.IDW, item2.Reps, item2.Weight) );
                    }
                }
                if( sWeight != string.Empty && sReps != string.Empty )
                {
                    UpdateBeter(wEMBest, lstBest);
                    DateTime dt = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day );
                    App.Db.SeveHistory(new History { IDW = item.IDW, Reps = sReps, Weights = sWeight, Date = dt});
                }
            }
            _isStopTime = true;
            App.Current.MainPage = new NavigationPage(new Tabbed());
        }
        private void UpdateBeter(WorkoutRMBest w, List<WorkoutRMBest> lstW)
        {
            foreach( var item in lstW )
            {
                if( w == null )
                {
                    w = new WorkoutRMBest
                    {
                        IDW = item.IDW,
                        LombardiRes = item.LombardiRes,
                        MayhewRes = item.MayhewRes,
                        EpleyRes = item.EpleyRes,
                        BrzyckiRes = item.BrzyckiRes,
                    };
                    App.Db.SaveNewBest(w);
                }
                else
                {
                    double sumW1 = w.BrzyckiRes + w.EpleyRes + w.LombardiRes + w.MayhewRes;
                    double sumW2 = item.BrzyckiRes + item.EpleyRes + item.LombardiRes + item.MayhewRes;
                    if( sumW1 < sumW2 )
                    {
                        w.LombardiRes = item.LombardiRes;
                        w.MayhewRes = item.MayhewRes;
                        w.EpleyRes = item.EpleyRes;
                        w.BrzyckiRes = item.BrzyckiRes;
                    }
                }
            }
            App.Db.UpdateBest(w);
        }
        private WorkoutRMBest ReadBest(int idWrk, double iReps, double dWeights)
        {
            WorkoutRMBest res = new WorkoutRMBest();
            res.IDW = idWrk;
            res.LombardiRes = dWeights * Math.Pow(iReps, 0.1);
            res.MayhewRes = dWeights * 100 / (52.2 + (41.9 * Math.Exp(-1 * (iReps * 0.055))));
            res.BrzyckiRes = dWeights * (36 / (37 - iReps));
            res.EpleyRes = dWeights * (1 + (iReps / 30));
            return res;
        }
        class WorkoutList
        {
            public int ID { get; set; }
            public string Img { get; set; }
            public string Name { get; set; }
            public int? RPM { get; set; }
            public int? Rate { get; set; }
            public int? Time { get; set; }
            public List<Series> LstSeries { get; set; }
        }
        class Series
        {
            public string IDS { get; set; }
            public int Reps { get; set; }
            public double Weight { get; set; }
        }
        class WorkExpSave
        {
            public int ID { get; set; }
            public int IDW { get; set; }
            public List<WorkSerSave> LstSeries { get; set; }

        }
        class WorkSerSave
        {
            public double Weight { get; set; }
            public int Reps { get; set; }
            public int Time { get; set; }
        }
    }
}