﻿using System.Windows;
using System.IO;
using System;
using System.Collections.Generic;
using BatchRenamer.Windows;

namespace BatchRenamer
{
    public partial class MainWindow : Window
    {
        private string[] FromArr { get; set; }
        private string[] ToArr { get; set; }
        private Logger MyLogger;

        public MainWindow()
        {
            InitializeComponent();
            MyLogger = Logger.Instance;

            PathBox.TextChanged += ValidatePath_TextChanged;
            From.TextChanged += ValidateFromTo_TextChanged;
            To.TextChanged += ValidateFromTo_TextChanged;
            RenameBtn.Click += RenameBtn_Click;
        }

        private void ValidatePath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) // enables disables From To Textboxes
        {
            if (Directory.Exists(PathBox.Text))
            {
                From.IsEnabled = true;
                To.IsEnabled = true;
            }
            else
            {
                From.IsEnabled = false;
                To.IsEnabled = false;
            }
        }

        private void ValidateFromTo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) // enables disables RenameBtn
        {
            if (From.Text != String.Empty || To.Text != String.Empty) {
                FromArr = From.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                ToArr = To.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (FromArr.Length == ToArr.Length)
                {
                    RenameBtn.IsEnabled = true;
                }
                else
                {
                    RenameBtn.IsEnabled = false;
                }
            }
        }

        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            string from, to;

            List<string> pathsThatDontExist = new List<String>();

            for (int i = 0; i < FromArr.Length; i++) // validation
            {
                from = Path.Combine(PathBox.Text, FromArr[i]);
                if (!File.Exists(from))
                {
                    pathsThatDontExist.Add(from);
                }
                    
            }

            if (pathsThatDontExist.Count > 0)
            {
                string renameError = "The following files are not files or do not exist:" + "\n\n" + string.Join(Environment.NewLine, pathsThatDontExist) + "\n\n" + "Please rectify this error!";
                MyLogger.WriteLog(renameError);
                MessageBox.Show(renameError);
                return;
            }

            for (int i = 0; i < FromArr.Length; i++)
            {
                from = Path.Combine(PathBox.Text, FromArr[i]);
                to = Path.Combine(PathBox.Text, ToArr[i]);
                if ((bool)KeepExtensionChkBox.IsChecked)
                {
                    to = Path.ChangeExtension(to, null); // get filename with path without the extension
                    to = to + Path.GetExtension(from);
                    File.Move(from, to);
                    MyLogger.WriteLog("Renamed " + from + " to " + to);
                }
                else
                {
                    File.Move(from, to);
                    MyLogger.WriteLog("Renamed " + from + " to " + to);
                }
                
            }

            string renameSuccess = "All files have been successfully renamed!";
            MyLogger.WriteLog(renameSuccess);
            MessageBox.Show(renameSuccess);
        }

        private void ViewLogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Log.IsOpen)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Log)
                    {
                        window.Activate();
                    }
                }
            }
            else
            {
                var logWindow = new Log();
                logWindow.Show();
            }
        }
    }
}
