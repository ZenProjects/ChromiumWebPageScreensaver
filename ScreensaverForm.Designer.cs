using System;
using System.Diagnostics;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Web_Page_Screensaver
{
    partial class ScreensaverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ScreensaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 322);
            this.BackColor = System.Drawing.Color.Black;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.TopMost = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Name = "ScreensaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ScreensaverForm_Load);

            // 
            // webBrowser
            // 
            this.webBrowser = new CefSharp.WinForms.ChromiumWebBrowser("about:blank")
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Margin = new System.Windows.Forms.Padding(4),
                MinimumSize = new System.Drawing.Size(27, 25),
                Name = "webBrowser",
                Size = new System.Drawing.Size(379, 322),
                TabIndex = 0,
                Enabled = false,
                ActivateBrowserOnCreation = true,
                Visible = false,
                TabStop = false,
                AllowDrop = false,

                
            };
            Console.WriteLine(this.webBrowser.Handle);
            // ie11
            //this.webBrowser.ScriptErrorsSuppressed = true;

            this.webBrowser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            this.Controls.Add(this.webBrowser);
            // 
            // closeButton
            // 
            this.closeButton = new System.Windows.Forms.Button();
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(323, 15);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 28);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Visible = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.Controls.Add(this.closeButton);

            this.ResumeLayout(false);




        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser webBrowser;
        private System.Windows.Forms.Button closeButton;
    }
}

