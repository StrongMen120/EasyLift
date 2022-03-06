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
    public partial class AddRecords : Popup
    {
        public AddRecords()
        {
            InitializeComponent();
            List<Items> lst = new List<Items>();
            lst.Add(new Items { ID = PowerLift.Squat, Name = "Przysiad" });
            lst.Add(new Items { ID = PowerLift.BenchPress, Name = "Wyciskanie Leżąc" });
            lst.Add(new Items { ID = PowerLift.Deadlift, Name = "Martwy Ciąg" });
            workoutPicker.ItemsSource = lst;
        }
        
        private void AddRecordsClicked( object sender, EventArgs e )
        {
            double Weights;
            Records res = new Records{ Date = DateTime.Now };
            if( workoutPicker.SelectedIndex == -1 ) {
                Dismiss( "Wrk" );
            }
            else 
            if ( string.IsNullOrEmpty(entryWeights.Text ) ) {
                    Dismiss("Weights");
            }
            else
            {
                if( double.TryParse(entryWeights.Text, out Weights) ) {
                    res.Weight = Weights;
                }
                res.Description = editorDescription.Text;
                res.Wrk = (PowerLift) workoutPicker.SelectedIndex;
                Dismiss(res);
            }
        }   
        private void CancelClicked( object sender, EventArgs e )
        {
            Dismiss( null );
        }
        public class Items
        {
            public PowerLift ID { get; set; }
            public string Name { get; set; }
        }
    }
}