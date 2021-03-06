﻿//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010 _hawk_

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

//Contact: hawk.ac@gmx.net

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace BluRip
{
    /// <summary>
    /// Interaktionslogik für LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private delegate void Message(string msg);
        private MainWindow mainWindow = null;

        public LogWindow(MainWindow mainWindow)
        {            
            try
            {                
                InitializeComponent();
                this.mainWindow = mainWindow;
                checkBoxAutoscroll.IsChecked = mainWindow.Settings.autoScroll;
            }
            catch (Exception ex)
            {
                Global.ErrorMsg(ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
                mainWindow.UncheckLogWindow();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void MessageMain(string msg)
        {
            try
            {
                if (richTextBoxMainLog.Dispatcher.CheckAccess())
                {
                    richTextBoxMainLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if(checkBoxAutoscroll.IsChecked == true) richTextBoxMainLog.ScrollToEnd();
                }
                else
                {
                    richTextBoxMainLog.Dispatcher.Invoke(new Message(MessageMain), new object[] { msg });
                }                
            }
            catch (Exception)
            {
            }
        }

        public void MessageDemux(string msg)
        {
            try
            {
                if (richTextBoxDemuxLog.Dispatcher.CheckAccess())
                {
                    richTextBoxDemuxLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if (checkBoxAutoscroll.IsChecked == true) richTextBoxDemuxLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxDemuxLog.Dispatcher.Invoke(new Message(MessageDemux), new object[] { msg });
                }
                
            }
            catch (Exception)
            {
            }
        }

        public void MessageCrop(string msg)
        {
            try
            {
                if (richTextBoxCropLog.Dispatcher.CheckAccess())
                {
                    richTextBoxCropLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if (checkBoxAutoscroll.IsChecked == true) richTextBoxCropLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxCropLog.Dispatcher.Invoke(new Message(MessageCrop), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageSubtitle(string msg)
        {
            try
            {
                if (richTextBoxSubtitleLog.Dispatcher.CheckAccess())
                {
                    richTextBoxSubtitleLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if (checkBoxAutoscroll.IsChecked == true) richTextBoxSubtitleLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxSubtitleLog.Dispatcher.Invoke(new Message(MessageSubtitle), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageEncode(string msg)
        {
            try
            {
                if (richTextBoxEncodeLog.Dispatcher.CheckAccess())
                {
                    richTextBoxEncodeLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if (checkBoxAutoscroll.IsChecked == true) richTextBoxEncodeLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxEncodeLog.Dispatcher.Invoke(new Message(MessageEncode), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        public void MessageMux(string msg)
        {
            try
            {
                if (richTextBoxMuxLog.Dispatcher.CheckAccess())
                {
                    richTextBoxMuxLog.AppendText("[" + DateTime.Now.ToString() + "] " + msg + "\r");
                    if (checkBoxAutoscroll.IsChecked == true) richTextBoxMuxLog.ScrollToEnd();
                    MessageMain(msg);
                }
                else
                {
                    richTextBoxMuxLog.Dispatcher.Invoke(new Message(MessageMux), new object[] { msg });
                }

            }
            catch (Exception)
            {
            }
        }

        private void menuLogClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                richTextBoxMainLog.Document.Blocks.Clear();
                richTextBoxDemuxLog.Document.Blocks.Clear();
                richTextBoxCropLog.Document.Blocks.Clear();
                richTextBoxSubtitleLog.Document.Blocks.Clear();
                richTextBoxEncodeLog.Document.Blocks.Clear();
                richTextBoxMuxLog.Document.Blocks.Clear();
            }
            catch (Exception)
            {
            }
        }

        public void ClearAll()
        {
            try
            {
                menuLogClearAll_Click(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void menuLogClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = tabControlLog.SelectedIndex;
                switch (index)
                {
                    case 0:
                        richTextBoxMainLog.Document.Blocks.Clear();
                        break;
                    case 1:
                        richTextBoxDemuxLog.Document.Blocks.Clear();
                        break;
                    case 2:
                        richTextBoxCropLog.Document.Blocks.Clear();
                        break;
                    case 3:
                        richTextBoxSubtitleLog.Document.Blocks.Clear();
                        break;
                    case 4:
                        richTextBoxEncodeLog.Document.Blocks.Clear();
                        break;
                    case 5:
                        richTextBoxMuxLog.Document.Blocks.Clear();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void menuLogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = tabControlLog.SelectedIndex;
                switch (index)
                {
                    case 0:
                        SaveLog(richTextBoxMainLog);
                        break;
                    case 1:
                        SaveLog(richTextBoxDemuxLog);
                        break;
                    case 2:
                        SaveLog(richTextBoxCropLog);
                        break;
                    case 3:
                        SaveLog(richTextBoxSubtitleLog);
                        break;
                    case 4:
                        SaveLog(richTextBoxEncodeLog);
                        break;
                    case 5:
                        SaveLog(richTextBoxMuxLog);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                if(IsActive)
                    mainWindow.UpdateDiffLog();
            }
            catch (Exception)
            {
            }
        }

        private void SaveLog(System.Windows.Controls.RichTextBox rtb)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = Global.Res("LogSaveFileFilter");
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveLog(GetText(rtb), sfd.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private string GetText(System.Windows.Controls.RichTextBox rtb)
        {
            try
            {
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                return textRange.Text;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void SaveMainLog(string filename)
        {
            try
            {
                SaveLog(GetText(richTextBoxMainLog), filename);
            }
            catch (Exception)
            {
            }
        }

        private void SaveLog(string log, string filename)
        {
            try
            {
                string[] lines = log.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string tmp = "";
                foreach (string s in lines) tmp += s.Trim() + "\r\n";
                System.IO.File.WriteAllText(filename, tmp);
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxAutoscroll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mainWindow != null)
                {
                    mainWindow.Settings.autoScroll = (bool)checkBoxAutoscroll.IsChecked;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
