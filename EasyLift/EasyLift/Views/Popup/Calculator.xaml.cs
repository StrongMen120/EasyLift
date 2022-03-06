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
    public partial class Calculator : Popup
    {
        public Calculator()
        {
            InitializeComponent();
        }
        /////////////////////////
        /// Wyliczanie Maksymalnego ciężaru
        private void CalculateClicked( object sender, EventArgs e )
        {
            if(!string.IsNullOrEmpty(entryWeights.Text) && !string.IsNullOrEmpty(entryReps.Text) )
            {
                if( double.TryParse(entryWeights.Text, out double dWeight) )
                {
                    if( double.TryParse(entryReps.Text, out double dReps) )
                    {
                        double res1 = dWeight * Math.Pow(dReps, 0.1); // Wzór Lombardiego
                        Result1.Text = "" + String.Format("{0:0.00}", res1) + "kg";
                        double res2 = dWeight * 100 / (52.2 + (41.9 * Math.Exp(-1 * (dReps * 0.055)))); // Wzór Mayhew et al
                        Result2.Text = "" + String.Format("{0:0.00}", res2) + "kg";
                        double res3 = dWeight * (36 / (37 - dReps)); // Wzór Brzyckiego
                        Result3.Text = "" + String.Format("{0:0.00}", res3) + "kg";
                        double res4 = dWeight * (1 + (dReps / 30)); // Formuła Epley
                        Result4.Text = "" + String.Format("{0:0.00}", res4) + "kg";
                    }
                }
            }
        }   
        private void CancelClicked( object sender, EventArgs e )
        {
            Dismiss( null );
        }
    }
}