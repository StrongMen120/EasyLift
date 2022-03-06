using EasyLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class AddWrkPopup : Popup
    {
        public AddWrkPopup()
        {
            InitializeComponent();
            List<ListWork> lstwrk = new List<ListWork>()
            {
                new ListWork {Id = 1, BodyElm = "Barki"},
                new ListWork {Id = 2, BodyElm = "Klatka Piersiowa"},
                new ListWork {Id = 3, BodyElm = "Plecy"},
                new ListWork {Id = 4, BodyElm = "Biceps"},
                new ListWork {Id = 5, BodyElm = "Triceps"},
                new ListWork {Id = 6, BodyElm = "Przedramie"},
                new ListWork {Id = 7, BodyElm = "Brzuch"},
                new ListWork {Id = 8, BodyElm = "Pośladki"},
                new ListWork {Id = 9, BodyElm = "Nogi"},
                new ListWork {Id = 10, BodyElm = "Łydki"},
            };
            workoutEntry.ItemsSource = lstwrk;
        }

        private void AddWorkoutClicked( object sender, EventArgs e )
        {
            
            if ( string.IsNullOrEmpty( entryName.Text ) ) {
                Dismiss("Name");
            }
            else
            if ( string.IsNullOrEmpty( entryDescription.Text ) ) {
                Dismiss("Description");
            }
            else
            if ( workoutEntry.SelectedItem == null ) {
                Dismiss("BodyElm");
            }
            else
            {
                Workout _res = new Workout{Name = entryName.Text, Description = entryDescription.Text,BodyElm = (BodyElement) workoutEntry.SelectedIndex + 1 };
                Dismiss( _res );
            }
        }   
        private void CancelClicked( object sender, EventArgs e )
        {
            Dismiss( null );
        }
        public class ListWork
        {
            public int Id { get; set; }
            public string BodyElm { get; set; }
        }
    }
}