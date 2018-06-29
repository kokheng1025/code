namespace VB6ActiveXDllConverterUI
{
    partial class frmMain
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
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.btnConvertStart = new System.Windows.Forms.Button();
            this.btnBrowseConvertVBNet = new System.Windows.Forms.Button();
            this.btnBrowseConvertVB6 = new System.Windows.Forms.Button();
            this.txtConvertVBNetPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConvertVB6Project = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwStatus = new System.Windows.Forms.ListView();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.btnConvertStart);
            this.splMain.Panel1.Controls.Add(this.btnBrowseConvertVBNet);
            this.splMain.Panel1.Controls.Add(this.btnBrowseConvertVB6);
            this.splMain.Panel1.Controls.Add(this.txtConvertVBNetPath);
            this.splMain.Panel1.Controls.Add(this.label2);
            this.splMain.Panel1.Controls.Add(this.txtConvertVB6Project);
            this.splMain.Panel1.Controls.Add(this.label1);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.lvwStatus);
            this.splMain.Size = new System.Drawing.Size(1344, 729);
            this.splMain.SplitterDistance = 180;
            this.splMain.TabIndex = 0;
            // 
            // btnConvertStart
            // 
            this.btnConvertStart.Location = new System.Drawing.Point(11, 125);
            this.btnConvertStart.Name = "btnConvertStart";
            this.btnConvertStart.Size = new System.Drawing.Size(162, 34);
            this.btnConvertStart.TabIndex = 6;
            this.btnConvertStart.Text = "Start";
            this.btnConvertStart.UseVisualStyleBackColor = true;
            this.btnConvertStart.Click += new System.EventHandler(this.btnConvertStart_Click);
            // 
            // btnBrowseConvertVBNet
            // 
            this.btnBrowseConvertVBNet.Location = new System.Drawing.Point(1230, 93);
            this.btnBrowseConvertVBNet.Name = "btnBrowseConvertVBNet";
            this.btnBrowseConvertVBNet.Size = new System.Drawing.Size(93, 23);
            this.btnBrowseConvertVBNet.TabIndex = 5;
            this.btnBrowseConvertVBNet.Text = "Browse...";
            this.btnBrowseConvertVBNet.UseVisualStyleBackColor = true;
            this.btnBrowseConvertVBNet.Click += new System.EventHandler(this.btnBrowseConvertVBNet_Click);
            // 
            // btnBrowseConvertVB6
            // 
            this.btnBrowseConvertVB6.Location = new System.Drawing.Point(1230, 36);
            this.btnBrowseConvertVB6.Name = "btnBrowseConvertVB6";
            this.btnBrowseConvertVB6.Size = new System.Drawing.Size(93, 23);
            this.btnBrowseConvertVB6.TabIndex = 2;
            this.btnBrowseConvertVB6.Text = "Browse...";
            this.btnBrowseConvertVB6.UseVisualStyleBackColor = true;
            this.btnBrowseConvertVB6.Click += new System.EventHandler(this.btnBrowseConvertVB6_Click);
            // 
            // txtConvertVBNetPath
            // 
            this.txtConvertVBNetPath.Location = new System.Drawing.Point(11, 95);
            this.txtConvertVBNetPath.Name = "txtConvertVBNetPath";
            this.txtConvertVBNetPath.ReadOnly = true;
            this.txtConvertVBNetPath.Size = new System.Drawing.Size(1210, 20);
            this.txtConvertVBNetPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Save VB.Net Project to:";
            // 
            // txtConvertVB6Project
            // 
            this.txtConvertVB6Project.Location = new System.Drawing.Point(11, 38);
            this.txtConvertVB6Project.Name = "txtConvertVB6Project";
            this.txtConvertVB6Project.ReadOnly = true;
            this.txtConvertVB6Project.Size = new System.Drawing.Size(1210, 20);
            this.txtConvertVB6Project.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "VB6 Project File:";
            // 
            // lvwStatus
            // 
            this.lvwStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus});
            this.lvwStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwStatus.FullRowSelect = true;
            this.lvwStatus.GridLines = true;
            this.lvwStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwStatus.Location = new System.Drawing.Point(0, 0);
            this.lvwStatus.MultiSelect = false;
            this.lvwStatus.Name = "lvwStatus";
            this.lvwStatus.ShowGroups = false;
            this.lvwStatus.Size = new System.Drawing.Size(1344, 545);
            this.lvwStatus.TabIndex = 0;
            this.lvwStatus.UseCompatibleStateImageBehavior = false;
            this.lvwStatus.View = System.Windows.Forms.View.Details;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 1300;
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "vbp";
            this.fileDialog.Filter = "VB6 Project files|*.vbp";
            // 
            // folderDialog
            // 
            this.folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 729);
            this.Controls.Add(this.splMain);
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VB6 ActiveX DLL Converter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel1.PerformLayout();
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.ListView lvwStatus;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button btnConvertStart;
        private System.Windows.Forms.Button btnBrowseConvertVBNet;
        private System.Windows.Forms.Button btnBrowseConvertVB6;
        private System.Windows.Forms.TextBox txtConvertVBNetPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConvertVB6Project;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}

