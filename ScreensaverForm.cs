using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
//using System.Threading;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;

namespace Web_Page_Screensaver
{
    public partial class ScreensaverForm : Form
    {
        private DateTime StartTime;
        private Timer timer;
        private int currentSiteIndex = -1;
        private GlobalUserEventHandler userEventHandler;
        private bool shuffleOrder;
        private List<string> urls;

        private PreferencesManager prefsManager = new PreferencesManager();

        private int screenNum;

        [ThreadStatic]
        private static Random random;

        public ScreensaverForm(int? screenNumber = null)
        {
            userEventHandler = new GlobalUserEventHandler();
            userEventHandler.Event += new GlobalUserEventHandler.UserEvent(HandleUserActivity);

            if (screenNumber == null) screenNum = prefsManager.EffectiveScreensList.FindIndex(s => s.IsPrimary);
            else screenNum = (int)screenNumber;

            InitializeComponent();

            Cursor.Hide();


        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((CefSharp.WinForms.ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() =>
            {
                //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                b.Focus();
                RotateSite();
            });
            

        }


        public List<string> Urls
        {
            get
            {
                if (urls == null)
                {
                    urls = prefsManager.GetUrlsByScreen(screenNum);
                }

                return urls;
            }
        }

        private void ScreensaverForm_Load(object sender, EventArgs e)
        {
            if (Urls.Any())
            {
                if (Urls.Count > 1)
                {

                    // Shuffle the URLs if necessary
                    shuffleOrder = prefsManager.GetRandomizeFlagByScreen(screenNum);
                    if (shuffleOrder)
                    {
                        random = new Random();

                        int n = urls.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = random.Next(n + 1);
                            var value = urls[k];
                            urls[k] = urls[n];
                            urls[n] = value;
                        }
                    }

                    // Set up timer to rotate to the next URL
                    timer = new Timer();
                    timer.Interval = prefsManager.GetRotationIntervalByScreen(screenNum) * 1000;
                    timer.Tick += (s, ee) => RotateSite();
                    timer.Start();
                    
                }

                StartTime = DateTime.Now;

            }
            else
            {
                webBrowser.Visible = false;
                closeButton.Visible = false;
            }
        }

        private void BrowseTo(string url)
        {
            // Disable the user event handler while navigating
            Application.RemoveMessageFilter(userEventHandler);

            if (string.IsNullOrWhiteSpace(url))
            {
                webBrowser.Visible = false;
            }
            else
            {
                webBrowser.Visible = true;
                try
                {
                    webBrowser.Load(url);

                }
                catch
                {
                    // This can happen if IE pops up a window that isn't closed before the next call to Navigate()
                }
            }
            Application.AddMessageFilter(userEventHandler);
        }


        private void RotateSite()
        {
            currentSiteIndex++;

            if (currentSiteIndex >= Urls.Count)
            {
                currentSiteIndex = 0;
            }

            var url = Urls[currentSiteIndex];

            BrowseTo(url);
        }

        private bool HandleUserActivity()
        {
            if (StartTime.AddSeconds(1) > DateTime.Now) return true;

            if (prefsManager.CloseOnActivity)
            {
                Close();
                return true;
            }
            else
            {
                closeButton.Visible = true;
                Cursor.Show();
            }

            return false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class GlobalUserEventHandler : IMessageFilter
    {
        public delegate bool UserEvent();

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        // screensavers and especially multi-window apps can get spurrious WM_MOUSEMOVE events
        // that don't actually involve any movement (cursor chnages and some mouse driver software
        // can generate them, for example) - so we record the actual mouse position and compare against it for actual movement.
        private Point? lastMousePos;

        public event UserEvent Event;

        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == WM_MOUSEMOVE) && (this.lastMousePos == null))
            {
                this.lastMousePos = Cursor.Position;
            }

            if (((m.Msg == WM_MOUSEMOVE) && (Cursor.Position != this.lastMousePos))
                || (m.Msg > WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK) || m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP)
            {

                if (Event != null)
                {
                    return Event();
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }
    }
}