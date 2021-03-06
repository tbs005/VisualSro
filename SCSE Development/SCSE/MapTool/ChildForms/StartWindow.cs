﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapTool.ChildForms
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void bgwProcessNavmesh_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwProcessNavmesh.ReportProgress(0, "Opening Media.pk2");
            Codes.MediaPK2 = new Framework.PK2.cPK2Reader("D:\\Games\\rSRO\\Media.pk2");
            Codes.MediaPK2.Load();
            bgwProcessNavmesh.ReportProgress(1, "Opening Data.pk2");
            Codes.DataPK2 = new Framework.PK2.cPK2Reader("D:\\Games\\rSRO\\Data.pk2");
            Codes.DataPK2.Load();
            bgwProcessNavmesh.ReportProgress(2, "Print Content");
            Codes.MediaPK2.PrintContent();
            Codes.DataPK2.PrintContent();

            bgwProcessNavmesh.ReportProgress(5, "Loading Navmesh");

            Codes.nvmEngine = new Framework.Navmesh.NavmeshEngine(new int[1] { 24758 }, ref Codes.MediaPK2, ref Codes.DataPK2);

            bgwProcessNavmesh.ReportProgress(100, "Spawning Player");
        }

        private void bgwProcessNavmesh_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblCurrentStep.Text = (string)e.UserState;
            pbLoadProgress.Value = e.ProgressPercentage;
        }

        private void bgwProcessNavmesh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.Parent.SpawnPlayer();
            this.Close();
        }

        private void btnSpawn_Click(object sender, EventArgs e)
        {
            btnSpawn.Enabled = false;

            //Deactivate Settings
            panalSettings.Enabled = false;
            panalSettings.Visible = false;

            panalProgress.Visible = true;

            bgwProcessNavmesh.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            if (bgwProcessNavmesh.IsBusy)
            {

            }
            Application.Exit(new CancelEventArgs(true));
        }
    }
}
