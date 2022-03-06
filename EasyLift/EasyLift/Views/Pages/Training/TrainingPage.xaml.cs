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
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class TrainingPage : ContentPage
    {
        private Plan _plan;
        public TrainingPage(Plan plan)
        {
            _plan = plan;
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Workout> lstWor = App.Db.GetWorkout();
            List<WorkoutDetail> lstWorD = App.Db.GetWorkoutDetail();
            List<ItemList> res = CreatedTrainingList(lstWorD, lstWor);
            var navigationPage = App.Current.MainPage as NavigationPage;
            navigationPage.Title = _plan.Name;
            Grid2Row.Height = App.ScreenHeight - 35;
            trainingListView.ItemsSource = res;
            
        }
        private List<ItemList> CreatedTrainingList(List<WorkoutDetail> lstWrkD, List<Workout> lstWrk)
        {
            List<ItemList> res = new List<ItemList>();
            if ( !string.IsNullOrEmpty( _plan.IdWorkoutDetails ) )
            {
                string[] IDWtab = _plan.IdWorkoutDetails.Split(';');
                foreach ( var workdetail in lstWrkD )
                {
                    foreach( var idw in IDWtab )
                    {
                        if( idw == workdetail.ID.ToString() )
                        {
                            var wrk = lstWrk.FirstOrDefault(p => p.ID == workdetail.IDW);
                            res.Add(new ItemList { Name = wrk.Name, Rate = workdetail.Rate, RPM = workdetail.RPM, Img = $"BE{ (int) wrk.BodyElm }.png", Weight = SetSeries(workdetail.Weight, workdetail.Reps), IDWorkoutDetail = workdetail.ID });
                        }
                    }
                }
            }
            return res;
        }
        private async void DelBtnClicked(object sender, EventArgs e)
        {
            var res = await Navigation.ShowPopupAsync(new DelPopup(new Alert { Title = "Usuwanie !", Content = $"Czy na pewno chcesz usunąć plan: {_plan.Name} !" }));
            if( res != null )
            {
                if( Convert.ToBoolean(res) )
                {
                    App.Db.DeletePlan(_plan);
                    await Navigation.PopAsync();
                }
            }
        }
        private void PlayBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrainingPlay(_plan));
        }
        private void AddBtnClicked( object sender, EventArgs e )
        {
            Navigation.PushAsync(new AddWorkoutPage(_plan));
        }
        private void TrainingListView_ItemTapped( object sender, ItemTappedEventArgs e )
        {
            var x = trainingListView.SelectedItem as ItemList;
            List<WorkoutDetail> lstWorD = App.Db.GetWorkoutDetail();
            var item = lstWorD.FirstOrDefault(p => p.ID == x.IDWorkoutDetail);
            if( item != null )
            {
                Navigation.PushAsync(new AddWorkoutPage(_plan, item));
            }
        }
        private string SetSeries(string sWeight,string sReps)
        {
            string[] W = sWeight.Split(';');
            string[] R = sReps.Split(';');
            string res = string.Empty;
            for( int i = 0; i < W.Length; i++ )
            {
                if( !string.IsNullOrEmpty(W[i]) )
                {
                    res += W[i] + "x" + R[i] + " ";
                }
            } 
            return res;
        }
    }
    public class ItemList
    {
        public int IDWorkoutDetail { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public int? RPM { get; set; }
        public int? Rate { get; set; }
        public string Img { get; set; }
    }
}