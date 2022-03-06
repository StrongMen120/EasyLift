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
    public partial class AddPopup : Popup
    {
        public string LastImage { get; set; }
        public AddPopup()
        {
            InitializeComponent();
            LastImage = null;
        }

        private void AddExerciseClicked( object sender, EventArgs e )
        {
            if ( string.IsNullOrEmpty( entryTitle.Text ) ) {
                Dismiss( "Title" );
            }
            else 
            if ( string.IsNullOrEmpty( editorDescription.Text ) ) {
                Dismiss( "Description" );
            }
            else
            if ( string.IsNullOrEmpty( LastImage ) ) {
                Dismiss( "Image" );
            }
            else {
                Plan _res = new Plan{Description = editorDescription.Text, Name = entryTitle.Text, Img = LastImage};
                Dismiss( _res );
            }
        }   
        private void CancelClicked( object sender, EventArgs e )
        {
            Dismiss( null );
        }
        private void ImgTaped( object sender, EventArgs e )
        {
            Image x = (Image) sender;
            FileImageSource z = (FileImageSource) x.Source;
            Frame test = (Frame) FindByName(z.File.Split('.')[0]);
            if( test != null ) {
                test.BackgroundColor = Color.Red;
                if( LastImage != null ) {
                    Frame lastFrame = (Frame) FindByName( LastImage.Split( '.' )[0] );
                    if( lastFrame != null ) {
                        lastFrame.BackgroundColor = Color.Black;
                    }
                }
                LastImage = z.File;
            }
        }
    }
}