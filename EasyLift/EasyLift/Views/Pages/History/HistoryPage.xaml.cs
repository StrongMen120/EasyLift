using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyLift
{
    public partial class HistoryPage : ContentPage
    {
        private DateTime _date;
        public HistoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _date = DateTime.Now;
            dataPicker.Date = _date;
            dataPicker.MaximumDate = _date;
            dataPicker.Format = "yyyy - MM - dd";
            ReadDate(_date);
        }
        private void ReadDate(DateTime dt)
        {
            List<HistrItem> res = new List<HistrItem>();
            List<History> lstHist = App.Db.GetHistory().Where(p => p.Date.Day == dt.Day && p.Date.Year == dt.Year && p.Date.Month == dt.Month).ToList();
            List<Workout> lstWrk = App.Db.GetWorkout();
            foreach( var item in lstHist )
            {
                Workout x = lstWrk.FirstOrDefault(p => p.ID == item.IDW);
                HistrItem hsI = new HistrItem{ Weights = ReadWeights(item.Reps, item.Weights), Img = $"BE{(int)x.BodyElm}.png", Name = x.Name};
                res.Add(hsI);
            }
            if( res.Count == 0 )
            {
                labelError.IsVisible = true;
                historyListView.IsVisible = false;
            }
            else
            {
                labelError.IsVisible = false;
                historyListView.IsVisible = true;
                historyListView.ItemsSource = null;
                historyListView.ItemsSource = res;
            }
        }
        private void DataPickerDateSelected(object sender, DateChangedEventArgs e)
        {
            if( sender is DatePicker dateP)
            {
                _date = dateP.Date;
                ReadDate(dateP.Date);
            }
        }
        private void DayBefore(object sender, EventArgs e)
        {
            if( dataPicker.MaximumDate > _date )
            {
                _date = _date.AddDays(1);
                dataPicker.Date = _date;
                ReadDate(_date);
            }
        }
        private void DayAfter(object sender, EventArgs e)
        {
            _date = _date.AddDays(-1);
            dataPicker.Date = _date;
            ReadDate(_date);
        }
        private string ReadWeights(string reps,string weights)
        {
            StringBuilder sRes = new StringBuilder();
            string[] sReps = reps.Split(';');
            string[] sWeights = weights.Split(';');
            if( sReps.Length == sWeights.Length )
            {
                for( int i = 0; i < sReps.Length - 1 ; i++ )
                {
                    if( sReps[i] != "" )
                        sRes.Append(" " + sWeights[i] + " kg - " + sReps[i] + " ||");
                }
            }
            sRes.Remove(sRes.Length - 2, 2);
            return sRes.ToString();
        }
        public class HistrItem
        {
            public string Name { get; set; }
            public string Img { get; set; }
            public string Weights { get; set; }
        }
    }
}
