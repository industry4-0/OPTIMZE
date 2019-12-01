using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Vimachem.Hackathon.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class CalibrationTab : UserControl
    {
        ColorAnimation animation = new ColorAnimation();
        Storyboard moveStoryboard = new Storyboard();
        Button pushedButton = null;
        public CalibrationTab()
        {
            InitializeComponent();
            InsertUnit.Height = 0;

            animation.From = Colors.White;
            animation.To = Colors.Black;
            animation.RepeatBehavior = new RepeatBehavior(4);
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.250));
            animation.AutoReverse = true;

            //this.Background = (Brush)new BrushConverter().ConvertFrom("#0C4DA1");
        }

        private void ScannerCalibration_Click(object sender, RoutedEventArgs e)
        {
            pushedButton = (Button)sender;
            InsertUnit.Height = 50;
            InsertUnit.Content = "Insert Scanner Calibration Unit";
            BlinkInsertButton();

        }

        private void EncoderCalibration_Click(object sender, RoutedEventArgs e)
        {
            pushedButton = (Button)sender;
            InsertUnit.Height = 50;
            InsertUnit.Content = "Insert Encoder Calibration Unit";
            BlinkInsertButton();
        }

        private void BlinkInsertButton()
        {
            moveStoryboard.Completed += MoveStoryboard_Completed;
            Storyboard.SetTarget(animation, InsertUnit);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
            moveStoryboard.Children.Add(animation);
            moveStoryboard.Begin();
        }

        private void MoveStoryboard_Completed(object sender, EventArgs e)
        {
            BlinkButton(pushedButton);
        }
                     
        private void MoveStoryboard_Completed1(object sender, EventArgs e)
        {
            string message = null;
            if (pushedButton.Name == "ScannerCalibration")
            {
                scannerDate2.IsChecked = true;
                message = "Performed scanner calibration";
            }
            else if (pushedButton.Name == "EncoderCalibration")
            {
                encoderDate2.IsChecked = true;
                message = "Performed encoder calibration";
            }
            moveStoryboard.Completed -= MoveStoryboard_Completed1;


            
            HistoryTab.actions.Add(new Msg() { ID = (HistoryTab.actions.Max(x=> Int32.Parse(x.ID))+1).ToString(), Message= message, Date = DateTime.Now });
        }

        private void BlinkButton(Button pushedButton)
        {
            InsertUnit.Content = InsertUnit.Content.ToString().Replace("Insert", "Inserted");


            moveStoryboard.Completed += MoveStoryboard_Completed1;
            Storyboard.SetTarget(animation, pushedButton);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
            moveStoryboard.Children.Add(animation);
            moveStoryboard.Begin();
            moveStoryboard.Completed -= MoveStoryboard_Completed;
        }
    }
}
