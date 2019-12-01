using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Vimachem.Hackathon.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Schematics.xaml
    /// </summary>
    public partial class SchematicsTab : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }

        public SchematicsTab()
        {
            InitializeComponent();
            Production.Current.NewTube += Current_NewTube;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>()
                }
            };
            DataContext = this;

        }

        private void Current_NewTube(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                BatchCompletion.Content = String.Format("Batch Completion: {0:P}", Production.Current.Count / (Production.Current.Total * 1.0));
                TotalLengthRejected.Content = String.Format("Total Length Rejected: {0:0.00}m", Production.Current.Tubes.Sum(x => x.Cuts*0.2));
                PipesRejected.Content = String.Format("Pipes Rejected: {0}", Production.Current.Tubes.Count(x => x.Cuts >= 3));
                FirstPassCount.Content = String.Format("First Pass Count: {0}", Production.Current.Tubes.Count(x => x.Cuts == 1));
                FirstPassYield.Content = String.Format("First Pass Yield: {0:P}", Production.Current.Tubes.Count(x => x.Cuts == 1) / (Production.Current.Count * 1.0));
                CopperMassRejectedPercentage.Content = String.Format("Copper Mass Rejected Percentage: {0:P}", Production.Current.Tubes.Sum(x => x.Cuts * 0.2) / Production.Current.Tubes.Sum(x => x.Length+ x.Cuts *0.2));

                if (Production.Current.Count > 1)
                {
                    SeriesCollection[0].Values.Add(Production.Current.Tubes[Production.Current.Count - 2].Cuts);

                }



            }));
        }
    }
}
