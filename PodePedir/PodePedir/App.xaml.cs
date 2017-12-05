using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PodePedir
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Define por qual página nossa aplicação irá iniciar.
            MainPage = new PodePedir.View.SplashView();
        }

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
    }
}
