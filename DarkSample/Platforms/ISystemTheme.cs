using System;
namespace DarkSample.Platforms
{
    public interface ISystemTheme
    {
        void SetTheme(Theme theme);
        Theme GetTheme();
    }
}
