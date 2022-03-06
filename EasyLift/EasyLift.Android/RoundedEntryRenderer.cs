using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using EasyLift.Custom;
using EasyLift.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer( typeof( RoundedEntry ), typeof( RoundedEntryRenderer ) )]
namespace EasyLift.Droid
{
    [Obsolete]
    public class RoundedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged( ElementChangedEventArgs<Entry> e )
        {
            base.OnElementChanged( e );

            if ( Control != null ) {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius( 150f );
                gradientDrawable.SetColor( Android.Graphics.Color.ParseColor( "#b43c96" ) );
                Control.SetBackground( gradientDrawable );

                Control.SetPadding( 50, Control.PaddingTop, Control.PaddingRight,
                    Control.PaddingBottom );
            }
        }
    }
}