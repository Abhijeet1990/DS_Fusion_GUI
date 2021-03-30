using Bunifu.UI.WinForms;
using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFusionApp
{
    public partial class SequenceForm : Form
    {
        public string useCase = "";
        public List<string> columnNames = new List<string>();
        public BunifuDataGridView cyberRawDgv;
        public BunifuDataGridView pySharkDgv;
        public BunifuDataGridView packetBeatDgv;
        public BunifuDataGridView snortDgv;
        public BunifuDataGridView phyRawDgv;
        public BunifuDataGridView mergeDgv;
        public BunifuDataGridView imputationDgv;
        public BunifuDataGridView encodingDgv;

        public SequenceForm(string _usecase)
        {
            InitializeComponent();
            this.Text += " " + _usecase;
            useCase = _usecase;
            cyberRawDgv = new BunifuDataGridView();
            InitializeDGVDesign(cyberRawDgv, fromPcap);
            pySharkDgv = new BunifuDataGridView();
            InitializeDGVDesign(pySharkDgv, fromPyShark);
            packetBeatDgv = new BunifuDataGridView();
            InitializeDGVDesign(packetBeatDgv, fromPacketBeat);
            snortDgv = new BunifuDataGridView();
            InitializeDGVDesign(snortDgv, fromSnort);
            phyRawDgv = new BunifuDataGridView();
            InitializeDGVDesign(phyRawDgv, phyFromPcap);
            mergeDgv = new BunifuDataGridView();
            InitializeDGVDesign(mergeDgv, mergeCyPhy);
            imputationDgv = new BunifuDataGridView();
            InitializeDGVDesign(imputationDgv, imputate);
            encodingDgv = new BunifuDataGridView();
            InitializeDGVDesign(encodingDgv, encoding);

            columnNames = new List<string> { "TimeStamp","Frame Len","Frame Type", "Source MAC","Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length" ,"TCP Flag","Next Seq", "Ack Number"};
            ProcessTab.SelectedTab = fromPcap;
            populateDGVs(ref cyberRawDgv, this.useCase, "1",columnNames);

        }

        public void InitializeDGVDesign(BunifuDataGridView bdgv, TabPage tp)
        {
            tp.Controls.Add(bdgv);
            bdgv.Theme = BunifuDataGridView.PresetThemes.DarkViolet;
            bdgv.Dock = DockStyle.Fill;
            bdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void cyRawButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = fromPyShark;
            columnNames = new List<string> { "TimeStamp", "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number","Retrans", "RTT" };
            populateDGVs(ref pySharkDgv, this.useCase, "2",columnNames);

        }

        private void pySharkButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = fromPacketBeat;
            columnNames = new List<string> { "TimeStamp", "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number", "Retrans","RTT","Flow Count","Flow Final Count","Packets"};
            populateDGVs(ref packetBeatDgv, this.useCase, "3",columnNames);

        }

        private void packetBeatButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = fromSnort;
            columnNames = new List<string> { "TimeStamp", "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets", "Alert","Alert Type" };
            populateDGVs(ref snortDgv, this.useCase, "4",columnNames);

        }

        private void snortButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = phyFromPcap;
            columnNames = new List<string> { "TimeStamp", "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload"};
            populateDGVs(ref phyRawDgv, this.useCase, "5",columnNames);


        }

        private void phyRawButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = mergeCyPhy;
            columnNames = new List<string> { "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets", "Alert", "Alert Type", "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload" };
            populateDGVs(ref mergeDgv, this.useCase, "6", columnNames);

        }

        private void mergeButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = imputate;
            columnNames = new List<string> { "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets", "Alert", "Alert Type", "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload" };
            populateDGVs(ref imputationDgv, this.useCase, "7",columnNames);
            
        }

        private void imputationButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = encoding;
            columnNames = new List<string> { "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Next Seq", "Ack Number", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets", "Alert", "Alert Type", "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload" };
            populateDGVs(ref encodingDgv, this.useCase, "8", columnNames);

        }

        private void populateDGVs(ref BunifuDataGridView currentDGV, string usecase, string stage, List<string> columnNames)
        {
            currentDGV.Rows.Clear();
            currentDGV.Columns.Clear();



            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\substationc\AppData\Local\Programs\Python\Python37-32\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\substationc\Desktop\DataFusion\DataFusionApp\DataFusionApp\PythonScripts\GetStepData.py";
            //var usecase = this.useCase;
            //var stage = "8";

            psi.Arguments = $"\"{script}\" \"{usecase}\" \"{stage}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            if (!File.Exists("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\msgpack_" + usecase + "_" + stage + ".mp"))
            {
                using (var process = Process.Start(psi))
                {
                    errors = process.StandardError.ReadToEnd();
                    results = process.StandardOutput.ReadToEnd();
                }

            }

            var serializer = SerializationContext.Default.GetSerializer<MessagePackObject[][]>();
            var unpackedObject = serializer.Unpack(File.OpenRead("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\msgpack_" + usecase + "_" + stage + ".mp"));
            int ctr = 0;

            foreach (var features in unpackedObject)
            {
                var index = Array.IndexOf(unpackedObject, features);

                if (ctr == 0)
                {
                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        currentDGV.Columns.Add(columnNames[i], columnNames[i]);

                    }

                    for (int j = columnNames.Count; j < features.Length; j++)
                    {
                        currentDGV.Columns.Add("Meas "+(j+1-columnNames.Count).ToString(), "Meas " + (j + 1 - columnNames.Count).ToString());
                    }
                }
                ctr += 1;
                currentDGV.Rows.Add();
                foreach (var feature in features)
                {
                    var feature_index = Array.IndexOf(features, feature);
                    if (feature_index < 0) continue;
                    currentDGV.Rows[index].Cells[feature_index].Value = feature;
                }
            }
        }

        private void encodingButton_Click(object sender, EventArgs e)
        {
            ProcessTab.SelectedTab = plotAndSave;
            foreach (DataGridViewTextBoxColumn item in encodingDgv.Columns)
            {
                featureListBox.Items.Add(item.Name);
            }
        }

        private void PlotButton_Click(object sender, EventArgs e)
        {
            ClearPlots();
            int counter = 0;List<string> columnNames = new List<string>();
            foreach (DataGridViewTextBoxColumn item in encodingDgv.Columns) columnNames.Add(item.Name);
           
            foreach (var item in featureListBox.SelectedItems)
            {
                if (counter<4)
                {
                    if (columnNames.Contains(item.ToString()))
                    {
                        int ctr = 1;
                        List<double> xvalue = new List<double>();
                        List<double> yvalue = new List<double>();
                        foreach (DataGridViewRow row in encodingDgv.Rows)
                        {
                            xvalue.Add(Convert.ToDouble(ctr));
                            ctr += 1;
                            if (row.Cells[item.ToString()].Value != null) yvalue.Add(Convert.ToDouble(row.Cells[item.ToString()].Value.ToString()));
                            else yvalue.Add(0.0);
                        }
                        if (counter == 0) formsPlot1.plt.PlotScatter(xvalue.ToArray(), yvalue.ToArray());
                        if (counter == 1) formsPlot2.plt.PlotScatter(xvalue.ToArray(), yvalue.ToArray());
                        if (counter == 2) formsPlot3.plt.PlotScatter(xvalue.ToArray(), yvalue.ToArray());
                        if (counter == 3) formsPlot4.plt.PlotScatter(xvalue.ToArray(), yvalue.ToArray());
                    }
                    counter += 1;
                }
            }
            formsPlot1.Render();
            formsPlot2.Render();
            formsPlot3.Render();
            formsPlot4.Render();
        }

        public void ClearPlots()
        {
            formsPlot1.Reset();
            formsPlot2.Reset();
            formsPlot3.Reset();
            formsPlot4.Reset();

        }
    }
}
