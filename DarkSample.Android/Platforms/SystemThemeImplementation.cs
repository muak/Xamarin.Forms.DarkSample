using Android.Content;
using Android.Content.Res;
using AndroidX.AppCompat.App;
using DarkSample.Droid.Platforms;
using DarkSample.Platforms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(SystemThemeImplementation))]
namespace DarkSample.Droid.Platforms
{
    public class SystemThemeImplementation:ISystemTheme
    {
        public static Context Context;

        public Theme GetTheme()
        {
            var currentMode = Context.Resources.Configuration.UiMode & UiMode.NightMask;
            return currentMode switch
            {
                UiMode.NightYes => Theme.Dark,
                _ => Theme.Light
            };
        }

        public void SetTheme(Theme theme)
        {
            AppCompatDelegate.DefaultNightMode = theme switch
            {
                Theme.Dark => AppCompatDelegate.ModeNightYes,
                Theme.Light => AppCompatDelegate.ModeNightNo,
                _ => AppCompatDelegate.ModeNightFollowSystem,
            };            

            Context.GetSharedPreferences("AppSettings", FileCreationMode.Private)
                .Edit()
                .PutInt("NightKey", AppCompatDelegate.DefaultNightMode)
                .Apply();
        }
    }
}
