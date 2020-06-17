using System;
using System.Linq;
using DarkSample.Platforms;
using Xamarin.Forms;

namespace DarkSample
{
    public static class ThemeResource
    {
        static Theme LastSystemTheme = Theme.Light;

        public static T GetResource<T>(string key)
        {
            if(Application.Current.Resources.TryGetValue(key,out var value))
            {
                return (T)value;
            }            

            return (T)Application.Current.Resources.MergedDictionaries.First()[key];
        }

        public static void ApplyTheme()
        {
            var theme = (Theme)Xamarin.Essentials.Preferences.Get("mode", (int)Theme.Light);
            var sysTheme = DependencyService.Get<ISystemTheme>();
            if (Device.RuntimePlatform == Device.iOS)
            {
                // iOSのみシステムテーマ変更
                sysTheme.SetTheme(theme);
                // AndroidはPlatform側の起動時に設定
            }

            if (theme == Theme.Auto)
            {
                theme = sysTheme.GetTheme();
                // Autoであれば端末のモード切り替えを待ち受ける
                App.OnForeground += App_OnForeground;
                LastSystemTheme = theme;
            }

            SetDictionary(theme);
        }

        public static void ChangeTheme(Theme theme)
        {
            Xamarin.Essentials.Preferences.Set("mode", (int)theme);

            var sysTheme = DependencyService.Get<ISystemTheme>();

            // PlatformのThemeを変更
            sysTheme.SetTheme(theme);

            App.OnForeground -= App_OnForeground;
            if (theme == Theme.Auto)
            {
                theme = sysTheme.GetTheme();
                // Autoであれば端末のモード切り替えを待ち受ける
                App.OnForeground += App_OnForeground;
                LastSystemTheme = theme;
            }

            SetDictionary(theme);
        }

        static void SetDictionary(Theme theme)
        {
            var mergedDict = Application.Current.Resources.MergedDictionaries;
            mergedDict.Clear();

            switch (theme)
            {
                case Theme.Light:
                    mergedDict.Add(new LightTheme());
                    break;
                case Theme.Dark:
                    mergedDict.Add(new DarkTheme());
                    break;
            }
        }

        private static void App_OnForeground(object sender, EventArgs e)
        {
            var sysTheme = DependencyService.Get<ISystemTheme>();
            var curTheme = sysTheme.GetTheme();
            // 前回とテーマが変わらなければ何もしない
            if (curTheme == LastSystemTheme)
            {
                return;
            }

            // PlatformのThemeを変更
            sysTheme.SetTheme(curTheme);

            SetDictionary(curTheme);

            LastSystemTheme = curTheme;
        }

    }
}
