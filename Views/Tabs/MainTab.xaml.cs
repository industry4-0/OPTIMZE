using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Vimachem.Hackathon.Views.Tabs
{
    /// <summary>
    /// Interaction logic for MainTab.xaml
    /// </summary>
    public partial class MainTab : UserControl
    {

        private Random generator = new Random();
        //private System.Timers.Timer timer = new System.Timers.Timer(1000);
        //private System.Timers.Timer timer2 = new System.Timers.Timer(5000);
        public ChartValues<DynamicCharts> ChartValues { get; set; }

        private bool bearingStarted = false;
        private bool engineStarted = false;

        private int encoderValue = 0;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private List<RecipeParameter> Recipe = new List<RecipeParameter>();

        public static ConcurrentDictionary<string, string> GlobalVariables = new ConcurrentDictionary<string, string>();

        public MainTab()
        {
            InitializeComponent();
            this.Loaded += MainTab_Loaded;

            //timer.Enabled = true;
            //timer.AutoReset = true;
            //timer.Elapsed += TimerOnElapsed;

            //timer2.Enabled = true;
            //timer2.AutoReset = true;
            //timer2.Elapsed += TimerOnElapsed2;

            GlobalVariables.GetOrAdd("Height", "0");
            GlobalVariables.GetOrAdd("Width", "0");
            GlobalVariables.GetOrAdd("Total Length", "0");

            Machine.Current.TimeElapsed += Current_TimeElapsed;
            ChartValues = new ChartValues<DynamicCharts>();
            int min = 0;
            for (double i = 0; i < 0.20; i += 0.01)
            {
                ChartValues.Add(new DynamicCharts
                {
                    DateTime = DateTime.Today.AddMinutes(min++),
                    Value = i
                });
            }
            Production.Current.Cut += Current_Cut;
            Production.Current.Pass += Current_Pass;
            Production.Current.Reject += Current_Reject;
            Production.Current.Inspect += Current_Inspect;
            Production.Current.NewTube += Current_NewTube;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>()
                }
            };

            ////adding series will update and animate the chart automatically
            //SeriesCollection.Add(new ColumnSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42 }
            //});

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Formatter = value => value.ToString("N");


            DataContext = this;

        }

        private void Current_NewTube(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Reject.Background = Brushes.Transparent;
                Pass.Background = Brushes.Transparent;
                Cut.Background = Brushes.Transparent;
                EncoderValue.Content = Production.Current.CurrentTube.Length.ToString("0.00");
                CutNum.Content = Production.Current.CurrentTube.Cuts.ToString("0.00");
                OuterDiamention.Content = Production.Current.CurrentTube.OuterDiameter.ToString("0.00");
                Distortion.Content = Production.Current.CurrentTube.Distortion.ToString("0.00");

                if (Production.Current.Count > 1)
                {
                    SeriesCollection[0].Values.Add(Production.Current.Tubes[Production.Current.Count - 2].Cuts);
                    if (SeriesCollection[0].Values.Count >= 30)
                    {
                        SeriesCollection[0].Values.RemoveAt(0);
                    }
                }



            }));
        }

        private void Current_Inspect(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Reject.Background = Brushes.Transparent;
                Pass.Background = Brushes.Transparent;
                Cut.Background = Brushes.Transparent;
                EncoderValue.Content = Production.Current.CurrentTube.Length.ToString("0.00");
                CutNum.Content = Production.Current.CurrentTube.Cuts.ToString();
                OuterDiamention.Content = Production.Current.CurrentTube.OuterDiameter.ToString("0.00");
                Distortion.Content = Production.Current.CurrentTube.Distortion.ToString("0.00");
            }));
        }

        private void Current_Reject(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Reject.Background = Brushes.Red;
                Pass.Background = Brushes.Transparent;
                Cut.Background = Brushes.Transparent;
                EncoderValue.Content = Production.Current.CurrentTube.Length.ToString("0.00");
                CutNum.Content = Production.Current.CurrentTube.Cuts.ToString();
                OuterDiamention.Content = Production.Current.CurrentTube.OuterDiameter.ToString("0.00");
                Distortion.Content = Production.Current.CurrentTube.Distortion.ToString("0.00");

            }));
        }

        private void Current_Pass(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Pass.Background = Brushes.Green;
                Reject.Background = Brushes.Transparent;
                Cut.Background = Brushes.Transparent;
                EncoderValue.Content = Production.Current.CurrentTube.Length.ToString("0.00");
                CutNum.Content = Production.Current.CurrentTube.Cuts.ToString();
                OuterDiamention.Content = Production.Current.CurrentTube.OuterDiameter.ToString("0.00");
                Distortion.Content = Production.Current.CurrentTube.Distortion.ToString("0.00"); 
            }));
        }

        private void Current_Cut(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Cut.Background = Brushes.Orange;
                Reject.Background = Brushes.Transparent;
                Pass.Background = Brushes.Transparent;
                EncoderValue.Content = Production.Current.CurrentTube.Length.ToString("0.00");
            }));
        }

        private void Current_TimeElapsed(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                //Sensor1.GenerateValue();
                //Sensor2.GenerateValue();
                //Sensor3.GenerateValue();
                EncoderValue.Content = (5 + new Random().NextDouble() / 10).ToString("0.00");
            }));
        }
        private void SetupSensor(SensorControl sensor, RecipeParameter recipeParameter)
        {

            sensor.Title = String.Format("{0}: {1}({2})", recipeParameter.Description, recipeParameter.Value, recipeParameter.Tolerance);
            sensor.To = Convert.ToDouble(recipeParameter.Value) * (1 + Convert.ToDouble(recipeParameter.Tolerance) * 2 / 100);
            sensor.From = Convert.ToDouble(recipeParameter.Value) * (1 - Convert.ToDouble(recipeParameter.Tolerance) * 2 / 100);
            sensor.Value = Convert.ToDouble(recipeParameter.Value);
            sensor.Tolerance = Convert.ToDouble(recipeParameter.Tolerance) / 100;
            sensor.NominalValue = Convert.ToDouble(recipeParameter.Value);


        }
        private void MainTab_Loaded(object sender, RoutedEventArgs e)
        {
            //Bearings_Start_Stop_Label.Content = "Start";
            //Bearings_Start_Stop.Background = Brushes.Green;

            //Engine_Start_Stop_Label.Content = "Start";
            //Engine_Start_Stop.Background = Brushes.Green;

            //Bearings_Reset.Background = Brushes.Orange;
            //Engine_Reset.Background = Brushes.Orange;


            Recipe = RecipeManager.GetRecipe();

            //SetupSensor(Sensor1, Recipe.FirstOrDefault(x => x.ID == 1));
            //SetupSensor(Sensor2, Recipe.FirstOrDefault(x => x.ID == 2));
            //SetupSensor(Sensor3, Recipe.FirstOrDefault(x => x.ID == 3));



            //recipeParameter = Recipe.FirstOrDefault(x => x.ID == 2);
            //Sensor2Limit.Content = String.Format("{0}: {1}({2})", recipeParameter.Description, recipeParameter.Value, recipeParameter.Tolerance);
            //recipeParameter = Recipe.FirstOrDefault(x => x.ID == 3);
            //Sensor3Limit.Content = String.Format("{0}: {1}({2})", recipeParameter.Description, recipeParameter.Value, recipeParameter.Tolerance);
        }

        private void OnClickResetBearing(object sender, RoutedEventArgs e)
        {
            //UpdateOnClick(sender, "Reseting bearings ...");
            //UpdateOnSuccess(sender, Bearings_Start_Stop_Label, "Reset", Brushes.Orange);
        }

        private void OnClickResetEngine(object sender, RoutedEventArgs e)
        {
            //UpdateOnClick(sender, "Reseting engine ...");
            //UpdateOnSuccess(sender, Engine_Start_Stop_Label, "Reset", Brushes.Orange);
        }

        private void OnClickStartBearing(object sender, RoutedEventArgs e)
        {
            //UpdateOnClick(sender, bearingStarted == true ? "Stoping bearings ..." : "Starting bearings ...");

            //string content = bearingStarted == true ? "Start" : "Stop";
            //Brush color = bearingStarted == true ? Brushes.Green : Brushes.Red;
            //UpdateOnSuccess(sender, Bearings_Start_Stop_Label, content, color);
        }

        private void OnClickStartEngine(object sender, RoutedEventArgs e)
        {
            //if (!bearingStarted)
            //{
            //    ButtonLog.Text = "Cannot start Engine when Bearings" +
            //        " are not started!!";
            //}
            //else
            //{
            //    UpdateOnClick(sender, engineStarted == true ? "Stoping engine" : "Starting engine ...");

            //    string content = engineStarted == true ? "Start" : "Stop";
            //    Brush color = engineStarted == true ? Brushes.Red : Brushes.Green;
            //    UpdateOnSuccess(sender, Engine_Start_Stop_Label, content, color);
            //}
        }

        private void UpdateOnClick(object sender, string message)
        {
            //Button PushedButton = sender as Button;
            //PushedButton.Background = Brushes.Yellow;
            //ButtonLog.Text = message;
        }

        private void UpdateOnSuccess(object sender, Label label, string content, Brush color)
        {
            Button PushedButton = sender as Button;
            PushedButton.Background = Brushes.Yellow;
            Thread thr = new Thread(() => wait(PushedButton, label, content, color));
            thr.Start();
        }

        private void wait(Button PushedButton, Label label, string content, Brush color)
        {
            //Thread.Sleep(3000);

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            //{

            //    label.Content = content;
            //    PushedButton.Background = color;
            //    ButtonLog.Text = "Success!";

            //    if (PushedButton.Name.Contains("Bearings_Start_Stop"))
            //    {
            //        bearingStarted = !bearingStarted;
            //    }
            //    if (PushedButton.Name.Contains("Engine_Start_Stop"))
            //    {
            //        engineStarted = !engineStarted;
            //    }
            //    if (PushedButton.Name.Contains("Bearings_Reset"))
            //    {
            //        bearingStarted = false;
            //        encoderValue = 0;
            //    }
            //    if (PushedButton.Name.Contains("Engine_Reset"))
            //    {
            //        engineStarted = false;
            //    }
            //}));
        }

        private void UpdateButton(object sender, string content, Brush color)
        {
            Button PushedButton = sender as Button;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                PushedButton.Content = content;
                PushedButton.Background = color;
                //ButtonLog.Text = "Success!";
            }));
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            //double nominalValue = 0;
            //double precision = 0;
            //double actualValue = 0;

            //try
            //{
            //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            //    {
            //        Sensor1.GenerateValue();
            //        Sensor2.GenerateValue();
            //        Sensor3.GenerateValue();

            //        nominalValue = GetValue(Recipe.FirstOrDefault(x => x.ID == 1).Value);
            //        precision = GetValue(Recipe.FirstOrDefault(x => x.ID == 1).Tolerance);
            //        actualValue = nominalValue + (generator.NextDouble() / 10) * nominalValue;

            //        // Update UI value.
            //        //if (!VerifyValue(actualValue, nominalValue, precision)) { Sensor1_Value.Background = Brushes.Red; }
            //        //else { Sensor1_Value.Background = Brushes.Green; }
            //        //Sensor1_Value.Value = actualValue;
            //        //GlobalVariables["Height"] = Sensor1_Value.Text;

            //        nominalValue = GetValue(Recipe.FirstOrDefault(x => x.ID == 2).Value);
            //        precision = GetValue(Recipe.FirstOrDefault(x => x.ID == 2).Tolerance);
            //        actualValue = nominalValue + (generator.NextDouble() / 10) * nominalValue;

            //        // Update UI value.
            //        if (!VerifyValue(actualValue, nominalValue, precision)) { Sensor2_Value.Background = Brushes.Red; }
            //        else { Sensor2_Value.Background = Brushes.Green; }
            //        Sensor2_Value.Text = actualValue.ToString("F");
            //        GlobalVariables["Width"] = Sensor2_Value.Text;

            //        //nominalValue = GetValue(Recipe.FirstOrDefault(x => x.ID == 3).Value);
            //        //precision = GetValue(Recipe.FirstOrDefault(x => x.ID == 3).Tolerance);
            //        //actualValue = nominalValue + (generator.NextDouble() / 10) * nominalValue;

            //        //// Update UI value.
            //        //if (!VerifyValue(actualValue, nominalValue, precision)) { Sensor3_Value.Background = Brushes.Red; }
            //        //else { Sensor3_Value.Background = Brushes.Green; }
            //        //Sensor3_Value.Text = actualValue.ToString("F");
            //        //GlobalVariables["Total Length"] = Sensor3_Value.Text;

            //        if (engineStarted)
            //        {
            //            encoderValue++;
            //        }
            //    }));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void TimerOnElapsed2(object sender, ElapsedEventArgs e)
        {
            //double nominalValue = 0;
            //double precision = 0;
            //double actualValue = 0;

            //try
            //{
            //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            //    {

            //        nominalValue = GetValue(Recipe.FirstOrDefault(x => x.ID == 3).Value);
            //        precision = GetValue(Recipe.FirstOrDefault(x => x.ID == 3).Tolerance);
            //        actualValue = nominalValue + (generator.NextDouble() / 10) * nominalValue;

            //        // Update UI value.
            //        if (!VerifyValue(actualValue, nominalValue, precision)) { Sensor3_Value.Background = Brushes.Red; }
            //        else { Sensor3_Value.Background = Brushes.Green; }
            //        Sensor3_Value.Text = actualValue.ToString("F");
            //        GlobalVariables["Length"] = Sensor3_Value.Text;

            //        if (engineStarted)
            //        {
            //            encoderValue++;
            //        }
            //    }));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private double GetValue(string valueString)
        {
            return 0;

            //double value = 0;

            //if (!String.IsNullOrEmpty(valueString) && !Double.TryParse(valueString, out value))
            //{
            //    throw new Exception("Invalid precision format");
            //}

            //return value;
        }

        private bool VerifyValue(double value, double nominal, double precision)
        {
            if (value > nominal + nominal * (precision / 100))
            {
                return false;
            }

            return true;
        }
    }
}
