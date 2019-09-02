[![Build Status](https://dev.azure.com/zen-projects/Chromium-Web-Page-Screensaver/_apis/build/status/ZenProjects.Chromium-Web-Page-Screensaver?branchName=master)](https://dev.azure.com/zen-projects/Chromium-Web-Page-Screensaver/_build/latest?definitionId=1&branchName=master)

# Chromium Web Page Screensaver

Is a Fork of the old archived project [cwc/web-page-screensaver](https://github.com/cwc/web-page-screensaver) that use Chromium [(using CefSharp WinForms)](https://github.com/cefsharp/CefSharp) in place of the old [IE 11 Embeded WinForms](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser?view=netframework-4.8) to display web pages as your screensaver.

## Dependencies

- [.NET Framework v4.5.2+](https://www.microsoft.com/en-US/download/details.aspx?id=42642 ".NET Framework")
- [CefSharp - Embedded Chromium for .NET v37+](https://cefsharp.github.io/ "CefSharp - Embedded Chromium for .NET")
- Windows 7 & up

## Download and Install the last CI Build

- click on `Azure Pipelines badges`
- click on `Summary`
- click on `Build artifacts published` download link to donwload de build zip archive.
- Unzip it
- Find `Web-Page-Screensaver.scr` in the unziped directory, right click it
- Select `Install` to install, or `Test` to test it out without installing it
- If installing it, the windows `Screen Saver Settings` dialog will pop up with the correct screen saver already selected
- Use the `Settings...` button in the same dialog to change the web page(s) list displayed by the screen saver

## Build 

- Clone the source repository
- Open the `.sln` project file with Visual Studio (i've tested only with VS 2019).
- Build in `Release` with `x86` mode
- Find `Web-Page-Screensaver.scr` in `bin/x86/Release` of your project directory, right click it
- Select `Install` to install, or `Test` to test it out without installing it
- If installing it, the windows `Screen Saver Settings` dialog will pop up with the correct screen saver already selected
- Use the `Settings...` button in the same dialog to change the web page(s) list displayed by the screen saver


