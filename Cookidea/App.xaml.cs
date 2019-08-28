using Cookidea.Services;
using Cookidea.Views;
using DLToolkit.Forms.Controls;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Cookidea
{
    public partial class App : Application
    {
        #region Properties
        public static MainViewModel viewModel;
        public static DatabaseService databaseService;
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            MainPage = new NavigationPage(new MainPage()){
                BarBackgroundColor = Color.DarkOrange,
                BarTextColor = Color.White
            };
        }
        #endregion

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static DatabaseService DatabaseService
        {
            get
            {
                if(databaseService == null)
                {
                    databaseService = new DatabaseService(
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MySongsDB.db3"));
                }
                return databaseService;
            }
        }
    }
}
