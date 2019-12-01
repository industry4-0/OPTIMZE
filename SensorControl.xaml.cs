using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vimachem.Hackathon
{
    /// <summary>
    /// Interaction logic for SensorControl.xaml
    /// </summary>
    public partial class SensorControl : UserControl
    {
        public static Random rnd = new Random();

        public SensorControl()
        {
            InitializeComponent();
            chartGauge.LabelFormatter = a => String.Format("{0:#,##0.00}", a);
        }

        public string Title { get => lbTitle.Content.ToString(); set => lbTitle.Content = value; }
        public double From { get => chartGauge.From; set => chartGauge.From = value; }
        public double To { get => chartGauge.To; set => chartGauge.To = value; }
        public double Value { get => chartGauge.Value; set => chartGauge.Value = value; }

        public double NominalValue { get; set; }

        public double Tolerance { get; set; }

        public void GenerateValue()
        {
            var start = NominalValue * (1 - Tolerance*1.3);
            var end = NominalValue * (1 + Tolerance*1.3);

            var val = start + ((end - start) * rnd.NextDouble());

            if (val > NominalValue * (1 + Tolerance))
            {
                Background = Brushes.Red;
            }
            else if (val < NominalValue * (1 - Tolerance))
            {
                Background = Brushes.Red;
            }
            else
            {
                Background = (Brush)new BrushConverter().ConvertFrom("#FFB7B7B7");
            }
            Value = val;
        }


    }
}
