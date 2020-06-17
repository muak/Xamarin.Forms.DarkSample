using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using DarkSample.Droid.Platforms;

namespace DarkSample.Droid
{
    [Activity(Label = "DarkSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var theme = GetSharedPreferences("AppSettings", FileCreationMode.Private)
                .GetInt("NightKey", AppCompatDelegate.ModeNightNo);
            AppCompatDelegate.DefaultNightMode = theme;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;            

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            SystemThemeImplementation.Context = this;

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                // モードによってステータスバーのアイコンのモードを変える。
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                var uimode = Resources.Configuration.UiMode & UiMode.NightMask;
                Window.DecorView.SystemUiVisibility = uimode == UiMode.NightNo ? flag : 0;
            }

            var app = new App();
            LoadApplication(app);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}