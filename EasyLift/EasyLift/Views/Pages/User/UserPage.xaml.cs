using EasyLift.Models;
using EasyLift.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace EasyLift
{
    public partial class UserPage : ContentPage
    {
        private User _user;
        private List<Records> _records;
        public UserPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _user = App.Db.GetUser();
            _records = App.Db.GetRecords();
            if( _user != null )
            {
                WeightsLabel.Text = "" + _user.Weight + " kg";
                HeightLabel.Text = "" + _user.Height + " cm";
                AgeLabel.Text = "" + _user.Age + " ";
            }
            if(_records != null )
            {
                if( _records.Where(p => p.Wrk == PowerLift.Squat).ToList().Count != 0 ) {
                    SquatLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.Squat).Max(x => x.Weight) + " kg";
                }
                if( _records.Where(p => p.Wrk == PowerLift.Deadlift).ToList().Count != 0 ) {
                    DedliftLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.Deadlift).Max(x => x.Weight) + " kg";
                }
                if( _records.Where(p => p.Wrk == PowerLift.BenchPress).ToList().Count != 0 ) { 
                    BenchPressLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.BenchPress).Max(x => x.Weight) + " kg";
                }
            }
        }
        private void EditBtnClicked(object sender, EventArgs e)
        {
            if( _user != null )
            {
                WeightsEntry.Text = "" + _user.Weight;
                HeightEntry.Text = "" + _user.Height;
                AgeEntry.Text = "" + _user.Age;
            }
            EditBtn.IsVisible = false;
            SaveBtn.IsVisible = true;
            RecordsBtn.IsVisible = false;
            CancelBtn.IsVisible = true;
            WeightsEntry.IsVisible = true;
            WeightsLabel.IsVisible = false;
            HeightEntry.IsVisible = true;
            HeightLabel.IsVisible = false;
            AgeEntry.IsVisible = true;
            AgeLabel.IsVisible = false;
        }

        private async void SaveBtnClicked(object sender, EventArgs e)
        {

            User usr = new User() { Date = DateTime.Now };
            int Age;
            double Weights,Heights;
            if( int.TryParse(AgeEntry.Text, out Age) ) {
                usr.Age = Age;
            }
            else {
                await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Wiek musi być liczbą całkowitą !", Button = "Ok" }));
                return;
            }
            if( double.TryParse(WeightsEntry.Text, out Weights) ) {
                usr.Weight = Weights;
            }
            else {
                await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Waga musi być liczbą !", Button = "Ok" }));
                return;
            }
            if( double.TryParse(HeightEntry.Text, out Heights ) ) {
                usr.Height = Heights;
            }
            else {
                await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Wzrost musi być liczbą !", Button = "Ok" }));
                return;
            }
            App.Db.SeveUser(usr);
            _user = usr;
            EditBtn.IsVisible = true;
            SaveBtn.IsVisible = false;
            RecordsBtn.IsVisible = true;
            CancelBtn.IsVisible = false;
            WeightsEntry.IsVisible = false;
            WeightsLabel.IsVisible = true;
            WeightsLabel.Text = "" + _user.Weight + " kilogramów";
            HeightEntry.IsVisible = false;
            HeightLabel.IsVisible = true;
            HeightLabel.Text = "" + _user.Height + " centymetrów";
            AgeEntry.IsVisible = false;
            AgeLabel.IsVisible = true;
            AgeLabel.Text = "" + _user.Age + " lat";
        }

        private async void RecordsBtnClicked(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync( new AddRecords() {
                IsLightDismissEnabled = false
            });
            if( result != null )
            {
                if( Convert.ToString(result) == "Wrk" )
                {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wybrano ćwiczenia !", Button = "Anuluj" }));
                }
                else
                if( Convert.ToString(result) == "Description" )
                {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wprowadnono opisu rekordu !", Button = "Anuluj" }));
                }
                else
                if( Convert.ToString(result) == "Weights" )
                {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wybrano obciążenia rekordu !", Button = "Anuluj" }));
                }
                else
                {
                    if(result is Records res )
                    {
                        App.Db.SeveRecords(res);
                        _records.Add(res);
                        if( _records.Where(p => p.Wrk == PowerLift.Squat).ToList().Count != 0 ) {
                            SquatLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.Squat).Max(x => x.Weight) + " kg";
                        }
                        if( _records.Where(p => p.Wrk == PowerLift.Deadlift).ToList().Count != 0 ) {
                            DedliftLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.Deadlift).Max(x => x.Weight) + " kg";
                        }
                        if( _records.Where(p => p.Wrk == PowerLift.BenchPress).ToList().Count != 0 ) {
                            BenchPressLabel.Text = "" + _records.Where(p => p.Wrk == PowerLift.BenchPress).Max(x => x.Weight) + " kg";
                        }
                    }
                }
            }
        }
        private void AllRMBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllRmPage());
        }
        private async void CalculatorBtnClicked(object sender, EventArgs e)
        {
            await Navigation.ShowPopupAsync(new Calculator()
            {
                IsLightDismissEnabled = false
            });
        }

        private void CancelBtnClicked(object sender, EventArgs e)
        {
            EditBtn.IsVisible = true;
            SaveBtn.IsVisible = false;
            RecordsBtn.IsVisible = true;
            CancelBtn.IsVisible = false;
            WeightsEntry.IsVisible = false;
            WeightsLabel.IsVisible = true;
            WeightsLabel.Text = "" + _user.Weight + " kilogramów";
            HeightEntry.IsVisible = false;
            HeightLabel.IsVisible = true;
            HeightLabel.Text = "" + _user.Height + " centymetrów";
            AgeEntry.IsVisible = false;
            AgeLabel.IsVisible = true;
            AgeLabel.Text = "" + _user.Age + " lat";
        }
        private async void HistoryBtnClicked(object sender, EventArgs e)
        {
            await Navigation.ShowPopupAsync(new HistoryPopup() { IsLightDismissEnabled = false });
        }
    }
}
