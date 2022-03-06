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
    public partial class AlertPopup : Popup
    {
        public AlertPopup(Alert alert)
        {
            InitializeComponent();
            Title.Text = alert.Title;
            Content.Text = alert.Content;
            Button.Text = alert.Button;
        }

        private void AlertOkBtn( object sender, EventArgs e )
        {
            Dismiss( null );
        }
    }
}