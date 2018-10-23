using System;

namespace MasterDevs.ChromeDevTools
{
    public interface IChromeProcessFactory
    {
        IChromeProcess Create(Uri uri, bool headless, bool hideScrollBars);
    }
}