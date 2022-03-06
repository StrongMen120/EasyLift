using EasyLift.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLift
{
    public partial class App : Application
    {
        private static Db _ctx;
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Tabbed());
        }
        public static Db Db
        {
            get
            {
                if ( _ctx == null )
                {
                    string dbName = "EasyLiftDB.sqlite";
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string fullPath = Path.Combine(folderPath,dbName);
                    _ctx = new Db(fullPath);
                }
                return _ctx;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
