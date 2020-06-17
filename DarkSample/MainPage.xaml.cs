using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DarkSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static BindableProperty CurrentModeProperty = BindableProperty.Create(
            nameof(CurrentMode),
            typeof(string),
            typeof(MainPage),
            default(string),
            defaultBindingMode: BindingMode.OneWay
        );

        public string CurrentMode
        {
            get { return (string)GetValue(CurrentModeProperty); }
            set { SetValue(CurrentModeProperty, value); }
        }

        public MainPage()
        {
            InitializeComponent();

            CurrentMode = GetCurrentMode((Theme)Xamarin.Essentials.Preferences.Get("mode", (int)Theme.Light));
        }

        string GetCurrentMode(Theme theme)
        {
            return theme switch
            {
                Theme.Light => "Light Theme",
                Theme.Dark => "Dark Theme",
                _ => "Auto"
            };
        }

        void Light_Clicked(System.Object sender, System.EventArgs e)
        {
            CurrentMode = GetCurrentMode(Theme.Light);
            ThemeResource.ChangeTheme(Theme.Light);
        }

        void Dark_Clicked(System.Object sender, System.EventArgs e)
        {
            CurrentMode = GetCurrentMode(Theme.Dark);
            ThemeResource.ChangeTheme(Theme.Dark);
        }

        void Auto_Clicked(System.Object sender, System.EventArgs e)
        {
            CurrentMode = GetCurrentMode(Theme.Auto);
            ThemeResource.ChangeTheme(Theme.Auto);
        }
    }
}
