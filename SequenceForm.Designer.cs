namespace DataFusionApp
{
    partial class SequenceForm
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
            this.components = new System.ComponentModel.Container();
            this.ProcessTab = new System.Windows.Forms.TabControl();
            this.fromPcap = new System.Windows.Forms.TabPage();
            this.cyRawButton = new System.Windows.Forms.Button();
            this.fromPyShark = new System.Windows.Forms.TabPage();
            this.pySharkButton = new System.Windows.Forms.Button();
            this.fromPacketBeat = new System.Windows.Forms.TabPage();
            this.packetBeatButton = new System.Windows.Forms.Button();
            this.fromSnort = new System.Windows.Forms.TabPage();
            this.snortButton = new System.Windows.Forms.Button();
            this.phyFromPcap = new System.Windows.Forms.TabPage();
            this.phyRawButton = new System.Windows.Forms.Button();
            this.mergeCyPhy = new System.Windows.Forms.TabPage();
            this.mergeButton = new System.Windows.Forms.Button();
            this.imputate = new System.Windows.Forms.TabPage();
            this.imputationButton = new System.Windows.Forms.Button();
            this.encoding = new System.Windows.Forms.TabPage();
            this.encodingButton = new System.Windows.Forms.Button();
            this.plotAndSave = new System.Windows.Forms.TabPage();
            this.plotButton = new System.Windows.Forms.Button();
            this.featureListBox = new System.Windows.Forms.ListBox();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.formsPlot4 = new ScottPlot.FormsPlot();
            this.ProcessTab.SuspendLayout();
            this.fromPcap.SuspendLayout();
            this.fromPyShark.SuspendLayout();
            this.fromPacketBeat.SuspendLayout();
            this.fromSnort.SuspendLayout();
            this.phyFromPcap.SuspendLayout();
            this.mergeCyPhy.SuspendLayout();
            this.imputate.SuspendLayout();
            this.encoding.SuspendLayout();
            this.plotAndSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessTab
            // 
            this.ProcessTab.Controls.Add(this.fromPcap);
            this.ProcessTab.Controls.Add(this.fromPyShark);
            this.ProcessTab.Controls.Add(this.fromPacketBeat);
            this.ProcessTab.Controls.Add(this.fromSnort);
            this.ProcessTab.Controls.Add(this.phyFromPcap);
            this.ProcessTab.Controls.Add(this.mergeCyPhy);
            this.ProcessTab.Controls.Add(this.imputate);
            this.ProcessTab.Controls.Add(this.encoding);
            this.ProcessTab.Controls.Add(this.plotAndSave);
            this.ProcessTab.Location = new System.Drawing.Point(4, 12);
            this.ProcessTab.Name = "ProcessTab";
            this.ProcessTab.SelectedIndex = 0;
            this.ProcessTab.Size = new System.Drawing.Size(773, 426);
            this.ProcessTab.TabIndex = 0;
            // 
            // fromPcap
            // 
            this.fromPcap.Controls.Add(this.cyRawButton);
            this.fromPcap.Location = new System.Drawing.Point(4, 22);
            this.fromPcap.Name = "fromPcap";
            this.fromPcap.Padding = new System.Windows.Forms.Padding(3);
            this.fromPcap.Size = new System.Drawing.Size(765, 400);
            this.fromPcap.TabIndex = 0;
            this.fromPcap.Text = "Cyber Raw Features";
            this.fromPcap.UseVisualStyleBackColor = true;
            // 
            // cyRawButton
            // 
            this.cyRawButton.Location = new System.Drawing.Point(680, 370);
            this.cyRawButton.Name = "cyRawButton";
            this.cyRawButton.Size = new System.Drawing.Size(78, 23);
            this.cyRawButton.TabIndex = 0;
            this.cyRawButton.Text = "Next >>";
            this.cyRawButton.UseVisualStyleBackColor = true;
            this.cyRawButton.Click += new System.EventHandler(this.cyRawButton_Click);
            // 
            // fromPyShark
            // 
            this.fromPyShark.Controls.Add(this.pySharkButton);
            this.fromPyShark.Location = new System.Drawing.Point(4, 22);
            this.fromPyShark.Name = "fromPyShark";
            this.fromPyShark.Padding = new System.Windows.Forms.Padding(3);
            this.fromPyShark.Size = new System.Drawing.Size(765, 400);
            this.fromPyShark.TabIndex = 1;
            this.fromPyShark.Text = "From PyShark";
            this.fromPyShark.UseVisualStyleBackColor = true;
            // 
            // pySharkButton
            // 
            this.pySharkButton.Location = new System.Drawing.Point(680, 370);
            this.pySharkButton.Name = "pySharkButton";
            this.pySharkButton.Size = new System.Drawing.Size(78, 23);
            this.pySharkButton.TabIndex = 1;
            this.pySharkButton.Text = "Next >>";
            this.pySharkButton.UseVisualStyleBackColor = true;
            this.pySharkButton.Click += new System.EventHandler(this.pySharkButton_Click);
            // 
            // fromPacketBeat
            // 
            this.fromPacketBeat.Controls.Add(this.packetBeatButton);
            this.fromPacketBeat.Location = new System.Drawing.Point(4, 22);
            this.fromPacketBeat.Name = "fromPacketBeat";
            this.fromPacketBeat.Padding = new System.Windows.Forms.Padding(3);
            this.fromPacketBeat.Size = new System.Drawing.Size(765, 400);
            this.fromPacketBeat.TabIndex = 2;
            this.fromPacketBeat.Text = "From Packetbeat";
            this.fromPacketBeat.UseVisualStyleBackColor = true;
            // 
            // packetBeatButton
            // 
            this.packetBeatButton.Location = new System.Drawing.Point(680, 370);
            this.packetBeatButton.Name = "packetBeatButton";
            this.packetBeatButton.Size = new System.Drawing.Size(78, 23);
            this.packetBeatButton.TabIndex = 1;
            this.packetBeatButton.Text = "Next >>";
            this.packetBeatButton.UseVisualStyleBackColor = true;
            this.packetBeatButton.Click += new System.EventHandler(this.packetBeatButton_Click);
            // 
            // fromSnort
            // 
            this.fromSnort.Controls.Add(this.snortButton);
            this.fromSnort.Location = new System.Drawing.Point(4, 22);
            this.fromSnort.Name = "fromSnort";
            this.fromSnort.Padding = new System.Windows.Forms.Padding(3);
            this.fromSnort.Size = new System.Drawing.Size(765, 400);
            this.fromSnort.TabIndex = 3;
            this.fromSnort.Text = "From Snort";
            this.fromSnort.UseVisualStyleBackColor = true;
            // 
            // snortButton
            // 
            this.snortButton.Location = new System.Drawing.Point(681, 370);
            this.snortButton.Name = "snortButton";
            this.snortButton.Size = new System.Drawing.Size(78, 23);
            this.snortButton.TabIndex = 1;
            this.snortButton.Text = "Next >>";
            this.snortButton.UseVisualStyleBackColor = true;
            this.snortButton.Click += new System.EventHandler(this.snortButton_Click);
            // 
            // phyFromPcap
            // 
            this.phyFromPcap.Controls.Add(this.phyRawButton);
            this.phyFromPcap.Location = new System.Drawing.Point(4, 22);
            this.phyFromPcap.Name = "phyFromPcap";
            this.phyFromPcap.Padding = new System.Windows.Forms.Padding(3);
            this.phyFromPcap.Size = new System.Drawing.Size(765, 400);
            this.phyFromPcap.TabIndex = 4;
            this.phyFromPcap.Text = "Phy Raw Feature";
            this.phyFromPcap.UseVisualStyleBackColor = true;
            // 
            // phyRawButton
            // 
            this.phyRawButton.Location = new System.Drawing.Point(680, 370);
            this.phyRawButton.Name = "phyRawButton";
            this.phyRawButton.Size = new System.Drawing.Size(78, 23);
            this.phyRawButton.TabIndex = 1;
            this.phyRawButton.Text = "Next >>";
            this.phyRawButton.UseVisualStyleBackColor = true;
            this.phyRawButton.Click += new System.EventHandler(this.phyRawButton_Click);
            // 
            // mergeCyPhy
            // 
            this.mergeCyPhy.Controls.Add(this.mergeButton);
            this.mergeCyPhy.Location = new System.Drawing.Point(4, 22);
            this.mergeCyPhy.Name = "mergeCyPhy";
            this.mergeCyPhy.Padding = new System.Windows.Forms.Padding(3);
            this.mergeCyPhy.Size = new System.Drawing.Size(765, 400);
            this.mergeCyPhy.TabIndex = 5;
            this.mergeCyPhy.Text = "Merge Cy-Phy";
            this.mergeCyPhy.UseVisualStyleBackColor = true;
            // 
            // mergeButton
            // 
            this.mergeButton.Location = new System.Drawing.Point(680, 370);
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.Size = new System.Drawing.Size(78, 23);
            this.mergeButton.TabIndex = 1;
            this.mergeButton.Text = "Next >>";
            this.mergeButton.UseVisualStyleBackColor = true;
            this.mergeButton.Click += new System.EventHandler(this.mergeButton_Click);
            // 
            // imputate
            // 
            this.imputate.Controls.Add(this.imputationButton);
            this.imputate.Location = new System.Drawing.Point(4, 22);
            this.imputate.Name = "imputate";
            this.imputate.Padding = new System.Windows.Forms.Padding(3);
            this.imputate.Size = new System.Drawing.Size(765, 400);
            this.imputate.TabIndex = 6;
            this.imputate.Text = "Imputation";
            this.imputate.UseVisualStyleBackColor = true;
            // 
            // imputationButton
            // 
            this.imputationButton.Location = new System.Drawing.Point(680, 370);
            this.imputationButton.Name = "imputationButton";
            this.imputationButton.Size = new System.Drawing.Size(78, 23);
            this.imputationButton.TabIndex = 1;
            this.imputationButton.Text = "Next >>";
            this.imputationButton.UseVisualStyleBackColor = true;
            this.imputationButton.Click += new System.EventHandler(this.imputationButton_Click);
            // 
            // encoding
            // 
            this.encoding.Controls.Add(this.encodingButton);
            this.encoding.Location = new System.Drawing.Point(4, 22);
            this.encoding.Name = "encoding";
            this.encoding.Padding = new System.Windows.Forms.Padding(3);
            this.encoding.Size = new System.Drawing.Size(765, 400);
            this.encoding.TabIndex = 7;
            this.encoding.Text = "Encoding";
            this.encoding.UseVisualStyleBackColor = true;
            // 
            // encodingButton
            // 
            this.encodingButton.Location = new System.Drawing.Point(680, 370);
            this.encodingButton.Name = "encodingButton";
            this.encodingButton.Size = new System.Drawing.Size(78, 23);
            this.encodingButton.TabIndex = 1;
            this.encodingButton.Text = "Next >>";
            this.encodingButton.UseVisualStyleBackColor = true;
            this.encodingButton.Click += new System.EventHandler(this.encodingButton_Click);
            // 
            // plotAndSave
            // 
            this.plotAndSave.Controls.Add(this.formsPlot4);
            this.plotAndSave.Controls.Add(this.formsPlot3);
            this.plotAndSave.Controls.Add(this.formsPlot2);
            this.plotAndSave.Controls.Add(this.formsPlot1);
            this.plotAndSave.Controls.Add(this.featureListBox);
            this.plotAndSave.Controls.Add(this.plotButton);
            this.plotAndSave.Location = new System.Drawing.Point(4, 22);
            this.plotAndSave.Name = "plotAndSave";
            this.plotAndSave.Padding = new System.Windows.Forms.Padding(3);
            this.plotAndSave.Size = new System.Drawing.Size(765, 400);
            this.plotAndSave.TabIndex = 8;
            this.plotAndSave.Text = "Plot & Save";
            this.plotAndSave.UseVisualStyleBackColor = true;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(6, 367);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(120, 27);
            this.plotButton.TabIndex = 1;
            this.plotButton.Text = "Plot";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.PlotButton_Click);
            // 
            // featureListBox
            // 
            this.featureListBox.FormattingEnabled = true;
            this.featureListBox.Location = new System.Drawing.Point(6, 6);
            this.featureListBox.Name = "featureListBox";
            this.featureListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.featureListBox.Size = new System.Drawing.Size(120, 355);
            this.featureListBox.TabIndex = 2;
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(132, 17);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(626, 87);
            this.formsPlot1.TabIndex = 3;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(133, 110);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(626, 87);
            this.formsPlot2.TabIndex = 4;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Location = new System.Drawing.Point(132, 203);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(626, 87);
            this.formsPlot3.TabIndex = 5;
            // 
            // formsPlot4
            // 
            this.formsPlot4.Location = new System.Drawing.Point(132, 296);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(626, 87);
            this.formsPlot4.TabIndex = 6;
            // 
            // SequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 439);
            this.Controls.Add(this.ProcessTab);
            this.Name = "SequenceForm";
            this.Text = "Data Processing Steps";
            this.ProcessTab.ResumeLayout(false);
            this.fromPcap.ResumeLayout(false);
            this.fromPyShark.ResumeLayout(false);
            this.fromPacketBeat.ResumeLayout(false);
            this.fromSnort.ResumeLayout(false);
            this.phyFromPcap.ResumeLayout(false);
            this.mergeCyPhy.ResumeLayout(false);
            this.imputate.ResumeLayout(false);
            this.encoding.ResumeLayout(false);
            this.plotAndSave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ProcessTab;
        private System.Windows.Forms.TabPage fromPcap;
        private System.Windows.Forms.TabPage fromPyShark;
        private System.Windows.Forms.TabPage fromPacketBeat;
        private System.Windows.Forms.TabPage fromSnort;
        private System.Windows.Forms.TabPage phyFromPcap;
        private System.Windows.Forms.TabPage mergeCyPhy;
        private System.Windows.Forms.TabPage imputate;
        private System.Windows.Forms.TabPage encoding;
        private System.Windows.Forms.TabPage plotAndSave;
        private System.Windows.Forms.Button cyRawButton;
        private System.Windows.Forms.Button pySharkButton;
        private System.Windows.Forms.Button packetBeatButton;
        private System.Windows.Forms.Button snortButton;
        private System.Windows.Forms.Button phyRawButton;
        private System.Windows.Forms.Button mergeButton;
        private System.Windows.Forms.Button imputationButton;
        private System.Windows.Forms.Button encodingButton;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.DataGridView cyRawDgv;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot2;
        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.ListBox featureListBox;
        private ScottPlot.FormsPlot formsPlot4;
    }
}