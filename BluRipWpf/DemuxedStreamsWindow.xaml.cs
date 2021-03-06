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
    /// Interaktionslogik für DemuxedStreamsWindow.xaml
    /// </summary>
    public partial class DemuxedStreamsWindow : Window
    {
        private MainWindow mainWindow = null;

        public DemuxedStreamsWindow(MainWindow mainWindow)
        {            
            try
            {
                InitializeComponent();
                this.mainWindow = mainWindow;
            }
            catch(Exception ex)
            {
                Global.ErrorMsg(ex.Message);
            }
        }

        public void UpdateDemuxedStreams()
        {
            try
            {
                listBoxDemuxedStreams.Items.Clear();
                foreach (StreamInfo si in mainWindow.DemuxedStreams.streams)
                {
                    listBoxDemuxedStreams.Items.Add(si.desc + " - " + si.filename);
                }
                mainWindow.UpdateBitrate();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
                mainWindow.UncheckDemuxedStreamsWindow();
            }
            catch (Exception)
            {
            }
        }

        private void buttonLoadDemuxedStreams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*_streamInfo.xml|*.xml";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TitleInfo ti = new TitleInfo();
                    TitleInfo.LoadSettingsFile(ref ti, ofd.FileName);
                    mainWindow.DemuxedStreams = new TitleInfo(ti);
                    UpdateDemuxedStreams();
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
                    mainWindow.UpdateDiffDemuxedStreams();
            }
            catch (Exception)
            {
            }
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index > 0)
                {
                    StreamInfo si = mainWindow.DemuxedStreams.streams[index];
                    mainWindow.DemuxedStreams.streams.RemoveAt(index);
                    mainWindow.DemuxedStreams.streams.Insert(index - 1, si);
                    UpdateDemuxedStreams();
                    listBoxDemuxedStreams.SelectedIndex = index - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index > -1 && index < mainWindow.DemuxedStreams.streams.Count - 1)
                {
                    StreamInfo si = mainWindow.DemuxedStreams.streams[index];
                    mainWindow.DemuxedStreams.streams.RemoveAt(index);
                    mainWindow.DemuxedStreams.streams.Insert(index + 1, si);
                    UpdateDemuxedStreams();
                    listBoxDemuxedStreams.SelectedIndex = index + 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index > -1)
                {
                    mainWindow.DemuxedStreams.streams.RemoveAt(index);
                    UpdateDemuxedStreams();
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "*_streamInfo.xml|*.xml";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TitleInfo.SaveStreamInfoFile(mainWindow.DemuxedStreams, sfd.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxDemuxedStreams_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = listBoxDemuxedStreams.SelectedIndex;
                if (index > -1)
                {
                    EditStreamInfoWindow esiw = new EditStreamInfoWindow(mainWindow.DemuxedStreams.streams[index]);
                    esiw.ShowDialog();
                    if (esiw.DialogResult == true)
                    {
                        mainWindow.DemuxedStreams.streams[index] = new StreamInfo(esiw.streamInfo);
                        UpdateDemuxedStreams();
                        listBoxDemuxedStreams.SelectedIndex = index;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamInfo si = new StreamInfo();
                EditStreamInfoWindow esiw = new EditStreamInfoWindow(si);
                esiw.ShowDialog();
                if (esiw.DialogResult == true)
                {
                    mainWindow.DemuxedStreams.streams.Add(esiw.streamInfo);
                    UpdateDemuxedStreams();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
