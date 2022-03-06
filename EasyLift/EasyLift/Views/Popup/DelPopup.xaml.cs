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
    public partial class DelPopup : Popup
    {
        public DelPopup(Alert alert)
        {
            InitializeComponent();
            Title.Text = alert.Title;
            Content.Text = alert.Content;
            ButtonNo.Text = "Nie";
            ButtonYes.Text = "Tak";
        }

        private void DelYesBtn( object sender, EventArgs e )
        {
            Dismiss( true );
        }
        private void DelNoBtn(object sender, EventArgs e)
        {
            Dismiss( false );
        }
    }
}