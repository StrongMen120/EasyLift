using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class HistoryPopup : Popup
    {
        public HistoryPopup()
        {
            InitializeComponent();
            var x = App.Db.GetAllUser().OrderBy(p => p.Date);
            List<Items> lst = new List<Items>();
            foreach( var item in x )
            {
                lst.Add(new Items { Name = $"{item.Date:d} Waga-{item.Weight} kg Wzrost-{item.Height} cm Wiek-{item.Age} lat" });
            }
            histListView.ItemsSource = lst;
        }
        private void BodyClicked( object sender, EventArgs e )
        {
            recordsBtn.IsVisible = true;
            bodyBtn.IsVisible = false;
            var x = App.Db.GetAllUser().OrderBy(p => p.Date);
            List<Items> lst = new List<Items>();
            foreach( var item in x )
            {
                lst.Add(new Items { Name = $"{item.Date:d} Waga-{item.Weight} kg Wzrost-{item.Height} cm Wiek-{item.Age} lat" });
            }
            histListView.ItemsSource = null;
            histListView.ItemsSource = lst;
        }
        private void RekordClicked(object sender, EventArgs e)
        {
            recordsBtn.IsVisible = false;
            bodyBtn.IsVisible = true;
            var x = App.Db.GetRecords().OrderBy(p => p.Date);
            List<Items> lst = new List<Items>();
            foreach( var item in x )
            {
                string wrk;
                if( item.Wrk == PowerLift.Squat ) {
                    wrk = "Przysiad";
                }
                else
                if( item.Wrk == PowerLift.BenchPress ) {
                    wrk = "Wyciskanie Leżąc";
                }
                else {
                    wrk = "Martwy Ciąg";
                }
                lst.Add(new Items { Name = $"{item.Date:d} | {wrk} Waga-{item.Weight} kg" });
            }
            histListView.ItemsSource = null;
            histListView.ItemsSource = lst;
        }
        private void CloseClicked( object sender, EventArgs e )
        {
            Dismiss( null );
        }
        public class Items
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}