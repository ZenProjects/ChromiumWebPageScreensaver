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
            string szDir = Environment.CurrentDirectory; // get current directory

            // try to find processs directory where they are cef ressource
            var szProcess = Process.GetCurrentProcess(); 
            // get the directory name of the process fullpath filename
            string szProcessusFullPath = Path.GetDirectoryName(szProcess.MainModule.FileName);

            // check if libcef.dll are in this directory
            if (File.Exists(szProcessusFullPath + "\\libcef.dll"))
            {
                szDir = szProcessusFullPath;
                Directory.SetCurrentDirectory(szDir);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            CefSettings settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to     specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                // configure ressource path with screensave process directory path
                ResourcesDirPath = szDir,
                // configure browsersubprocess path based on screensaver directory
                BrowserSubprocessPath = szDir + "\\CefSharp.BrowserSubprocess.exe",
                // configure locales path based on screensaver directory
                LocalesDirPath = szDir + "\\Locales\\",
                
            };

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (args.Length > 0 && args[0].ToLower().Contains("/p"))
            {
                return;
            }

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
