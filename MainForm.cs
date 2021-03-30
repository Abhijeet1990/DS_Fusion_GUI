using IronPython.Hosting;
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
using Bunifu.UI.WinForms;

namespace DataFusionApp
{
    public partial class MainForm : Form
    {
        public BunifuDataGridView bdgvCyber;
        public BunifuDataGridView bdgvPhysical;
        public BunifuDataGridView bdgvCyberPhysical;
        public BunifuDataGridView bdgvTrained;
        public BunifuDataGridView bdgvPscores;
        public BunifuDataGridView bdgvCoTrained;
        public MainForm()
        {
            InitializeComponent();
            bdgvCyber = new BunifuDataGridView();
            InitializeDGVDesign(bdgvCyber, cyberTabPage);
            bdgvPhysical = new BunifuDataGridView();
            InitializeDGVDesign(bdgvPhysical, physicalTabPage);
            bdgvCyberPhysical = new BunifuDataGridView();
            InitializeDGVDesign(bdgvCyberPhysical, cpTabPage);
            bdgvTrained = new BunifuDataGridView();
            InitializeDGVDesign(bdgvTrained, trainedTabPage);
            bdgvPscores = new BunifuDataGridView();
            InitializeDGVDesign(bdgvPscores,probTabPage);
            bdgvCoTrained = new BunifuDataGridView();
            InitializeDGVDesign(bdgvCoTrained, coTrainTabPage);

        }

        public void InitializeDGVDesign(BunifuDataGridView bdgv, TabPage tp)
        {
            tp.Controls.Add(bdgv);
            bdgv.Theme = BunifuDataGridView.PresetThemes.DarkViolet;
            bdgv.Dock = DockStyle.Fill;
            bdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void useCaseTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if(e.Node.Text == "UC1_10OS_30poll")
            //{
            //    GetDataPythonType2(e.Node.Text);
            //}

            GetDataPython(e.Node.Text);
        }

        private void GetDataPython(string useCase)
        {
            if (!useCase.Contains("poll")) return;
            //cyberDataGridView.Rows.Clear();
            //cyberDataGridView.Columns.Clear();
            bdgvCyber.Rows.Clear();
            bdgvCyber.Columns.Clear();
            

            bdgvPhysical.Rows.Clear();
            bdgvPhysical.Columns.Clear();

            bdgvCyberPhysical.Rows.Clear();
            bdgvCyberPhysical.Columns.Clear();


            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\substationc\AppData\Local\Programs\Python\Python37-32\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\substationc\Desktop\DataFusion\DataFusionApp\DataFusionApp\PythonScripts\GetData.py";
            var usecase = useCase;

            psi.Arguments = $"\"{script}\" \"{usecase}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            var cyberColumnNames = new List<string> { "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets","alert","alert_type"};
            var phyColumnNames = new List<string> { "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload","Meas 1","Meas 2","Meas 3","Meas 4","Meas 5", "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload","Meas 1","Meas 2","Meas 3","Meas 4","Meas 5" };
            var cyphyColNames = new List<string> { "Frame Len", "Frame Type", "Source MAC", "Dest MAC", "Src IP", "Dest IP", "IP Length", "IP Flag", "Src Port", "Dest Port", "TCP Length", "TCP Flag", "Retrans", "RTT", "Flow Count", "Flow Final Count", "Packets", "alert","alert_type",  "DNP3 Src", "DNP3 Dest", "LL Len", "LL Control", "TL Control", "Func Code", "AL Control", "DNP3 obj", "Obj Count", "Raw Objects", "AL Payload", "Meas 1", "Meas 2", "Meas 3", "Meas 4", "Meas 5","Meas 6" };
        var serializer = SerializationContext.Default.GetSerializer<MessagePackObject[][]>();
        var unpackedObject = serializer.Unpack(File.OpenRead("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\msgpack_" + useCase+".mp"));
            int ctr = 0;

            foreach (var features in unpackedObject)
            {
                var index = Array.IndexOf(unpackedObject, features);
                
                if(ctr == 0)
                {
                    for (int i = 0; i < features.Length; i++)
                    {
                        bdgvCyberPhysical.Columns.Add(cyphyColNames[i],cyphyColNames[i]);
                        if (i <19)bdgvCyber.Columns.Add(cyberColumnNames[i], cyberColumnNames[i]);
                        else if ((i-19) > 0)bdgvPhysical.Columns.Add(phyColumnNames[i-19], phyColumnNames[i-19]);
                    }

                }
                ctr += 1;
                bdgvCyber.Rows.Add();
                bdgvPhysical.Rows.Add();
                bdgvCyberPhysical.Rows.Add();
                foreach (var feature in features)
                {
                    var feature_index = Array.IndexOf(features, feature);
                    bdgvCyberPhysical.Rows[index].Cells[feature_index].Value = feature;
                    if (feature_index < 19) bdgvCyber.Rows[index].Cells[feature_index].Value = feature;
                    else bdgvPhysical.Rows[index].Cells[feature_index-19].Value = feature;
                }
            }
        }

        private void GetDataPython_Unused(string useCase)
        {
            // create engine
            var engine = Python.CreateEngine();
            //var searchPaths = engine.GetSearchPaths();
            //searchPaths.Add(@"C:\Users\abhij\Anaconda3\Lib");


            var script = @"D:\DataFusionWindowsApp\DataFusionApp\DataFusionApp\PythonScripts\GetData.py";
            var source = engine.CreateScriptSourceFromFile(script);

            var argv = new List<string>();
            argv.Add("");
            argv.Add(useCase);

            engine.GetSysModule().SetVariable("argv", argv);

            // 3) Output redirect
            var eIO = engine.Runtime.IO;

            var errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            var results = new MemoryStream();
            eIO.SetOutput(results, Encoding.Default);

            // 4) Execute script
            var scope = engine.CreateScope();
            source.Execute(scope);

            // 5) Display output
            string str(byte[] x) => Encoding.Default.GetString(x);


            var error_op = str(errors.ToArray());
            var results_op = str(results.ToArray());


            // Run the python script to get the serialized output
            var serializer = SerializationContext.Default.GetSerializer<MessagePackObject[][]>();
            var unpackedObject = serializer.Unpack(File.OpenRead("d:\\msgpack1.mp"));


        }

        private void GetTrainedScores(string useCase, bool pureCyber, bool purePhy, bool enablePCA)
        {

            bdgvTrained.Rows.Clear();
            bdgvTrained.Columns.Clear();

            bdgvPscores.Rows.Clear();
            bdgvPscores.Columns.Clear();

            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\substationc\AppData\Local\Programs\Python\Python37-32\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\substationc\Desktop\DataFusion\DataFusionApp\DataFusionApp\PythonScripts\GetTrainedScores.py";
            var usecase = useCase;

            string pc = "False";
            string pp = "False";
            string enablepca = "False";
            if (pureCyber == true) pc = "True";
            else pc = "False";
            if (purePhy == true) pp = "True";
            else pp = "False";
            if (enablePCA == true) pp = "True";
            else enablepca = "False";
            psi.Arguments = $"\"{script}\" \"{usecase}\" \"{enablepca}\" \"{pc}\" \"{pp}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            var serializer = SerializationContext.Default.GetSerializer<MessagePackObject[][]>();
            var unpackedObject = serializer.Unpack(File.OpenRead("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\pscores_" + useCase + ".mp"));
            var unpackedScoreObject = serializer.Unpack(File.OpenRead("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\prob_" + useCase + ".mp"));
            int ctr = 0, ctr2=0;

            List<string> classifiers = new List<string> { "k-Nearest Neighbor", "Support Vector", "Decision Tree", "Random Forest", "Gaussian NB", "Bernoulii NB", "MLP" };

            foreach (var features in unpackedObject)
            {
                var index = Array.IndexOf(unpackedObject, features);

                if (ctr == 0)
                {
                    bdgvTrained.Columns.Add("Score Type", "Score Type");
                    for (int i = 0; i < features.Length; i++)
                    {
                        bdgvTrained.Columns.Add(classifiers[i],classifiers[i]);
                    }

                }
                ctr += 1;
                bdgvTrained.Rows.Add();
                foreach (var feature in features)
                {
                    var feature_index = Array.IndexOf(features, feature);
                    bdgvTrained.Rows[index].Cells[feature_index+1].Value = feature;
                    
                }
                if (index ==0) bdgvTrained.Rows[index].Cells[0].Value = "Precision";
                if (index==1) bdgvTrained.Rows[index].Cells[0].Value = "Recall";
                if (index == 2) bdgvTrained.Rows[index].Cells[0].Value = "F1-Score";
            }


            foreach (var clfrs in unpackedScoreObject)
            {
                var index = Array.IndexOf(unpackedScoreObject, clfrs);

                if (ctr2 == 0)
                {
                    for (int i = 0; i < clfrs.Length; i++)
                    {
                        bdgvPscores.Columns.Add(classifiers[i], classifiers[i]);
                    }

                }
                ctr2 += 1;
                bdgvPscores.Rows.Add();
                foreach (var feature in clfrs)
                {
                    var clf_index = Array.IndexOf(clfrs, feature);
                    bdgvPscores.Rows[index].Cells[clf_index].Value = feature;

                }
            }
        }

        private void GetCoTrainedScores(string useCase, bool pureCyber, bool purePhy, bool enablePCA)
        {

            bdgvCoTrained.Rows.Clear();
            bdgvCoTrained.Columns.Clear();

            //pscoresGridView.Rows.Clear();
            //pscoresGridView.Columns.Clear();

            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\substationc\AppData\Local\Programs\Python\Python37-32\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\substationc\Desktop\DataFusion\DataFusionApp\DataFusionApp\PythonScripts\CoTrainedScores.py";
            var usecase = useCase;

            string pc = "False";
            string pp = "False";
            string enablepca = "False";
            if (pureCyber == true) pc = "True";
            else pc = "False";
            if (purePhy == true) pp = "True";
            else pp = "False";
            if (enablePCA == true) pp = "True";
            else enablepca = "False";
            psi.Arguments = $"\"{script}\" \"{usecase}\" \"{enablepca}\" \"{pc}\" \"{pp}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            var serializer = SerializationContext.Default.GetSerializer<MessagePackObject[][]>();
            var unpackedObject = serializer.Unpack(File.OpenRead("C:\\Users\\substationc\\Desktop\\DataFusion\\DataFusionApp\\DataFusionApp\\bin\\Debug\\cotrain_pscores_" + useCase + ".mp"));
            //var unpackedScoreObject = serializer.Unpack(File.OpenRead("D:\\DataFusionWindowsApp\\DataFusionApp\\DataFusionApp\\bin\\Debug\\prob_" + useCase + ".mp"));
            int ctr = 0, ctr2 = 0;

            List<string> classifiers = new List<string> { "Support Vector", "Decision Tree", "Random Forest", "Gaussian NB", "Bernoulii NB", "MLP" };

            foreach (var features in unpackedObject)
            {
                var index = Array.IndexOf(unpackedObject, features);

                if (ctr == 0)
                {
                    bdgvCoTrained.Columns.Add("Score Type", "Score Type");
                    for (int i = 0; i < features.Length; i++)
                    {
                        bdgvCoTrained.Columns.Add(classifiers[i], classifiers[i]);
                    }

                }
                ctr += 1;
                bdgvCoTrained.Rows.Add();
                foreach (var feature in features)
                {
                    var feature_index = Array.IndexOf(features, feature);
                    bdgvCoTrained.Rows[index].Cells[feature_index + 1].Value = feature;

                }
                if (index == 0) bdgvCoTrained.Rows[index].Cells[0].Value = "Precision";
                if (index == 1) bdgvCoTrained.Rows[index].Cells[0].Value = "Recall";
                if (index == 2) bdgvCoTrained.Rows[index].Cells[0].Value = "F1-Score";
            }


            //foreach (var clfrs in unpackedScoreObject)
            //{
            //    var index = Array.IndexOf(unpackedScoreObject, clfrs);

            //    if (ctr2 == 0)
            //    {
            //        for (int i = 0; i < clfrs.Length; i++)
            //        {
            //            pscoresGridView.Columns.Add(classifiers[i], classifiers[i]);
            //        }

            //    }
            //    ctr2 += 1;
            //    pscoresGridView.Rows.Add();
            //    foreach (var feature in clfrs)
            //    {
            //        var clf_index = Array.IndexOf(clfrs, feature);
            //        pscoresGridView.Rows[index].Cells[clf_index].Value = feature;

            //    }
            //}

            dataViewTabControl.SelectedTab = coTrainTabPage;
        }

        private void mlTrainingButton_Click(object sender, EventArgs e)
        {
            bool pc = true;
            bool pp = true;
            bool cp = true;
            if (dataViewTabControl.SelectedTab.Text=="Cyber")
            {
                pc = true;
                pp = false;
                GetTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
            else if (dataViewTabControl.SelectedTab.Text == "Physical")
            {
                pc = false;
                pp = true;
                GetTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
            else if (dataViewTabControl.SelectedTab.Text == "Cyber-Physical")
            {
                pc = false;
                pp = false;
                GetTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
        }

        private void chartButton_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm(bdgvPscores);
            cf.Show();
        }

        private void coTrainButton_Click(object sender, EventArgs e)
        {
            bool pc = true;
            bool pp = true;
            bool cp = true;
            if (dataViewTabControl.SelectedTab.Text == "Cyber")
            {
                pc = true;
                pp = false;
                GetCoTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
            else if (dataViewTabControl.SelectedTab.Text == "Physical")
            {
                pc = false;
                pp = true;
                GetCoTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
            else if (dataViewTabControl.SelectedTab.Text == "Cyber-Physical")
            {
                pc = false;
                pp = false;
                GetCoTrainedScores(useCaseTreeView.SelectedNode.Text, pc, pp, false);
            }
        }

        private void stepsButton_Click(object sender, EventArgs e)
        {
            var usecase = useCaseTreeView.SelectedNode.Text;
            SequenceForm sf = new SequenceForm(usecase);
            sf.Show();
        }

        private void DsFusionButton_Click(object sender, EventArgs e)
        {
            DSTheoryForm dstform = new DSTheoryForm(useCaseTreeView.SelectedNode.Text);
            dstform.Show();
        }
    }
}
