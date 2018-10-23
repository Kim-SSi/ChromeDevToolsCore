using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace MasterDevs.ChromeDevTools
{
    public class ChromeProcessFactory : IChromeProcessFactory
    {
        public IDirectoryCleaner DirectoryCleaner { get; set; }
        public string ChromePath { get; }

        public ChromeProcessFactory(IDirectoryCleaner directoryCleaner, string chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe")
        {
            DirectoryCleaner = directoryCleaner;
            ChromePath = chromePath;
        }

        public IChromeProcess Create(Uri uri, bool headless, bool hideScrollBars = false)
        {            
            // Check for existing chrome
            var remoteChrome = new RemoteChromeProcess(uri);
            try
            {
                var sessionInfo = remoteChrome.GetSessionInfo().Result;
                return remoteChrome;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException &&
                    uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    // Chrome is not running locally so start it.
                    return CreateLocalChrome(uri, headless, hideScrollBars);
                }
                throw;
            }
        }

        private IChromeProcess CreateLocalChrome(Uri uri, bool headless, bool hideScrollBars = false)
        {
            string path = Path.GetRandomFileName();
            var directoryInfo = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), path));
            var remoteDebuggingArg = $"--remote-debugging-port={uri.Port}";
            var userDirectoryArg = $"--user-data-dir=\"{directoryInfo.FullName}\"";
            const string headlessArg = "--headless --disable-gpu";
            var chromeProcessArgs = new List<string>
            {
                remoteDebuggingArg,
                userDirectoryArg,
                "--bwsi",
                "--no-first-run"
            };
            if (headless)
                chromeProcessArgs.Add(headlessArg);

            if (hideScrollBars)
                chromeProcessArgs.Add("--hide-scrollbars");

            var processStartInfo = new ProcessStartInfo(ChromePath, string.Join(" ", chromeProcessArgs));
            var chromeProcess = Process.Start(processStartInfo);

            return new LocalChromeProcess(uri, () => DirectoryCleaner.Delete(directoryInfo), chromeProcess);
        }
    }
}