using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DataFusionApp.DSTheory;

namespace DataFusionApp
{
    public partial class DSTheoryForm : Form
    {
        string scenario = "";
        string filePathGlobal = @"C:\Users\substationc\Desktop\DataFusion\DataFusionApp\DataFusionApp\PythonScripts\csvs\Data_for_DS\";
        public DSTheoryForm(string _scenario)
        {
            InitializeComponent();
            this.Text += " Use Case: " + _scenario;
            scenario = _scenario;
            if (_scenario.Contains("10OS"))
            {
                scenario = scenario.Remove(6, 2);
                scenario = scenario.Remove(9, 4);
            }
            else if (_scenario.Contains("5OS"))
            {
                scenario = scenario.Remove(5, 2);
                scenario = scenario.Remove(8, 4);
            }

            //Compute scores
            //ProcessDSScores(false,true,15,"cp");
            ProcessDSScoresIntraDomain(false, true, 15, "cp");
        }

        public void ProcessDSScores(bool combinedCautious, bool isDisjunctive, int res, string type)
        {
            ClearPlots();
            Dictionary<string, double> hyp_val = new Dictionary<string, double> { { "ab", 0.6 }, { "bc", 0.3 }, { "a", 0.1 }, { "ad", 0.0 } };
            label1.Text = "Support Vector Machine";
            label2.Text = "K-Nearest Neighbor";
            label3.Text = "Gaussian Naive Bayes";
            label4.Text = "Bernoulli Naive Bayes";
            label5.Text = "Multi Layer Perceptron";
            label6.Text = "Random Forest";
            label7.Text = "Decision Tree";
            label8.Text = "Comparison";

            MassFunction m = new MassFunction(hyp_val);
            m.ComputeFocalSet();
            m.ComputeFrameOfDiscernment();

            ////compute for 1000 samples
            int sampleSize = 1000;

            List<string> classifiers = new List<string>(new string[] { "svc", "knn", "gnb", "bnb", "mlp", "rf", "dt" });
            
            //string type = "cp";

            Dictionary<string, List<double>> scores = new Dictionary<string, List<double>>();
            foreach (var classifier in classifiers)
            {
                string filePath_pc = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_pc.csv";
                var sensor1_mf_list = ConstructMassFunction(filePath_pc, "ds");
                var sensor2_mf_list = ConstructMassFunction(filePath_pc, "master");
                var sensor3_mf_list = ConstructMassFunction(filePath_pc, "router");

                string filePath_pp = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_pp.csv";
                var sensor4_mf_list = ConstructMassFunction(filePath_pp, "ds");
                var sensor5_mf_list = ConstructMassFunction(filePath_pp, "master");
                var sensor6_mf_list = ConstructMassFunction(filePath_pp, "router");

                sampleSize = sensor1_mf_list.Count;

                int tp_c1 = 0, tn_c1 = 0, fp_c1 = 0, fn_c1 = 0;
                double pre_c1 = 0.0, re_c1 = 0.0, f1_c1 = 0.0, acc_c1 = 0.0;

                int tp_c2 = 0, tn_c2 = 0, fp_c2 = 0, fn_c2 = 0;
                double pre_c2 = 0.0, re_c2 = 0.0, f1_c2 = 0.0, acc_c2 = 0.0;

                int tp_c3 = 0, tn_c3 = 0, fp_c3 = 0, fn_c3 = 0;
                double pre_c3 = 0.0, re_c3 = 0.0, f1_c3 = 0.0, acc_c3 = 0.0;

                int tp_c4 = 0, tn_c4 = 0, fp_c4 = 0, fn_c4 = 0;
                double pre_c4 = 0.0, re_c4 = 0.0, f1_c4 = 0.0, acc_c4 = 0.0;

                if (combinedCautious)
                {
                    sensor1_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "ds");
                    sensor2_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "master");
                    sensor3_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "router");
                    sensor4_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "ds");
                    sensor5_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "master");
                    sensor6_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "router");
                }

                var labels = GetLabel(filePath_pc);

                List<string> ranked_Hyp_ByBelief = new List<string>();
                List<double> ranked_count_ByBelief = new List<double>();
                List<double> ranked_value_ByBelief = new List<double>();

                List<string> ranked_Hyp_ByPlausibility = new List<string>();
                List<double> ranked_count_ByPlausibility = new List<double>();
                List<double> ranked_value_ByPlausibility = new List<double>();

                List<string> rankedPignistic = new List<string>();
                List<double> ranked_count_ByPignistic = new List<double>();
                List<double> ranked_value_ByPignistic = new List<double>();

                // construct mf using Generalized bayesian Theorem and store it here for every sample fused.
                List<Dictionary<string, double>> gbt_sample_fused = new List<Dictionary<string, double>>();
                List<string> ranked_mf_gbt = new List<string>();
                List<double> ranked_count_ByGBT = new List<double>();
                List<double> ranked_value_ByGBT = new List<double>();

                List<double> conflictMeasure = new List<double>();
                List<double> hartleyMeasure = new List<double>();


                for (int i = 0; i < sampleSize; i++)
                {
                    var s1_mf = sensor1_mf_list[i];
                    var s2_mf = sensor2_mf_list[i];
                    var cfused = m.combinedDeterministically(s1_mf, s2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) cfused = m.combineCautious(s1_mf, s2_mf, isDisjunctive);
                    var s3_mf = sensor3_mf_list[i];
                    var cfused3 = m.combinedDeterministically(cfused, s3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) cfused3 = m.combineCautious(cfused, s3_mf, isDisjunctive);

                    var p1_mf = sensor4_mf_list[i];
                    var p2_mf = sensor5_mf_list[i];
                    var pfused = m.combinedDeterministically(p1_mf, p2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) pfused = m.combineCautious(p1_mf, p2_mf, isDisjunctive);
                    var p3_mf = sensor3_mf_list[i];
                    var pfused3 = m.combinedDeterministically(pfused, p3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) pfused3 = m.combineCautious(pfused, p3_mf, isDisjunctive);

                    var fused3 = m.combinedDeterministically(cfused, pfused, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) fused3 = m.combineCautious(cfused, pfused, isDisjunctive);


                    //conflict measure
                    MassFunction t1 = new MassFunction(cfused);
                    MassFunction t2 = new MassFunction(pfused);
                    var conflict = m.conflict(t1, t2, 0);
                    conflictMeasure.Add(conflict);

                    //hartley measure
                    var hartley = m.hartley_measure(fused3);
                    hartleyMeasure.Add(hartley);

                    MassFunction newM = new MassFunction(fused3);
                    newM.ComputeFocalSet();
                    newM.ComputeFrameOfDiscernment();
                    var bel_func = newM.beliefFunction();
                    var plaus_func = newM.plausFunction();

                    // test gbt 
                    var gbt_mf = newM.gbt(plaus_func, false, 0);
                    gbt_sample_fused.Add(gbt_mf);


                    // decision basis
                    var max_hyp_bel = bel_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByBelief.Add(max_hyp_bel);
                    if (max_hyp_bel.Contains('a'))
                    {
                        ranked_value_ByBelief.Add(Math.Round(bel_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c1 += 1;
                        else fp_c1 += 1;
                    }
                    else
                    {
                        ranked_value_ByBelief.Add(0.0);
                        if (labels[i] == 0.0) tn_c1 += 1;
                        else fn_c1 += 1;
                    }
                    if (max_hyp_bel.Length == 1 && max_hyp_bel.Contains('b')) ranked_count_ByBelief.Add(-1);
                    else ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));
                    //ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));

                    var max_hyp_plaus = plaus_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByPlausibility.Add(max_hyp_plaus);
                    if (max_hyp_plaus.Contains('a'))
                    {
                        ranked_value_ByPlausibility.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c2 += 1;
                        else fp_c2 += 1;
                    }
                    else
                    {
                        ranked_value_ByPlausibility.Add(0.0);
                        if (labels[i] == 0.0) tn_c2 += 1;
                        else fn_c2 += 1;
                    }
                    if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPlausibility.Add(-1);
                    else ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    //ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));

                    var pignisticFunction = newM.pignistic();
                    if (pignisticFunction != null && pignisticFunction.Count != 0)
                    {
                        var max_pignistic = pignisticFunction.OrderByDescending(x => x.Value).First().Key;
                        rankedPignistic.Add(max_pignistic);
                        if (max_pignistic.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(pignisticFunction.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_pignistic.Length == 1 && max_pignistic.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                    }
                    else
                    {
                        rankedPignistic.Add(max_hyp_plaus);
                        if (max_hyp_plaus.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    }

                    var max_mf_gbt = gbt_mf.OrderByDescending(x => x.Value).First().Key;
                    ranked_mf_gbt.Add(max_mf_gbt);
                    if (max_mf_gbt.Contains('a'))
                    {
                        ranked_value_ByGBT.Add(Math.Round(gbt_mf.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c4 += 1;
                        else fp_c4 += 1;
                    }
                    else
                    {
                        ranked_value_ByGBT.Add(0.0);
                        if (labels[i] == 0.0) tn_c4 += 1;
                        else fn_c4 += 1;
                    }
                    if (max_mf_gbt.Length == 1 && max_mf_gbt.Contains('b')) ranked_count_ByGBT.Add(-1);
                    else ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                    //ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                }

                //Time
                List<double> realtime = new List<double>();
                for (double i = 0; i < sampleSize; i++) realtime.Add(i + 1);

                //plot Labels
                //var plt_label = new ScottPlot.Plot(800, 400);
                //plt_label.PlotScatter(realtime.ToArray(), labels.ToArray());
                //plt_label.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_labels.png");

                ////plot conflict measure
                //var plt_conflict = new ScottPlot.Plot(800, 400);
                //plt_conflict.PlotScatter(realtime.ToArray(), conflictMeasure.ToArray());
                //plt_conflict.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_conflict_measure.png");

                ////plot hartley measure
                //var plt_hartley = new ScottPlot.Plot(800, 400);
                //plt_hartley.PlotScatter(realtime.ToArray(), hartleyMeasure.ToArray());
                //plt_hartley.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_hartley_measure.png");

                ////plot the decision rules based on the count of strings that were ranked highest in the evidences
                //var plt_rank_belief = new ScottPlot.Plot(800, 400);
                //plt_rank_belief.PlotScatter(realtime.ToArray(), ranked_count_ByBelief.ToArray());
                //plt_rank_belief.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_belief_count.png");

                //var plt_rank_belief_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_belief_scores.PlotScatter(realtime.ToArray(), ranked_value_ByBelief.ToArray());
                //plt_rank_belief_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_belief_scores.png");

                //var plt_rank_plausibility = new ScottPlot.Plot(800, 400);
                //plt_rank_plausibility.PlotScatter(realtime.ToArray(), ranked_count_ByPlausibility.ToArray());
                //plt_rank_plausibility.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_plausibility_count.png");

                //var plt_rank_plausibility_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_plausibility_scores.PlotScatter(realtime.ToArray(), ranked_value_ByPlausibility.ToArray());
                //plt_rank_plausibility_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_plausibility_scores.png");

                //var plt_rank_pignistic = new ScottPlot.Plot(800, 400);
                //plt_rank_pignistic.PlotScatter(realtime.ToArray(), ranked_count_ByPignistic.ToArray());
                //plt_rank_pignistic.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_pignistic_count.png");

                //var plt_rank_pignistic_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_pignistic_scores.PlotScatter(realtime.ToArray(), ranked_value_ByPignistic.ToArray());
                //plt_rank_pignistic_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_pignistic_scores.png");

                //var plt_rank_gbt = new ScottPlot.Plot(800, 400);
                //plt_rank_gbt.PlotScatter(realtime.ToArray(), ranked_count_ByGBT.ToArray());
                //plt_rank_gbt.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_gbt_count.png");

                //var plt_rank_gbt_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_gbt_scores.PlotScatter(realtime.ToArray(), ranked_value_ByGBT.ToArray());
                //plt_rank_gbt_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_gbt_scores.png");

                //var plt_all = new ScottPlot.Plot(600, 400);
                //plt_all.PlotScatter(realtime.ToArray(), ranked_value_ByBelief.ToArray(), label: "Belief");
                //plt_all.PlotScatter(realtime.ToArray(), ranked_value_ByPlausibility.ToArray(), label: "Plausibility");
                //plt_all.PlotScatter(realtime.ToArray(), ranked_value_ByPignistic.ToArray(), label: "Pignistic");
                //plt_all.PlotScatter(realtime.ToArray(), ranked_value_ByGBT.ToArray(), label: "GBT");
                //plt_all.PlotScatter(realtime.ToArray(), labels.ToArray(), label: "Attack Labels");
                //plt_all.Legend();
                //plt_all.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_all_scores.png");

                // compute the recall,precision,f1 and accuracy score and store
                pre_c1 = (double)tp_c1 / (double)(tp_c1 + fp_c1);
                re_c1 = (double)tp_c1 / (double)(tp_c1 + fn_c1);
                f1_c1 = (double)(2 * pre_c1 * re_c1) / (double)(pre_c1 + re_c1);
                acc_c1 = (double)(tp_c1 + tn_c1) / (double)(tp_c1 + tn_c1 + fn_c1 + fp_c1);

                pre_c2 = (double)tp_c2 / (double)(tp_c2 + fp_c2);
                re_c2 = (double)tp_c2 / (double)(tp_c2 + fn_c2);
                f1_c2 = (double)(2 * pre_c2 * re_c2) / (double)(pre_c2 + re_c2);
                acc_c2 = (double)(tp_c2 + tn_c2) / (double)(tp_c2 + tn_c2 + fn_c2 + fp_c2);

                if ((tp_c3 + fp_c3) == 0) pre_c3 = 0.0;
                else pre_c3 = (double)tp_c3 / (double)(tp_c3 + fp_c3);

                re_c3 = (double)tp_c3 / (double)(tp_c3 + fn_c3);

                if ((pre_c3 + re_c3) == 0.0) f1_c3 = 0.0;
                else f1_c3 = (double)(2 * pre_c3 * re_c3) / (double)(pre_c3 + re_c3);

                acc_c3 = (double)(tp_c3 + tn_c3) / (double)(tp_c3 + tn_c3 + fn_c3 + fp_c3);

                pre_c4 = (double)tp_c4 / (double)(tp_c4 + fp_c4);
                re_c4 = (double)tp_c4 / (double)(tp_c4 + fn_c4);
                f1_c4 = (double)(2 * pre_c4 * re_c4) / (double)(pre_c4 + re_c4);
                acc_c4 = (double)(tp_c4 + tn_c4) / (double)(tp_c4 + tn_c4 + fn_c4 + fp_c4);

                double[] pre_series = new double[] { pre_c1, pre_c2, pre_c3, pre_c4 };
                double[] re_series = new double[] { re_c1, re_c2, re_c3, re_c4 };
                double[] f1_series = new double[] { f1_c1, f1_c2, f1_c3, f1_c4 };
                double[] acc_series = new double[] { acc_c1, acc_c2, acc_c3, acc_c4 };

                if (classifier == "svc") ScottPlots(ref svm_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "knn") ScottPlots(ref knn_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "gnb") ScottPlots(ref gnb_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "bnb") ScottPlots(ref bnb_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "mlp") ScottPlots(ref mlp_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "rf") ScottPlots(ref rf_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "dt") ScottPlots(ref dt_fp, pre_series, re_series, f1_series, acc_series);
                scores[classifier] = new List<double> { pre_c3, re_c3, f1_c3, acc_c3 };

            }

            PlotScoresByClassifier(scores, scenario);
        }

        public void ProcessDSScoresIntraDomain(bool combinedCautious, bool isDisjunctive, int res, string type)
        {
            ClearPlots();
            Dictionary<string, double> hyp_val = new Dictionary<string, double> { { "ab", 0.6 }, { "bc", 0.3 }, { "a", 0.1 }, { "ad", 0.0 } };
            label1.Text = "Support Vector Machine";
            label2.Text = "K-Nearest Neighbor";
            label3.Text = "Gaussian Naive Bayes";
            label4.Text = "Bernoulli Naive Bayes";
            label5.Text = "Multi Layer Perceptron";
            label6.Text = "Random Forest";
            label7.Text = "Decision Tree";
            label8.Text = "Comparison";

            MassFunction m = new MassFunction(hyp_val);
            m.ComputeFocalSet();
            m.ComputeFrameOfDiscernment();
            List<string> classifiers = new List<string>(new string[] { "svc", "knn", "gnb", "bnb", "mlp", "rf", "dt" });
            Dictionary<string, List<double>> scores = new Dictionary<string, List<double>>();

            foreach (var classifier in classifiers)
            {
                string filePath = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + ".csv";
                var sensor1_mf_list = ConstructMassFunction(filePath, "ds");
                var sensor2_mf_list = ConstructMassFunction(filePath, "master");
                var sensor3_mf_list = ConstructMassFunction(filePath, "router");
                int sampleSize = sensor1_mf_list.Count;

                int tp_c1 = 0, tn_c1 = 0, fp_c1 = 0, fn_c1 = 0;
                double pre_c1 = 0.0, re_c1 = 0.0, f1_c1 = 0.0, acc_c1 = 0.0;

                int tp_c2 = 0, tn_c2 = 0, fp_c2 = 0, fn_c2 = 0;
                double pre_c2 = 0.0, re_c2 = 0.0, f1_c2 = 0.0, acc_c2 = 0.0;

                int tp_c3 = 0, tn_c3 = 0, fp_c3 = 0, fn_c3 = 0;
                double pre_c3 = 0.0, re_c3 = 0.0, f1_c3 = 0.0, acc_c3 = 0.0;

                int tp_c4 = 0, tn_c4 = 0, fp_c4 = 0, fn_c4 = 0;
                double pre_c4 = 0.0, re_c4 = 0.0, f1_c4 = 0.0, acc_c4 = 0.0;

                if (combinedCautious)
                {
                    sensor1_mf_list = ConstructNonDogmaticMassFunction(filePath, "ds");
                    sensor2_mf_list = ConstructNonDogmaticMassFunction(filePath, "master");
                    sensor3_mf_list = ConstructNonDogmaticMassFunction(filePath, "router");
                }

                var labels = GetLabel(filePath);

                List<string> ranked_Hyp_ByBelief = new List<string>();
                List<double> ranked_count_ByBelief = new List<double>();
                List<double> ranked_value_ByBelief = new List<double>();

                List<string> ranked_Hyp_ByPlausibility = new List<string>();
                List<double> ranked_count_ByPlausibility = new List<double>();
                List<double> ranked_value_ByPlausibility = new List<double>();

                List<string> rankedPignistic = new List<string>();
                List<double> ranked_count_ByPignistic = new List<double>();
                List<double> ranked_value_ByPignistic = new List<double>();

                // construct mf using Generalized bayesian Theorem and store it here for every sample fused.
                List<Dictionary<string, double>> gbt_sample_fused = new List<Dictionary<string, double>>();
                List<string> ranked_mf_gbt = new List<string>();
                List<double> ranked_count_ByGBT = new List<double>();
                List<double> ranked_value_ByGBT = new List<double>();

                List<double> conflictMeasure = new List<double>();
                List<double> hartleyMeasure = new List<double>();


                for (int i = 0; i < sampleSize; i++)
                {
                    var s1_mf = sensor1_mf_list[i];
                    var s2_mf = sensor2_mf_list[i];
                    var fused = m.combinedDeterministically(s1_mf, s2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) fused = m.combineCautious(s1_mf, s2_mf, isDisjunctive);
                    var s3_mf = sensor3_mf_list[i];
                    var fused3 = m.combinedDeterministically(fused, s3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) fused3 = m.combineCautious(fused, s3_mf, isDisjunctive);

                    //conflict measure
                    MassFunction t1 = new MassFunction(fused);
                    MassFunction t2 = new MassFunction(s3_mf);
                    var conflict = m.conflict(t1, t2, 0);
                    conflictMeasure.Add(conflict);

                    //hartley measure
                    var hartley = m.hartley_measure(fused3);
                    hartleyMeasure.Add(hartley);

                    MassFunction newM = new MassFunction(fused3);
                    newM.ComputeFocalSet();
                    newM.ComputeFrameOfDiscernment();
                    var bel_func = newM.beliefFunction();
                    var plaus_func = newM.plausFunction();

                    // test gbt 
                    var gbt_mf = newM.gbt(plaus_func, false, 0);
                    gbt_sample_fused.Add(gbt_mf);


                    // decision basis
                    var max_hyp_bel = bel_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByBelief.Add(max_hyp_bel);
                    if (max_hyp_bel.Contains('a'))
                    {
                        ranked_value_ByBelief.Add(Math.Round(bel_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c1 += 1;
                        else fp_c1 += 1;
                    }
                    else
                    {
                        ranked_value_ByBelief.Add(0.0);
                        if (labels[i] == 0.0) tn_c1 += 1;
                        else fn_c1 += 1;
                    }
                    if (max_hyp_bel.Length == 1 && max_hyp_bel.Contains('b')) ranked_count_ByBelief.Add(-1);
                    else ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));
                    //ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));

                    var max_hyp_plaus = plaus_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByPlausibility.Add(max_hyp_plaus);
                    if (max_hyp_plaus.Contains('a'))
                    {
                        ranked_value_ByPlausibility.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c2 += 1;
                        else fp_c2 += 1;
                    }
                    else
                    {
                        ranked_value_ByPlausibility.Add(0.0);
                        if (labels[i] == 0.0) tn_c2 += 1;
                        else fn_c2 += 1;
                    }
                    if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPlausibility.Add(-1);
                    else ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    //ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));

                    var pignisticFunction = newM.pignistic();
                    if (pignisticFunction != null && pignisticFunction.Count != 0)
                    {
                        var max_pignistic = pignisticFunction.OrderByDescending(x => x.Value).First().Key;
                        rankedPignistic.Add(max_pignistic);
                        if (max_pignistic.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(pignisticFunction.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_pignistic.Length == 1 && max_pignistic.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                    }
                    else
                    {
                        rankedPignistic.Add(max_hyp_plaus);
                        if (max_hyp_plaus.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    }

                    var max_mf_gbt = gbt_mf.OrderByDescending(x => x.Value).First().Key;
                    ranked_mf_gbt.Add(max_mf_gbt);
                    if (max_mf_gbt.Contains('a'))
                    {
                        ranked_value_ByGBT.Add(Math.Round(gbt_mf.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c4 += 1;
                        else fp_c4 += 1;
                    }
                    else
                    {
                        ranked_value_ByGBT.Add(0.0);
                        if (labels[i] == 0.0) tn_c4 += 1;
                        else fn_c4 += 1;
                    }
                    if (max_mf_gbt.Length == 1 && max_mf_gbt.Contains('b')) ranked_count_ByGBT.Add(-1);
                    else ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                    //ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                }

                //Time
                List<double> realtime = new List<double>();
                for (double i = 0; i < sampleSize; i++) realtime.Add(i + 1);

                //plot Labels
                //var plt_label = new ScottPlot.Plot(800, 400);
                //plt_label.PlotScatter(realtime.ToArray(), labels.ToArray());
                //plt_label.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_labels.png");

                ////plot conflict measure
                //var plt_conflict = new ScottPlot.Plot(800, 400);
                //plt_conflict.PlotScatter(realtime.ToArray(), conflictMeasure.ToArray());
                //plt_conflict.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_conflict_measure.png");

                ////plot hartley measure
                //var plt_hartley = new ScottPlot.Plot(800, 400);
                //plt_hartley.PlotScatter(realtime.ToArray(), hartleyMeasure.ToArray());
                //plt_hartley.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_hartley_measure.png");

                ////plot the decision rules based on the count of strings that were ranked highest in the evidences
                //var plt_rank_belief = new ScottPlot.Plot(800, 400);
                //plt_rank_belief.PlotScatter(realtime.ToArray(), ranked_count_ByBelief.ToArray());
                //plt_rank_belief.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_belief_count.png");

                //var plt_rank_belief_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_belief_scores.PlotScatter(realtime.ToArray(), ranked_value_ByBelief.ToArray());
                //plt_rank_belief_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_belief_scores.png");

                //var plt_rank_plausibility = new ScottPlot.Plot(800, 400);
                //plt_rank_plausibility.PlotScatter(realtime.ToArray(), ranked_count_ByPlausibility.ToArray());
                //plt_rank_plausibility.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_plausibility_count.png");

                //var plt_rank_plausibility_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_plausibility_scores.PlotScatter(realtime.ToArray(), ranked_value_ByPlausibility.ToArray());
                //plt_rank_plausibility_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_plausibility_scores.png");

                //var plt_rank_pignistic = new ScottPlot.Plot(800, 400);
                //plt_rank_pignistic.PlotScatter(realtime.ToArray(), ranked_count_ByPignistic.ToArray());
                //plt_rank_pignistic.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_pignistic_count.png");

                //var plt_rank_pignistic_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_pignistic_scores.PlotScatter(realtime.ToArray(), ranked_value_ByPignistic.ToArray());
                //plt_rank_pignistic_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_pignistic_scores.png");

                //var plt_rank_gbt = new ScottPlot.Plot(800, 400);
                //plt_rank_gbt.PlotScatter(realtime.ToArray(), ranked_count_ByGBT.ToArray());
                //plt_rank_gbt.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_gbt_count.png");

                //var plt_rank_gbt_scores = new ScottPlot.Plot(800, 400);
                //plt_rank_gbt_scores.PlotScatter(realtime.ToArray(), ranked_value_ByGBT.ToArray());
                //plt_rank_gbt_scores.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_rank_gbt_scores.png");

                pre_c1 = (double)tp_c1 / (double)(tp_c1 + fp_c1);
                re_c1 = (double)tp_c1 / (double)(tp_c1 + fn_c1);
                f1_c1 = (double)(2 * pre_c1 * re_c1) / (double)(pre_c1 + re_c1);
                acc_c1 = (double)(tp_c1 + tn_c1) / (double)(tp_c1 + tn_c1 + fn_c1 + fp_c1);

                pre_c2 = (double)tp_c2 / (double)(tp_c2 + fp_c2);
                re_c2 = (double)tp_c2 / (double)(tp_c2 + fn_c2);
                f1_c2 = (double)(2 * pre_c2 * re_c2) / (double)(pre_c2 + re_c2);
                acc_c2 = (double)(tp_c2 + tn_c2) / (double)(tp_c2 + tn_c2 + fn_c2 + fp_c2);

                if ((tp_c3 + fp_c3) == 0) pre_c3 = 0.0;
                else pre_c3 = (double)tp_c3 / (double)(tp_c3 + fp_c3);

                re_c3 = (double)tp_c3 / (double)(tp_c3 + fn_c3);

                if ((pre_c3 + re_c3) == 0.0) f1_c3 = 0.0;
                else f1_c3 = (double)(2 * pre_c3 * re_c3) / (double)(pre_c3 + re_c3);

                acc_c3 = (double)(tp_c3 + tn_c3) / (double)(tp_c3 + tn_c3 + fn_c3 + fp_c3);

                pre_c4 = (double)tp_c4 / (double)(tp_c4 + fp_c4);
                re_c4 = (double)tp_c4 / (double)(tp_c4 + fn_c4);
                f1_c4 = (double)(2 * pre_c4 * re_c4) / (double)(pre_c4 + re_c4);
                acc_c4 = (double)(tp_c4 + tn_c4) / (double)(tp_c4 + tn_c4 + fn_c4 + fp_c4);

                double[] pre_series = new double[] { pre_c1, pre_c2, pre_c3, pre_c4 };
                double[] re_series = new double[] { re_c1, re_c2, re_c3, re_c4 };
                double[] f1_series = new double[] { f1_c1, f1_c2, f1_c3, f1_c4 };
                double[] acc_series = new double[] { acc_c1, acc_c2, acc_c3, acc_c4 };

                if (classifier == "svc") ScottPlots(ref svm_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "knn") ScottPlots(ref knn_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "gnb") ScottPlots(ref gnb_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "bnb") ScottPlots(ref bnb_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "mlp") ScottPlots(ref mlp_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "rf") ScottPlots(ref rf_fp, pre_series, re_series, f1_series, acc_series);
                else if (classifier == "dt") ScottPlots(ref dt_fp, pre_series, re_series, f1_series, acc_series);
                scores[classifier] = new List<double> { pre_c3, re_c3, f1_c3, acc_c3 };

            }

            // Plot the scores separately for different classifier
            PlotScoresByClassifier(scores, scenario);
        }

        public void ProcessRawDSScores(bool combinedCautious, bool isDisjunctive, int res, string type, string classifier)
        {
            ClearPlots();
            Dictionary<string, double> hyp_val = new Dictionary<string, double> { { "ab", 0.6 }, { "bc", 0.3 }, { "a", 0.1 }, { "ad", 0.0 } };

            MassFunction m = new MassFunction(hyp_val);
            m.ComputeFocalSet();
            m.ComputeFrameOfDiscernment();

            ////compute for 1000 samples
            int sampleSize = 1000;

            List<string> classifiers = new List<string>(new string[] { "svc", "knn", "gnb", "bnb", "mlp", "rf", "dt" });

            //string type = "cp";

            Dictionary<string, List<double>> scores = new Dictionary<string, List<double>>();
            
                string filePath_pc = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_pc.csv";
                var sensor1_mf_list = ConstructMassFunction(filePath_pc, "ds");
                var sensor2_mf_list = ConstructMassFunction(filePath_pc, "master");
                var sensor3_mf_list = ConstructMassFunction(filePath_pc, "router");

                string filePath_pp = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_pp.csv";
                var sensor4_mf_list = ConstructMassFunction(filePath_pp, "ds");
                var sensor5_mf_list = ConstructMassFunction(filePath_pp, "master");
                var sensor6_mf_list = ConstructMassFunction(filePath_pp, "router");

                sampleSize = sensor1_mf_list.Count;

                int tp_c1 = 0, tn_c1 = 0, fp_c1 = 0, fn_c1 = 0;
                double pre_c1 = 0.0, re_c1 = 0.0, f1_c1 = 0.0, acc_c1 = 0.0;

                int tp_c2 = 0, tn_c2 = 0, fp_c2 = 0, fn_c2 = 0;
                double pre_c2 = 0.0, re_c2 = 0.0, f1_c2 = 0.0, acc_c2 = 0.0;

                int tp_c3 = 0, tn_c3 = 0, fp_c3 = 0, fn_c3 = 0;
                double pre_c3 = 0.0, re_c3 = 0.0, f1_c3 = 0.0, acc_c3 = 0.0;

                int tp_c4 = 0, tn_c4 = 0, fp_c4 = 0, fn_c4 = 0;
                double pre_c4 = 0.0, re_c4 = 0.0, f1_c4 = 0.0, acc_c4 = 0.0;

                if (combinedCautious)
                {
                    sensor1_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "ds");
                    sensor2_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "master");
                    sensor3_mf_list = ConstructNonDogmaticMassFunction(filePath_pc, "router");
                    sensor4_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "ds");
                    sensor5_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "master");
                    sensor6_mf_list = ConstructNonDogmaticMassFunction(filePath_pp, "router");
                }

                var labels = GetLabel(filePath_pc);

                List<string> ranked_Hyp_ByBelief = new List<string>();
                List<double> ranked_count_ByBelief = new List<double>();
                List<double> ranked_value_ByBelief = new List<double>();

                List<string> ranked_Hyp_ByPlausibility = new List<string>();
                List<double> ranked_count_ByPlausibility = new List<double>();
                List<double> ranked_value_ByPlausibility = new List<double>();

                List<string> rankedPignistic = new List<string>();
                List<double> ranked_count_ByPignistic = new List<double>();
                List<double> ranked_value_ByPignistic = new List<double>();

                // construct mf using Generalized bayesian Theorem and store it here for every sample fused.
                List<Dictionary<string, double>> gbt_sample_fused = new List<Dictionary<string, double>>();
                List<string> ranked_mf_gbt = new List<string>();
                List<double> ranked_count_ByGBT = new List<double>();
                List<double> ranked_value_ByGBT = new List<double>();

                List<double> conflictMeasure = new List<double>();
                List<double> hartleyMeasure = new List<double>();


                for (int i = 0; i < sampleSize; i++)
                {
                    var s1_mf = sensor1_mf_list[i];
                    var s2_mf = sensor2_mf_list[i];
                    var cfused = m.combinedDeterministically(s1_mf, s2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) cfused = m.combineCautious(s1_mf, s2_mf, isDisjunctive);
                    var s3_mf = sensor3_mf_list[i];
                    var cfused3 = m.combinedDeterministically(cfused, s3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) cfused3 = m.combineCautious(cfused, s3_mf, isDisjunctive);

                    var p1_mf = sensor4_mf_list[i];
                    var p2_mf = sensor5_mf_list[i];
                    var pfused = m.combinedDeterministically(p1_mf, p2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) pfused = m.combineCautious(p1_mf, p2_mf, isDisjunctive);
                    var p3_mf = sensor3_mf_list[i];
                    var pfused3 = m.combinedDeterministically(pfused, p3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) pfused3 = m.combineCautious(pfused, p3_mf, isDisjunctive);

                    var fused3 = m.combinedDeterministically(cfused, pfused, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) fused3 = m.combineCautious(cfused, pfused, isDisjunctive);


                    //conflict measure
                    MassFunction t1 = new MassFunction(cfused);
                    MassFunction t2 = new MassFunction(pfused);
                    var conflict = m.conflict(t1, t2, 0);
                    conflictMeasure.Add(conflict);

                    //hartley measure
                    var hartley = m.hartley_measure(fused3);
                    hartleyMeasure.Add(hartley);

                    MassFunction newM = new MassFunction(fused3);
                    newM.ComputeFocalSet();
                    newM.ComputeFrameOfDiscernment();
                    var bel_func = newM.beliefFunction();
                    var plaus_func = newM.plausFunction();

                    // test gbt 
                    var gbt_mf = newM.gbt(plaus_func, false, 0);
                    gbt_sample_fused.Add(gbt_mf);


                    // decision basis
                    var max_hyp_bel = bel_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByBelief.Add(max_hyp_bel);
                    if (max_hyp_bel.Contains('a'))
                    {
                        ranked_value_ByBelief.Add(Math.Round(bel_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c1 += 1;
                        else fp_c1 += 1;
                    }
                    else
                    {
                        ranked_value_ByBelief.Add(0.0);
                        if (labels[i] == 0.0) tn_c1 += 1;
                        else fn_c1 += 1;
                    }
                    if (max_hyp_bel.Length == 1 && max_hyp_bel.Contains('b')) ranked_count_ByBelief.Add(-1);
                    else ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));
                    //ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));

                    var max_hyp_plaus = plaus_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByPlausibility.Add(max_hyp_plaus);
                    if (max_hyp_plaus.Contains('a'))
                    {
                        ranked_value_ByPlausibility.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c2 += 1;
                        else fp_c2 += 1;
                    }
                    else
                    {
                        ranked_value_ByPlausibility.Add(0.0);
                        if (labels[i] == 0.0) tn_c2 += 1;
                        else fn_c2 += 1;
                    }
                    if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPlausibility.Add(-1);
                    else ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    //ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));

                    var pignisticFunction = newM.pignistic();
                    if (pignisticFunction != null && pignisticFunction.Count != 0)
                    {
                        var max_pignistic = pignisticFunction.OrderByDescending(x => x.Value).First().Key;
                        rankedPignistic.Add(max_pignistic);
                        if (max_pignistic.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(pignisticFunction.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_pignistic.Length == 1 && max_pignistic.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                    }
                    else
                    {
                        rankedPignistic.Add(max_hyp_plaus);
                        if (max_hyp_plaus.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    }

                    var max_mf_gbt = gbt_mf.OrderByDescending(x => x.Value).First().Key;
                    ranked_mf_gbt.Add(max_mf_gbt);
                    if (max_mf_gbt.Contains('a'))
                    {
                        ranked_value_ByGBT.Add(Math.Round(gbt_mf.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c4 += 1;
                        else fp_c4 += 1;
                    }
                    else
                    {
                        ranked_value_ByGBT.Add(0.0);
                        if (labels[i] == 0.0) tn_c4 += 1;
                        else fn_c4 += 1;
                    }
                    if (max_mf_gbt.Length == 1 && max_mf_gbt.Contains('b')) ranked_count_ByGBT.Add(-1);
                    else ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                    //ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                }

                //Time
                List<double> realtime = new List<double>();
                for (double i = 0; i < sampleSize; i++) realtime.Add(i + 1);

            //plt_hartley.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_hartley_measure.png");

                ////plot the decision rules based on the count of strings that were ranked highest in the evidences
                svm_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByBelief.ToArray());
                label1.Text = "Evidences ranked by Belief";
                svm_fp.Render();

                mlp_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByBelief.ToArray());
                label5.Text = "Ranked Belief Scores";
                mlp_fp.Render();

                knn_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByPlausibility.ToArray());
                label2.Text = "Evidences ranked by Plausibility";
                knn_fp.Render();

                rf_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByPlausibility.ToArray());
                label6.Text = "Ranked Plausibility Scores";
                rf_fp.Render();

                gnb_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByPignistic.ToArray());
                label3.Text = "Evidences ranked by Pignistic";
                gnb_fp.Render();

                dt_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByPignistic.ToArray());
                label7.Text = "Ranked Pignistic Scores";
                dt_fp.Render();

                bnb_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByGBT.ToArray());
                label4.Text = "Evidences ranked by GBT";
                bnb_fp.Render();

                comparison_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByGBT.ToArray());
                label8.Text = "Ranked GBT Scores";
                comparison_fp.Render();

        }

        public void ProcessRawDSScoresIntraDomain(bool combinedCautious, bool isDisjunctive, int res, string type, string classifier)
        {
            ClearPlots();
            Dictionary<string, double> hyp_val = new Dictionary<string, double> { { "ab", 0.6 }, { "bc", 0.3 }, { "a", 0.1 }, { "ad", 0.0 } };

            MassFunction m = new MassFunction(hyp_val);
            m.ComputeFocalSet();
            m.ComputeFrameOfDiscernment();
            List<string> classifiers = new List<string>(new string[] { "svc", "knn", "gnb", "bnb", "mlp", "rf", "dt" });
            Dictionary<string, List<double>> scores = new Dictionary<string, List<double>>();

          
                string filePath = filePathGlobal + classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + ".csv";
                var sensor1_mf_list = ConstructMassFunction(filePath, "ds");
                var sensor2_mf_list = ConstructMassFunction(filePath, "master");
                var sensor3_mf_list = ConstructMassFunction(filePath, "router");
                int sampleSize = sensor1_mf_list.Count;

                int tp_c1 = 0, tn_c1 = 0, fp_c1 = 0, fn_c1 = 0;
                double pre_c1 = 0.0, re_c1 = 0.0, f1_c1 = 0.0, acc_c1 = 0.0;

                int tp_c2 = 0, tn_c2 = 0, fp_c2 = 0, fn_c2 = 0;
                double pre_c2 = 0.0, re_c2 = 0.0, f1_c2 = 0.0, acc_c2 = 0.0;

                int tp_c3 = 0, tn_c3 = 0, fp_c3 = 0, fn_c3 = 0;
                double pre_c3 = 0.0, re_c3 = 0.0, f1_c3 = 0.0, acc_c3 = 0.0;

                int tp_c4 = 0, tn_c4 = 0, fp_c4 = 0, fn_c4 = 0;
                double pre_c4 = 0.0, re_c4 = 0.0, f1_c4 = 0.0, acc_c4 = 0.0;

                if (combinedCautious)
                {
                    sensor1_mf_list = ConstructNonDogmaticMassFunction(filePath, "ds");
                    sensor2_mf_list = ConstructNonDogmaticMassFunction(filePath, "master");
                    sensor3_mf_list = ConstructNonDogmaticMassFunction(filePath, "router");
                }

                var labels = GetLabel(filePath);

                List<string> ranked_Hyp_ByBelief = new List<string>();
                List<double> ranked_count_ByBelief = new List<double>();
                List<double> ranked_value_ByBelief = new List<double>();

                List<string> ranked_Hyp_ByPlausibility = new List<string>();
                List<double> ranked_count_ByPlausibility = new List<double>();
                List<double> ranked_value_ByPlausibility = new List<double>();

                List<string> rankedPignistic = new List<string>();
                List<double> ranked_count_ByPignistic = new List<double>();
                List<double> ranked_value_ByPignistic = new List<double>();

                // construct mf using Generalized bayesian Theorem and store it here for every sample fused.
                List<Dictionary<string, double>> gbt_sample_fused = new List<Dictionary<string, double>>();
                List<string> ranked_mf_gbt = new List<string>();
                List<double> ranked_count_ByGBT = new List<double>();
                List<double> ranked_value_ByGBT = new List<double>();

                List<double> conflictMeasure = new List<double>();
                List<double> hartleyMeasure = new List<double>();


                for (int i = 0; i < sampleSize; i++)
                {
                    var s1_mf = sensor1_mf_list[i];
                    var s2_mf = sensor2_mf_list[i];
                    var fused = m.combinedDeterministically(s1_mf, s2_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (s1_mf.Count != 1) && (s2_mf.Count != 1)) fused = m.combineCautious(s1_mf, s2_mf);
                    if (combinedCautious) fused = m.combineCautious(s1_mf, s2_mf, isDisjunctive);
                    var s3_mf = sensor3_mf_list[i];
                    var fused3 = m.combinedDeterministically(fused, s3_mf, isDisjunctive, combinedCautious);
                    //if (combinedCautious && (fused.Count != 1) && (s3_mf.Count != 1)) fused3 = m.combineCautious(fused, s3_mf);
                    if (combinedCautious) fused3 = m.combineCautious(fused, s3_mf, isDisjunctive);

                    //conflict measure
                    MassFunction t1 = new MassFunction(fused);
                    MassFunction t2 = new MassFunction(s3_mf);
                    var conflict = m.conflict(t1, t2, 0);
                    conflictMeasure.Add(conflict);

                    //hartley measure
                    var hartley = m.hartley_measure(fused3);
                    hartleyMeasure.Add(hartley);

                    MassFunction newM = new MassFunction(fused3);
                    newM.ComputeFocalSet();
                    newM.ComputeFrameOfDiscernment();
                    var bel_func = newM.beliefFunction();
                    var plaus_func = newM.plausFunction();

                    // test gbt 
                    var gbt_mf = newM.gbt(plaus_func, false, 0);
                    gbt_sample_fused.Add(gbt_mf);


                    // decision basis
                    var max_hyp_bel = bel_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByBelief.Add(max_hyp_bel);
                    if (max_hyp_bel.Contains('a'))
                    {
                        ranked_value_ByBelief.Add(Math.Round(bel_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c1 += 1;
                        else fp_c1 += 1;
                    }
                    else
                    {
                        ranked_value_ByBelief.Add(0.0);
                        if (labels[i] == 0.0) tn_c1 += 1;
                        else fn_c1 += 1;
                    }
                    if (max_hyp_bel.Length == 1 && max_hyp_bel.Contains('b')) ranked_count_ByBelief.Add(-1);
                    else ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));
                    //ranked_count_ByBelief.Add(Convert.ToDouble(max_hyp_bel.Length));

                    var max_hyp_plaus = plaus_func.OrderByDescending(x => x.Value).First().Key;
                    ranked_Hyp_ByPlausibility.Add(max_hyp_plaus);
                    if (max_hyp_plaus.Contains('a'))
                    {
                        ranked_value_ByPlausibility.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c2 += 1;
                        else fp_c2 += 1;
                    }
                    else
                    {
                        ranked_value_ByPlausibility.Add(0.0);
                        if (labels[i] == 0.0) tn_c2 += 1;
                        else fn_c2 += 1;
                    }
                    if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPlausibility.Add(-1);
                    else ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    //ranked_count_ByPlausibility.Add(Convert.ToDouble(max_hyp_plaus.Length));

                    var pignisticFunction = newM.pignistic();
                    if (pignisticFunction != null && pignisticFunction.Count != 0)
                    {
                        var max_pignistic = pignisticFunction.OrderByDescending(x => x.Value).First().Key;
                        rankedPignistic.Add(max_pignistic);
                        if (max_pignistic.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(pignisticFunction.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_pignistic.Length == 1 && max_pignistic.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_pignistic.Length));
                    }
                    else
                    {
                        rankedPignistic.Add(max_hyp_plaus);
                        if (max_hyp_plaus.Contains('a'))
                        {
                            ranked_value_ByPignistic.Add(Math.Round(plaus_func.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                            if (labels[i] == 1.0) tp_c3 += 1;
                            else fp_c3 += 1;
                        }
                        else
                        {
                            ranked_value_ByPignistic.Add(0.0);
                            if (labels[i] == 0.0) tn_c3 += 1;
                            else fn_c3 += 1;
                        }
                        if (max_hyp_plaus.Length == 1 && max_hyp_plaus.Contains('b')) ranked_count_ByPignistic.Add(-1);
                        else ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                        //ranked_count_ByPignistic.Add(Convert.ToDouble(max_hyp_plaus.Length));
                    }

                    var max_mf_gbt = gbt_mf.OrderByDescending(x => x.Value).First().Key;
                    ranked_mf_gbt.Add(max_mf_gbt);
                    if (max_mf_gbt.Contains('a'))
                    {
                        ranked_value_ByGBT.Add(Math.Round(gbt_mf.OrderByDescending(x => x.Value).First().Value, 2, MidpointRounding.AwayFromZero));
                        if (labels[i] == 1.0) tp_c4 += 1;
                        else fp_c4 += 1;
                    }
                    else
                    {
                        ranked_value_ByGBT.Add(0.0);
                        if (labels[i] == 0.0) tn_c4 += 1;
                        else fn_c4 += 1;
                    }
                    if (max_mf_gbt.Length == 1 && max_mf_gbt.Contains('b')) ranked_count_ByGBT.Add(-1);
                    else ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                    //ranked_count_ByGBT.Add(Convert.ToDouble(max_mf_gbt.Length));
                }

                //Time
                List<double> realtime = new List<double>();
                for (double i = 0; i < sampleSize; i++) realtime.Add(i + 1);

                //plt_hartley.SaveFig(classifier + "_" + res.ToString() + "s_" + scenario + "_" + type + "_hartley_measure.png");

                ////plot the decision rules based on the count of strings that were ranked highest in the evidences
                svm_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByBelief.ToArray());
                label1.Text = "Evidences ranked by Belief";
                svm_fp.Render();

                mlp_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByBelief.ToArray());
                label5.Text = "Ranked Belief Scores";
                mlp_fp.Render();

                knn_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByPlausibility.ToArray());
                label2.Text = "Evidences ranked by Plausibility";
                knn_fp.Render();

                rf_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByPlausibility.ToArray());
                label6.Text = "Ranked Plausibility Scores";
                rf_fp.Render();

                gnb_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByPignistic.ToArray());
                label3.Text = "Evidences ranked by Pignistic";
                gnb_fp.Render();

                dt_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByPignistic.ToArray());
                label7.Text = "Ranked Pignistic Scores";
                dt_fp.Render();

                bnb_fp.plt.PlotScatter(realtime.ToArray(), ranked_count_ByGBT.ToArray());
                label4.Text = "Evidences ranked by GBT";
                bnb_fp.Render();

                comparison_fp.plt.PlotScatter(realtime.ToArray(), ranked_value_ByGBT.ToArray());
                label8.Text = "Ranked GBT Scores";
                comparison_fp.Render();


        }
        
        public void ClearPlots()
        {
            svm_fp.Reset();
            knn_fp.Reset();
            gnb_fp.Reset();
            bnb_fp.Reset();
            mlp_fp.Reset();
            rf_fp.Reset();
            dt_fp.Reset();
            comparison_fp.Reset();
        }

        public void ScottPlots(ref FormsPlot fp, double[] pre_series, double[] re_series, double[] f1_series, double[] acc_series)
        {
            var x1 = new double[] { 1.0, 2.0, 3.0, 4.0 };

            fp.plt.PlotBar(x1, pre_series, null, "Precision", barWidth: .2, xOffset: -.3);
            fp.plt.PlotBar(x1, re_series, null, "Recall", barWidth: .2, xOffset: -.1);
            fp.plt.PlotBar(x1, f1_series, null, "F1-Score", barWidth: .2, xOffset: .1);
            fp.plt.PlotBar(x1, acc_series, null, "Accuracy", barWidth: .2, xOffset: .3);

            // customize the plot to make it look nicer
            fp.plt.Axis(y1: 0);
            fp.plt.Grid(enableVertical: false, lineStyle: ScottPlot.LineStyle.Dot);
            fp.plt.Axis(y1: 0);
            //fp.plt.Legend(location: ScottPlot.legendLocation.lowerRight);

            // apply custom axis tick labels
            string[] Decisionlabels = { "Belief", "Plausibility", "Pignistic", "GBT" };
            fp.plt.XTicks(x1, Decisionlabels);
            fp.Render();
        }

        public void PlotScoresByClassifier(Dictionary<string, List<double>> scores, string scenario)
        {
            
            var x1 = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0 };
            double[] pre_series = new double[7];
            double[] re_series = new double[7];
            double[] f1_series = new double[7];
            double[] acc_series = new double[7];

            int ctr = 0;
            foreach (var classifier_scores in scores)
            {
                pre_series[ctr] = classifier_scores.Value[0];
                re_series[ctr] = classifier_scores.Value[1];
                f1_series[ctr] = classifier_scores.Value[2];
                acc_series[ctr] = classifier_scores.Value[3];
                ctr += 1;
            }

            comparison_fp.plt.PlotBar(x1, pre_series, null, "Precision", barWidth: .2, xOffset: -.3);
            comparison_fp.plt.PlotBar(x1, re_series, null, "Recall", barWidth: .2, xOffset: -.1);
            comparison_fp.plt.PlotBar(x1, f1_series, null, "F1-Score", barWidth: .2, xOffset: .1);
            comparison_fp.plt.PlotBar(x1, acc_series, null, "Accuracy", barWidth: .2, xOffset: .3);

            // customize the plot to make it look nicer
            comparison_fp.plt.Axis(y1: 0);
            comparison_fp.plt.Grid(enableVertical: false, lineStyle: LineStyle.Dot);
            comparison_fp.plt.Axis(y1: 0);
            //comparison_fp.plt.Legend(location: legendLocation.lowerRight);

            // apply custom axis tick labels
            string[] Decisionlabels = { "SVC", "K-NN", "GNB", "BNB", "MLP", "RF", "DT" };
            comparison_fp.plt.XTicks(x1, Decisionlabels);
            comparison_fp.Render();


        }

        public static List<Dictionary<string, double>> ConstructMassFunction(string filePath, string source)
        {
            List<Dictionary<string, double>> mf_list = new List<Dictionary<string, double>>();
            using (var reader = new StreamReader(filePath))
            {
                int ctr = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ctr += 1;
                    if (ctr > 2)
                    {
                        Dictionary<string, double> hyp = new Dictionary<string, double>();

                        var index = 0;
                        if (source == "ds") index = 2;
                        else if (source == "router") index = 3;
                        else if (source == "master") index = 4;
                        if (values[index] == null || values[index] == "")
                        {
                            hyp["a"] = 0.0; hyp["b"] = 0.0; hyp[""] = 1.0;
                        }
                        else
                        {
                            hyp["a"] = Convert.ToDouble(values[index]);
                            hyp["b"] = 1.0 - Convert.ToDouble(values[index]);
                            hyp[""] = -1.0 * (Math.Pow(hyp["a"] - 0.5, 2.0) + Math.Pow(hyp["b"] - 0.5, 2.0));
                            hyp["a"] *= (1.0 - hyp[""]);
                            hyp["b"] *= (1.0 - hyp[""]);
                        }

                        mf_list.Add(hyp);
                    }

                }
            }
            return mf_list;
        }

        public static List<Dictionary<string, double>> ConstructNonDogmaticMassFunction(string filePath, string source)
        {
            List<Dictionary<string, double>> mf_list = new List<Dictionary<string, double>>();
            using (var reader = new StreamReader(filePath))
            {
                int ctr = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ctr += 1;
                    if (ctr > 2)
                    {
                        Dictionary<string, double> hyp = new Dictionary<string, double>();

                        var index = 0;
                        if (source == "ds") index = 2;
                        else if (source == "router") index = 3;
                        else if (source == "master") index = 4;
                        if (values[index] == null || values[index] == "")
                        {
                            hyp["a"] = 0.0; hyp["b"] = 0.0; hyp[""] = 0.9; hyp["ab"] = 0.1;
                        }
                        else
                        {
                            hyp["a"] = 0.9 * Convert.ToDouble(values[index]);
                            hyp["b"] = 0.9 * (1.0 - Convert.ToDouble(values[index]));
                            hyp["ab"] = 0.1;
                            hyp[""] = -1.0 * (Math.Pow(hyp["a"] - 0.45, 2.0) + Math.Pow(hyp["b"] - 0.45, 2.0));
                            hyp["a"] *= (1.0 - hyp[""]);
                            hyp["b"] *= (1.0 - hyp[""]);
                            hyp["ab"] *= (1.0 - hyp[""]);
                        }

                        mf_list.Add(hyp);
                    }

                }
            }
            return mf_list;
        }

        public static List<double> GetLabel(string filePath)
        {
            List<double> labels = new List<double>();
            using (var reader = new StreamReader(filePath))
            {
                int ctr = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ctr += 1;
                    if (ctr > 2)
                    {
                        labels.Add(Convert.ToDouble(values[5]));
                    }

                }
            }
            return labels;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            bool isDisjunctive = false;
            bool combineCautious = false;
            string type = "cp";
            if(fusionTypeCB.SelectedItem.ToString() == "Disjunctive")
            {
                isDisjunctive = true;
                combineCautious = false;
            }
            else if(fusionTypeCB.SelectedItem.ToString() == "Conjunctive")
            {
                isDisjunctive = false;
                combineCautious = false;
            }
            else if(fusionTypeCB.SelectedItem.ToString() == "Combine-Cautious")
            {
                isDisjunctive = false;
                combineCautious = true;
            }

            if (domainTypeCB.SelectedItem.ToString() == "Pure-Cyber") { type = "pc"; combineCautious = false; }
            else if (domainTypeCB.SelectedItem.ToString() == "Pure-Physical") { type = "pp"; combineCautious = false; }
            else if (domainTypeCB.SelectedItem.ToString() == "Cyber-Physical") type = "cp";
 

            int res = Convert.ToInt32(timeResCB.SelectedItem); 

            if (metricTypeCB.SelectedItem.ToString() == "Decision Criteria")
            {
                if (mergeLocDomainCB.Checked == true)
                {
                    ProcessRawDSScores(combineCautious, isDisjunctive, res, type, selectClfCB.SelectedItem.ToString().ToLower());
                }
                else
                {
                    ProcessRawDSScoresIntraDomain(combineCautious, isDisjunctive, res, type,selectClfCB.SelectedItem.ToString().ToLower());
                }

            }
            else
            {
                if (mergeLocDomainCB.Checked == true)
                {
                    ProcessDSScores(combineCautious, isDisjunctive, res, type);
                }
                else
                {
                    ProcessDSScoresIntraDomain(combineCautious, isDisjunctive, res, type);
                }

            }




        }

        private void mergeLocDomainCB_CheckedChanged(object sender, EventArgs e)
        {
            if (mergeLocDomainCB.Checked == true)domainTypeCB.Enabled = false;    
            else domainTypeCB.Enabled = true;
        }

        private void metricTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metricTypeCB.SelectedItem.ToString()=="Decision Criteria")
            {
                selectClfCB.Visible = true;
                label13.Visible = true;
            }
            else
            {
                selectClfCB.Visible = false;
                label13.Visible = false;
            }
        }
    }
}
