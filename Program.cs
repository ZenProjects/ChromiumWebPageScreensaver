using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using CefSharp;
using CefSharp.WinForms;

namespace Web_Page_Screensaver
{
    using System.Collections.Generic;
    using System.Drawing;

    static class Program
    {
        public static readonly string KEY = "Software\\Web-Page-Screensaver";

        [STAThread]
        static void Main(string[] args)
        {
            //MessageBox.Show("Screensaver directory:"+ Environment.CurrentDirectory, "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            //string szDir = "C:\\Donnees\\cefsharp";
            string szDir = Environment.CurrentDirectory;
            //Directory.SetCurrentDirectory(szDir);
            

            // Set version of embedded browser (http://weblog.west-wind.com/posts/2011/May/21/Web-Browser-Control-Specifying-the-IE-Version)
            //var exeName = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", exeName, 0x2AF8, RegistryValueKind.DWord);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            // https://docs.microsoft.com/fr-fr/windows/win32/sbscs/application-configuration-files#example
            // https://docs.microsoft.com/fr-fr/cpp/build/adding-references-in-visual-cpp-projects?view=vs-2019
            // https://github.com/dotnet/docs/blob/master/docs/framework/tools/gacutil-exe-gac-tool.md 

            /*
 C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp>gacutil /il gac.txt
Microsoft (R) .NET Global Assembly Cache Utility.  Version 4.0.30319.0
Copyright (c) Microsoft Corporation. Tous droits réservés.

Assembly C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp\CefSharp.BrowserSubprocess.Core.dll ajouté au cache.
Assembly C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp\CefSharp.BrowserSubprocess.exe ajouté au cache.
Assembly C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp\CefSharp.Core.dll ajouté au cache.
Assembly C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp\CefSharp.dll ajouté au cache.
Assembly C:\Users\u038472\source\repos\web-page-screensaver\web-page-screensaver\bin\cefsharp\CefSharp.WinForms.dll ajouté au cache.

Nombre d'assemblys traités = 5
Nombre d'assemblys installés = 5
Nombre d'échecs = 0

              */
            CefSettings settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to     specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                ResourcesDirPath = szDir,
                BrowserSubprocessPath = szDir + "\\CefSharp.BrowserSubprocess.exe",
                LocalesDirPath = szDir + "\\Locales\\",
                
            };

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (args.Length > 0 && args[0].ToLower().Contains("/p"))
                return;

            if (args.Length > 0 && args[0].ToLower().Contains("/c"))
            {
                Application.Run(new PreferencesForm());
            }
            else
            {
                var formsList = new List<Form>();
                var screens = (new PreferencesManager()).EffectiveScreensList;
                foreach (var screen in screens)
                {
                    var screensaverForm = new ScreensaverForm(screen.ScreenNum)
                    {
                        Location = new Point(screen.Bounds.Left, screen.Bounds.Top),
                        Size = new Size(screen.Bounds.Width, screen.Bounds.Height)
                    };

                    FormStartPosition x = screensaverForm.StartPosition;

                    formsList.Add(screensaverForm);
                }

                Application.Run(new MultiFormContext(formsList));
            }

            Cef.Shutdown();
        }
    }

    public class MultiFormContext : ApplicationContext
    {
        public MultiFormContext(List<Form> forms)
        {
            foreach (var form in forms)
            {
                form.FormClosed += (s, args) =>
                {
                    //When we have closed any form, 
                    //end the program.
                        ExitThread();
                };

                form.Show();
            }
        }
    }
}
