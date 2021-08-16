using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MVVM_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            MainPage = new MVVM_App.View.ListadoPagina();
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
