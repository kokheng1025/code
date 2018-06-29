using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VB6ActiveXDllConverter.Converters;

namespace VB6ActiveXDllConverterUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //// Quick test only
            //txtConvertVB6Project.Text = @"E:\Dev\Dev_ANTLR\VB6ActiveXDllConverterDemoV2\VB6-TestConvert\EInvCalculation.dll\EInvCalculation.vbp";
            //txtConvertVBNetPath.Text = @"E:\Dev\Dev_ANTLR\VB6ActiveXDllConverterDemoV2\VBNet-TestConvertResult";
        }

        private void btnBrowseConvertVB6_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtConvertVB6Project.Text = fileDialog.FileName;
            }
        }

        private void btnBrowseConvertVBNet_Click(object sender, EventArgs e)
        {
            folderDialog.SelectedPath = Directory.GetCurrentDirectory(); // default
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtConvertVBNetPath.Text = folderDialog.SelectedPath;
            }
        }

        private void btnConvertStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtConvertVB6Project.Text) || string.IsNullOrWhiteSpace(txtConvertVBNetPath.Text))
                    return;

                splMain.Panel1.Enabled = false;

                if (bgWorker.IsBusy != true)
                {
                    bgWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var cv = new VB6ActveXDllConverter();
            cv.Convert(txtConvertVB6Project.Text, txtConvertVBNetPath.Text);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    MessageBox.Show($"Error: {e.Error.Message}");
                else
                    MessageBox.Show("Done");
            }
            finally
            {
                splMain.Panel1.Enabled = true;
            }
        }
    }
}
