# Chromium Web Page Screensaver

Is a Fork of old archived project [cwc/web-page-screensaver](https://github.com/cwc/web-page-screensaver) that use [IE 11 Embeded WinForms](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser?view=netframework-4.8) that display web pages as your screensaver using Chromium [(using CEFSHARP)](https://github.com/cefsharp/CefSharp).

## Dependencies

- [.NET Framework v4.5.2+](https://www.microsoft.com/en-US/download/details.aspx?id=42642 ".NET Framework")
- [CefSharp - Embedded Chromium for .NET v37+](https://cefsharp.github.io/ "CefSharp - Embedded Chromium for .NET")

## Usage (Windows 7 & up)

1. Open the `.sln` project file with Visual Studio (i've tested only with VS 2019).
2. Build in `Release` with `x86` mode
1. Find `Web-Page-Screensaver.scr` in `bin/x86/Release` of your project directory, right click it
2. Select `Install` to install, or `Test` to test it out without installing it
3. If installing it, the windows `Screen Saver Settings` dialog will pop up with the correct screen saver already selected
4. Use the `Settings...` button in the same dialog to change the web page(s) list displayed by the screen saver
