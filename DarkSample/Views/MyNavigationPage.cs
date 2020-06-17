using Xamarin.Forms;

namespace DarkSample.Views
{
    public class MyNavigationPage:NavigationPage
    {
        public MyNavigationPage(Page root):base(root)
        {
            SetDynamicResource(BarBackgroundColorProperty, "AppBackground");
            SetDynamicResource(BarTextColorProperty, "TitleTextColor");
        }
    }
}


