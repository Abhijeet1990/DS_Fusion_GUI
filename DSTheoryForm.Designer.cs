namespace DataFusionApp
{
    partial class DSTheoryForm
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
            this.svm_fp = new ScottPlot.FormsPlot();
            this.knn_fp = new ScottPlot.FormsPlot();
            this.gnb_fp = new ScottPlot.FormsPlot();
            this.bnb_fp = new ScottPlot.FormsPlot();
            this.mlp_fp = new ScottPlot.FormsPlot();
            this.rf_fp = new ScottPlot.FormsPlot();
            this.dt_fp = new ScottPlot.FormsPlot();
            this.comparison_fp = new ScottPlot.FormsPlot();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.fusionTypeCB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.domainTypeCB = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.timeResCB = new System.Windows.Forms.ComboBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.mergeLocDomainCB = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.metricTypeCB = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.selectClfCB = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // svm_fp
            // 
            this.svm_fp.Location = new System.Drawing.Point(12, 67);
            this.svm_fp.Name = "svm_fp";
            this.svm_fp.Size = new System.Drawing.Size(276, 231);
            this.svm_fp.TabIndex = 0;
            // 
            // knn_fp
            // 
            this.knn_fp.Location = new System.Drawing.Point(294, 67);
            this.knn_fp.Name = "knn_fp";
            this.knn_fp.Size = new System.Drawing.Size(277, 231);
            this.knn_fp.TabIndex = 1;
            // 
            // gnb_fp
            // 
            this.gnb_fp.Location = new System.Drawing.Point(577, 67);
            this.gnb_fp.Name = "gnb_fp";
            this.gnb_fp.Size = new System.Drawing.Size(277, 231);
            this.gnb_fp.TabIndex = 2;
            // 
            // bnb_fp
            // 
            this.bnb_fp.Location = new System.Drawing.Point(860, 67);
            this.bnb_fp.Name = "bnb_fp";
            this.bnb_fp.Size = new System.Drawing.Size(305, 231);
            this.bnb_fp.TabIndex = 3;
            // 
            // mlp_fp
            // 
            this.mlp_fp.Location = new System.Drawing.Point(12, 338);
            this.mlp_fp.Name = "mlp_fp";
            this.mlp_fp.Size = new System.Drawing.Size(277, 241);
            this.mlp_fp.TabIndex = 4;
            // 
            // rf_fp
            // 
            this.rf_fp.Location = new System.Drawing.Point(294, 338);
            this.rf_fp.Name = "rf_fp";
            this.rf_fp.Size = new System.Drawing.Size(277, 241);
            this.rf_fp.TabIndex = 5;
            // 
            // dt_fp
            // 
            this.dt_fp.Location = new System.Drawing.Point(577, 338);
            this.dt_fp.Name = "dt_fp";
            this.dt_fp.Size = new System.Drawing.Size(277, 241);
            this.dt_fp.TabIndex = 6;
            // 
            // comparison_fp
            // 
            this.comparison_fp.Location = new System.Drawing.Point(860, 338);
            this.comparison_fp.Name = "comparison_fp";
            this.comparison_fp.Size = new System.Drawing.Size(305, 241);
            this.comparison_fp.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Support Vector Machine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(394, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "k-Nearest Neighbor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(650, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Gaussian Naive Bayes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(974, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bernoulli Naive Bayes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(72, 582);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Multi Layer Perceptron";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(394, 582);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Random Forest";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(686, 582);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Decision Tree";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(960, 582);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Classifier Comparison";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectClfCB);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.metricTypeCB);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.mergeLocDomainCB);
            this.groupBox1.Controls.Add(this.updateButton);
            this.groupBox1.Controls.Add(this.timeResCB);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.domainTypeCB);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.fusionTypeCB);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1153, 58);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DS Configuration";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Fusion Type";
            // 
            // fusionTypeCB
            // 
            this.fusionTypeCB.FormattingEnabled = true;
            this.fusionTypeCB.Items.AddRange(new object[] {
            "Disjunctive",
            "Conjunctive",
            "Combine-Cautious "});
            this.fusionTypeCB.Location = new System.Drawing.Point(72, 21);
            this.fusionTypeCB.Name = "fusionTypeCB";
            this.fusionTypeCB.Size = new System.Drawing.Size(103, 21);
            this.fusionTypeCB.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Source Domain";
            // 
            // domainTypeCB
            // 
            this.domainTypeCB.FormattingEnabled = true;
            this.domainTypeCB.Items.AddRange(new object[] {
            "Pure-Cyber",
            "Pure-Physical",
            "Cyber-Physical"});
            this.domainTypeCB.Location = new System.Drawing.Point(267, 20);
            this.domainTypeCB.Name = "domainTypeCB";
            this.domainTypeCB.Size = new System.Drawing.Size(99, 21);
            this.domainTypeCB.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(372, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Time Res. (in sec)";
            // 
            // timeResCB
            // 
            this.timeResCB.FormattingEnabled = true;
            this.timeResCB.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20"});
            this.timeResCB.Location = new System.Drawing.Point(470, 22);
            this.timeResCB.Name = "timeResCB";
            this.timeResCB.Size = new System.Drawing.Size(44, 21);
            this.timeResCB.TabIndex = 5;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(1059, 22);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(88, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // mergeLocDomainCB
            // 
            this.mergeLocDomainCB.AutoSize = true;
            this.mergeLocDomainCB.Location = new System.Drawing.Point(873, 24);
            this.mergeLocDomainCB.Name = "mergeLocDomainCB";
            this.mergeLocDomainCB.Size = new System.Drawing.Size(180, 17);
            this.mergeLocDomainCB.TabIndex = 7;
            this.mergeLocDomainCB.Text = "Merge by Location and Domain?";
            this.mergeLocDomainCB.UseVisualStyleBackColor = true;
            this.mergeLocDomainCB.CheckedChanged += new System.EventHandler(this.mergeLocDomainCB_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(520, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Metrics";
            // 
            // metricTypeCB
            // 
            this.metricTypeCB.FormattingEnabled = true;
            this.metricTypeCB.Items.AddRange(new object[] {
            "Decision Criteria",
            "Evaluation Metric"});
            this.metricTypeCB.Location = new System.Drawing.Point(565, 22);
            this.metricTypeCB.Name = "metricTypeCB";
            this.metricTypeCB.Size = new System.Drawing.Size(121, 21);
            this.metricTypeCB.TabIndex = 9;
            this.metricTypeCB.SelectedIndexChanged += new System.EventHandler(this.metricTypeCB_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(692, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Classifier";
            this.label13.Visible = false;
            // 
            // selectClfCB
            // 
            this.selectClfCB.FormattingEnabled = true;
            this.selectClfCB.Items.AddRange(new object[] {
            "SVC",
            "KNN",
            "GNB",
            "BNB",
            "MLP",
            "RF",
            "DT"});
            this.selectClfCB.Location = new System.Drawing.Point(746, 20);
            this.selectClfCB.Name = "selectClfCB";
            this.selectClfCB.Size = new System.Drawing.Size(95, 21);
            this.selectClfCB.TabIndex = 11;
            this.selectClfCB.Visible = false;
            // 
            // DSTheoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 604);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comparison_fp);
            this.Controls.Add(this.dt_fp);
            this.Controls.Add(this.rf_fp);
            this.Controls.Add(this.mlp_fp);
            this.Controls.Add(this.bnb_fp);
            this.Controls.Add(this.gnb_fp);
            this.Controls.Add(this.knn_fp);
            this.Controls.Add(this.svm_fp);
            this.Name = "DSTheoryForm";
            this.Text = "Dempster Shafer Theory --- Fusion by Location";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot svm_fp;
        private ScottPlot.FormsPlot knn_fp;
        private ScottPlot.FormsPlot gnb_fp;
        private ScottPlot.FormsPlot bnb_fp;
        private ScottPlot.FormsPlot mlp_fp;
        private ScottPlot.FormsPlot rf_fp;
        private ScottPlot.FormsPlot dt_fp;
        private ScottPlot.FormsPlot comparison_fp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox timeResCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox domainTypeCB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox fusionTypeCB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.CheckBox mergeLocDomainCB;
        private System.Windows.Forms.ComboBox selectClfCB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox metricTypeCB;
        private System.Windows.Forms.Label label12;
    }
}