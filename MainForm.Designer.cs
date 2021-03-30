namespace DataFusionApp
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("UC1_10OS_30poll");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("UC1_10OS_60poll");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("UseCase1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("UC2_5OS_30poll");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("UC2_5OS_60poll");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("UC2_10OS_30poll");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("UC2_10OS_60poll");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("UseCase2", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("UC3_5OS_30poll");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("UC3_5OS_60poll");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("UC3_10OS_30poll");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("UC3_10OS_60poll");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("UseCase3", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("UC4_5OS_30poll");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("UC4_5OS_60poll");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("UC4_10OS_30poll");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("UC4_10OS_60poll");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("UseCase4", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            this.useCaseTreeView = new System.Windows.Forms.TreeView();
            this.dataViewTabControl = new System.Windows.Forms.TabControl();
            this.cyberTabPage = new System.Windows.Forms.TabPage();
            this.physicalTabPage = new System.Windows.Forms.TabPage();
            this.cpTabPage = new System.Windows.Forms.TabPage();
            this.trainedTabPage = new System.Windows.Forms.TabPage();
            this.probTabPage = new System.Windows.Forms.TabPage();
            this.coTrainTabPage = new System.Windows.Forms.TabPage();
            this.mlTrainingButton = new System.Windows.Forms.Button();
            this.chartButton = new System.Windows.Forms.Button();
            this.coTrainButton = new System.Windows.Forms.Button();
            this.stepsButton = new System.Windows.Forms.Button();
            this.dsFusionButton = new System.Windows.Forms.Button();
            this.dataViewTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // useCaseTreeView
            // 
            this.useCaseTreeView.BackColor = System.Drawing.Color.MistyRose;
            this.useCaseTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useCaseTreeView.Location = new System.Drawing.Point(4, 47);
            this.useCaseTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useCaseTreeView.Name = "useCaseTreeView";
            treeNode1.Name = "Node1";
            treeNode1.Text = "UC1_10OS_30poll";
            treeNode2.Name = "Node2";
            treeNode2.Text = "UC1_10OS_60poll";
            treeNode3.Name = "Node0";
            treeNode3.Text = "UseCase1";
            treeNode4.Name = "Node4";
            treeNode4.Text = "UC2_5OS_30poll";
            treeNode5.Name = "Node5";
            treeNode5.Text = "UC2_5OS_60poll";
            treeNode6.Name = "Node6";
            treeNode6.Text = "UC2_10OS_30poll";
            treeNode7.Name = "Node7";
            treeNode7.Text = "UC2_10OS_60poll";
            treeNode8.Name = "Node3";
            treeNode8.Text = "UseCase2";
            treeNode9.Name = "Node10";
            treeNode9.Text = "UC3_5OS_30poll";
            treeNode10.Name = "Node11";
            treeNode10.Text = "UC3_5OS_60poll";
            treeNode11.Name = "Node12";
            treeNode11.Text = "UC3_10OS_30poll";
            treeNode12.Name = "Node13";
            treeNode12.Text = "UC3_10OS_60poll";
            treeNode13.Name = "Node8";
            treeNode13.Text = "UseCase3";
            treeNode14.Name = "Node14";
            treeNode14.Text = "UC4_5OS_30poll";
            treeNode15.Name = "Node15";
            treeNode15.Text = "UC4_5OS_60poll";
            treeNode16.Name = "Node16";
            treeNode16.Text = "UC4_10OS_30poll";
            treeNode17.Name = "Node17";
            treeNode17.Text = "UC4_10OS_60poll";
            treeNode18.Name = "Node9";
            treeNode18.Text = "UseCase4";
            this.useCaseTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode8,
            treeNode13,
            treeNode18});
            this.useCaseTreeView.Size = new System.Drawing.Size(276, 657);
            this.useCaseTreeView.TabIndex = 0;
            this.useCaseTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.useCaseTreeView_NodeMouseDoubleClick);
            // 
            // dataViewTabControl
            // 
            this.dataViewTabControl.Controls.Add(this.cyberTabPage);
            this.dataViewTabControl.Controls.Add(this.physicalTabPage);
            this.dataViewTabControl.Controls.Add(this.cpTabPage);
            this.dataViewTabControl.Controls.Add(this.trainedTabPage);
            this.dataViewTabControl.Controls.Add(this.probTabPage);
            this.dataViewTabControl.Controls.Add(this.coTrainTabPage);
            this.dataViewTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataViewTabControl.Location = new System.Drawing.Point(289, 47);
            this.dataViewTabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataViewTabControl.Name = "dataViewTabControl";
            this.dataViewTabControl.SelectedIndex = 0;
            this.dataViewTabControl.Size = new System.Drawing.Size(990, 658);
            this.dataViewTabControl.TabIndex = 1;
            // 
            // cyberTabPage
            // 
            this.cyberTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.cyberTabPage.Location = new System.Drawing.Point(4, 24);
            this.cyberTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cyberTabPage.Name = "cyberTabPage";
            this.cyberTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cyberTabPage.Size = new System.Drawing.Size(1159, 630);
            this.cyberTabPage.TabIndex = 0;
            this.cyberTabPage.Text = "Cyber";
            // 
            // physicalTabPage
            // 
            this.physicalTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.physicalTabPage.Location = new System.Drawing.Point(4, 24);
            this.physicalTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.physicalTabPage.Name = "physicalTabPage";
            this.physicalTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.physicalTabPage.Size = new System.Drawing.Size(982, 630);
            this.physicalTabPage.TabIndex = 1;
            this.physicalTabPage.Text = "Physical";
            // 
            // cpTabPage
            // 
            this.cpTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.cpTabPage.Location = new System.Drawing.Point(4, 24);
            this.cpTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cpTabPage.Name = "cpTabPage";
            this.cpTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cpTabPage.Size = new System.Drawing.Size(1159, 630);
            this.cpTabPage.TabIndex = 2;
            this.cpTabPage.Text = "Cyber-Physical";
            // 
            // trainedTabPage
            // 
            this.trainedTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.trainedTabPage.Location = new System.Drawing.Point(4, 24);
            this.trainedTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trainedTabPage.Name = "trainedTabPage";
            this.trainedTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trainedTabPage.Size = new System.Drawing.Size(1159, 630);
            this.trainedTabPage.TabIndex = 3;
            this.trainedTabPage.Text = "Trained Scores";
            // 
            // probTabPage
            // 
            this.probTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.probTabPage.Location = new System.Drawing.Point(4, 24);
            this.probTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.probTabPage.Name = "probTabPage";
            this.probTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.probTabPage.Size = new System.Drawing.Size(1159, 630);
            this.probTabPage.TabIndex = 4;
            this.probTabPage.Text = "Probability Scores";
            // 
            // coTrainTabPage
            // 
            this.coTrainTabPage.BackColor = System.Drawing.Color.LavenderBlush;
            this.coTrainTabPage.Location = new System.Drawing.Point(4, 24);
            this.coTrainTabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.coTrainTabPage.Name = "coTrainTabPage";
            this.coTrainTabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.coTrainTabPage.Size = new System.Drawing.Size(1159, 630);
            this.coTrainTabPage.TabIndex = 5;
            this.coTrainTabPage.Text = "CoTrained Scores";
            // 
            // mlTrainingButton
            // 
            this.mlTrainingButton.BackColor = System.Drawing.Color.MediumVioletRed;
            this.mlTrainingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlTrainingButton.Location = new System.Drawing.Point(621, 6);
            this.mlTrainingButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mlTrainingButton.Name = "mlTrainingButton";
            this.mlTrainingButton.Size = new System.Drawing.Size(159, 35);
            this.mlTrainingButton.TabIndex = 2;
            this.mlTrainingButton.Text = "Supervised Training";
            this.mlTrainingButton.UseVisualStyleBackColor = false;
            this.mlTrainingButton.Click += new System.EventHandler(this.mlTrainingButton_Click);
            // 
            // chartButton
            // 
            this.chartButton.BackColor = System.Drawing.Color.MediumVioletRed;
            this.chartButton.Location = new System.Drawing.Point(788, 7);
            this.chartButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chartButton.Name = "chartButton";
            this.chartButton.Size = new System.Drawing.Size(128, 35);
            this.chartButton.TabIndex = 3;
            this.chartButton.Text = "Charts";
            this.chartButton.UseVisualStyleBackColor = false;
            this.chartButton.Click += new System.EventHandler(this.chartButton_Click);
            // 
            // coTrainButton
            // 
            this.coTrainButton.BackColor = System.Drawing.Color.MediumVioletRed;
            this.coTrainButton.Location = new System.Drawing.Point(924, 7);
            this.coTrainButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.coTrainButton.Name = "coTrainButton";
            this.coTrainButton.Size = new System.Drawing.Size(132, 35);
            this.coTrainButton.TabIndex = 4;
            this.coTrainButton.Text = "Co-Training";
            this.coTrainButton.UseVisualStyleBackColor = false;
            this.coTrainButton.Click += new System.EventHandler(this.coTrainButton_Click);
            // 
            // stepsButton
            // 
            this.stepsButton.BackColor = System.Drawing.Color.MediumVioletRed;
            this.stepsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepsButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stepsButton.Location = new System.Drawing.Point(492, 7);
            this.stepsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stepsButton.Name = "stepsButton";
            this.stepsButton.Size = new System.Drawing.Size(121, 35);
            this.stepsButton.TabIndex = 5;
            this.stepsButton.Text = "Steps";
            this.stepsButton.UseVisualStyleBackColor = false;
            this.stepsButton.Click += new System.EventHandler(this.stepsButton_Click);
            // 
            // dsFusionButton
            // 
            this.dsFusionButton.BackColor = System.Drawing.Color.MediumVioletRed;
            this.dsFusionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dsFusionButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dsFusionButton.Location = new System.Drawing.Point(1064, 7);
            this.dsFusionButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dsFusionButton.Name = "dsFusionButton";
            this.dsFusionButton.Size = new System.Drawing.Size(215, 35);
            this.dsFusionButton.TabIndex = 6;
            this.dsFusionButton.Text = "Dempster Shafer ";
            this.dsFusionButton.UseVisualStyleBackColor = false;
            this.dsFusionButton.Click += new System.EventHandler(this.DsFusionButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1284, 719);
            this.Controls.Add(this.dsFusionButton);
            this.Controls.Add(this.stepsButton);
            this.Controls.Add(this.coTrainButton);
            this.Controls.Add(this.chartButton);
            this.Controls.Add(this.mlTrainingButton);
            this.Controls.Add(this.dataViewTabControl);
            this.Controls.Add(this.useCaseTreeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Data Fusion App";
            this.dataViewTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView useCaseTreeView;
        private System.Windows.Forms.TabControl dataViewTabControl;
        private System.Windows.Forms.TabPage cyberTabPage;
        private System.Windows.Forms.TabPage physicalTabPage;
        private System.Windows.Forms.TabPage cpTabPage;
        private System.Windows.Forms.Button mlTrainingButton;
        private System.Windows.Forms.TabPage trainedTabPage;
        private System.Windows.Forms.TabPage probTabPage;
        private System.Windows.Forms.Button chartButton;
        private System.Windows.Forms.TabPage coTrainTabPage;
        private System.Windows.Forms.Button coTrainButton;
        private System.Windows.Forms.Button stepsButton;
        private System.Windows.Forms.Button dsFusionButton;
    }
}

