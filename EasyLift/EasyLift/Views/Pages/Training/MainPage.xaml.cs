using EasyLift.Models;
using EasyLift.Views;
using SQLite;
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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Grid2Row.Height = App.ScreenHeight - 135;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            planListView.ItemsSource = App.Db.GetPlans();
        }

        private void PlanListView_ItemTapped( object sender, ItemTappedEventArgs e )
        {
            Plan x = planListView.SelectedItem as Plan;
            Navigation.PushAsync(new TrainingPage(x));
        }

        private async void AddBtnClicked( object sender, EventArgs e )
        {
            var result = await Navigation.ShowPopupAsync( new AddPopup() {
                IsLightDismissEnabled = false
            });
            if ( result != null )
            {
                if ( Convert.ToString(result) == "Title" ) {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wprowadnono tytułu treningu !", Button = "Anuluj" }));
                }
                else
                if ( Convert.ToString(result) == "Description" ) {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wprowadnono opisu treningu !", Button = "Anuluj" }));
                }
                else
                if ( Convert.ToString(result) == "Image" ) {
                    await Navigation.ShowPopupAsync(new AlertPopup(new Alert { Title = "Uwaga !", Content = "Nie wybrano obrazka do treningu !", Button = "Anuluj" }));
                }
                else
                {
                    App.Db.SevePlan(result as Plan);
                    planListView.ItemsSource = null;
                    planListView.ItemsSource = App.Db.GetPlans();
                }
            }
        }
    }
}
