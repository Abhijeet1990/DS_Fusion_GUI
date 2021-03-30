using Bunifu.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataFusionApp
{
    public partial class ChartForm : Form
    {
        BunifuDataGridView dgvForChart = new BunifuDataGridView();
        public ChartForm(BunifuDataGridView dgv)
        {
            InitializeComponent();
            dgvForChart = dgv;
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // Populate charts and plot the values stored in dgvForChart
            var columnCount = dgvForChart.Columns.Count;
            var rowCount = dgvForChart.Rows.Count;
            for (int i = 0; i < columnCount; i++)
            {
                Chart chrt = new Chart();
                chrt.Height = 150;
                chrt.Width = 180;
                chrt.Series.Add(dgvForChart.Columns[i].HeaderText);
                chrt.Series[dgvForChart.Columns[i].HeaderText].SetDefault(true);
                chrt.Series[dgvForChart.Columns[i].HeaderText].Enabled = true;
                chrt.Series[dgvForChart.Columns[i].HeaderText].ChartType = SeriesChartType.Spline;
                chrt.Visible = true;
                ChartArea chA = new ChartArea();
                chrt.ChartAreas.Add(chA);

                for (int j = 0; j < rowCount; j++)
                {
                    double score = 0.0;
                    //var value = dgvForChart[j, i].Value.ToString();
                    if (dgvForChart[i, j].Value != null) score = Convert.ToDouble(dgvForChart[i, j].Value.ToString());
                    chrt.Series[dgvForChart.Columns[i].HeaderText].Points.AddXY(Convert.ToDouble(j), score);
                }
                if (i<4) chrt.Location = new Point(200*i, 50);
                else chrt.Location = new Point(200 * (i-4), 250);
                Refresh();
                Label lbl = new Label();
                lbl.Text = dgvForChart.Columns[i].HeaderText;
                if (i < 4) lbl.Location = new Point(200 * i, 35);
                else lbl.Location = new Point(200 * (i - 4), 235);
                this.Controls.Add(chrt);
                this.Controls.Add(lbl);
                chrt.Show();
            }
        }
    }
}
