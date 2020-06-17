using DarkSample.iOS.Platforms;
using DarkSample.Platforms;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SystemThemeImplementation))]
namespace DarkSample.iOS.Platforms
{
    public class SystemThemeImplementation:ISystemTheme
    {
        public Theme GetTheme()
        {
            return UITraitCollection.CurrentTraitCollection.UserInterfaceStyle switch
            {
                UIUserInterfaceStyle.Dark => Theme.Dark,
                _ => Theme.Light
            };
        }

        public void SetTheme(Theme theme)
        {
            UIApplication.SharedApplication.KeyWindow.OverrideUserInterfaceStyle = theme switch
            {
                Theme.Dark => UIUserInterfaceStyle.Dark,
                Theme.Light => UIUserInterfaceStyle.Light,
                _ => UITraitCollection.CurrentTraitCollection.UserInterfaceStyle
            };            
        }
    }
}
