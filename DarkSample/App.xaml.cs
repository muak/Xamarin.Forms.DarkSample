using System;
using DarkSample.Views;
using Xamarin.Forms;

namespace DarkSample
{
    public partial class App : Application
    {
        public static event EventHandler OnForeground;

        public App()
        {
            InitializeComponent();

            MainPage = new MyNavigationPage(new MainPage());
        }

        protected override void OnStart()
        {            
            ThemeResource.ApplyTheme();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            OnForeground?.Invoke(this, EventArgs.Empty);
        }

    }
}
